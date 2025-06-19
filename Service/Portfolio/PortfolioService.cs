using KaiCryptoTracker.DbContext;
using Microsoft.EntityFrameworkCore;
using KaiCryptoTracker.Models;
using KaiCryptoTracker.PortfolioService;
using KaiCryptoTracker.AllApiCalls;
using KaiCryptoTracker.WalletService;
using Microsoft.Extensions.Caching.Memory;
using KaiCryptoTracker.ApiModels;
using System.Runtime.InteropServices;
using System.Globalization;

public class PortfolioService : IPortfolioService
{
    private readonly ApplicationDbContext _dbcontext;
    private readonly IApiCalls _api;
    private readonly IWalletService _walletservice; 
    private readonly ILogger<PortfolioService> _logger;
    private readonly IMemoryCache _memorycache;
    public PortfolioService(ApplicationDbContext dbcontext, ILogger<PortfolioService> logger, IApiCalls api, IWalletService walletservice, IMemoryCache memorycache)
    {
        _dbcontext = dbcontext;
        _logger = logger;
        _api = api;
        _walletservice = walletservice;
        _memorycache = memorycache;

    }
    public async Task<Portfolio> AddPortfolioAsync(Guid userId, string? portfolioname)
    {
        if (string.IsNullOrEmpty(portfolioname)) portfolioname = "Default";
        var portfolio = new Portfolio()
        {
            Chains = new List<Chain>() { },
            Wallets = new List<Wallet>() { },
            PortfolioName = portfolioname,
            TotalTokens = 0,
            TotalValueUsd = 0,
            TotalWallets = 0,
            UserId = userId,

        };
        
        _dbcontext.Add(portfolio);
        await _dbcontext.SaveChangesAsync();

        return portfolio;
       
    }

    public Task DeletePortfolioAsync(Guid userId, Guid PortfolioId)
    {
        throw new NotImplementedException();
    }


    //get all user Token holdings 
    public async Task<List<AggregatedTokenHoldings>> GetUserTokenHoldingsAsync(Guid userId)
    {

        //get user wallets, select the tokenholdings and also the tokenmetadata
        var tokenholdings = await _dbcontext.Wallets.Where(u => u.UserId == userId)
        .SelectMany(th => th.tokenHoldings)
        .Include(tmd => tmd.TokenMetadata)

        .ToListAsync();

         //make symbol and chain as key
        return tokenholdings.GroupBy(th => new { th.TokenMetadata.Symbol, th.TokenMetadata.Chain.ChainName })
        .Select(c => new AggregatedTokenHoldings
        {
            Symbol = c.Key.Symbol,
            Chain = c.Key.ChainName
        })
        .ToList();

    }   


    //function to retrieve and cache data needed for portfolio overview 
    public async Task<Portfolio?> GetPortfolioOverviewAsync(Guid userId)
    {
        string cachekey = $"GetPortfolioOverviewAsync({userId})";
        return await _memorycache.GetOrCreateAsync(cachekey, async entry =>
        {
            entry.AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(3);
            var wallets = await _walletservice.GetUserWalletsAsync(userId);
            var activechains = await GetUserActiveChains(userId);
            var totaltokens = await GetUserTokenHoldingsAsync(userId);
            var tokenbalancebywallet = await GetTokenBalance(userId);    //get token balance and market data 

            var totalwallets = wallets.Count();

            var getuserportfolioId =  _dbcontext.Portfolios.Where(p => p.UserId == userId)
            .Select(p => new
            {
                p.PortfolioId,
                p.PortfolioName

            }).FirstOrDefault();
            
    
            return new Portfolio()
            {
                UserId = userId,
                Wallets = wallets,
                Chains = await GetAllUserChains(getuserportfolioId.PortfolioId),
                PortfolioName = getuserportfolioId.PortfolioName?? "Default",
                TotalTokens = totaltokens.Count,
                TotalValueUsd = GetPorfolioTotalValue(tokenbalancebywallet),


            }; 


        });
      
    }

      // function to get list of user active chains by wallet
    public async Task<Dictionary<string, Activechains?>> GetUserActiveChains(Guid userId)
    {
        //get user wallets 
        var wallets = await _walletservice.GetUserWalletsAsync(userId);

        //call method to get  all active wallet chains (parrallel method)
        var task = wallets.ToDictionary(
            k => k.WalletAddress,
            k => _walletservice.GetWalletActiveChains(k.WalletAddress)
        );
        await Task.WhenAll(task.Values);

        return task.ToDictionary(
            k => k.Key,
            k => k.Value.Result
        );

    }


    //Function to calculate total portfolio value
    public decimal GetPorfolioTotalValue(Dictionary<string, TokenBalanceByWallet> alltokenbalance)
    {
        decimal totalvalue = 0m;
        foreach (var token in alltokenbalance)
        {
            foreach (var item in token.Value.Result)
            {
                if (decimal.TryParse(item.Balance_Formatted, NumberStyles.Any, CultureInfo.InvariantCulture, out var balance))
                {
                    totalvalue += balance * item.Usd_Price;
                }
            }
        }
        return totalvalue;
    }
 

    

    //get token balance by wallet 
    public async Task<Dictionary<string, TokenBalanceByWallet>> GetTokenBalance(Guid userId)
    {
        // //get user wallets and tokenmetadata

        var userwallet = await _dbcontext.Wallets.Where(w => w.UserId == userId)
        .SelectMany(w => w.tokenHoldings.Select(c => new
        {
            walletaddress = w.WalletAddress,
            chain = c.TokenMetadata.Chain.ChainName

        })).Distinct()
        .ToListAsync();

        //call in parrallel mode and 

        var task = userwallet.ToDictionary(
            k => $"{k.chain} : {k.walletaddress}",
            k => _walletservice.GetTokenBalanceByWallet(k.walletaddress, k.chain)

        );
        
        await Task.WhenAll(task.Values);

        return task.ToDictionary(
           k => k.Key,
           k => k.Value.Result
        );

    }

    public Task<Portfolio> UpdatePortfolio(Guid userId, Guid PortfolioId, Guid WalletId)
    {

        // _walletservice.GetWalletActiveChains();
        throw new NotImplementedException();
    }
     


     //Save user active chains to db 
    public async Task SaveUserActiveChainsToPortfolioAsync(Guid userId, Guid portfolioId)
    {
        var activeChainsDict = await GetUserActiveChains(userId);

        var chainsToSave = new List<Chain>();

        foreach (var kvp in activeChainsDict)
        {
            var activeChains = kvp.Value;

            if (activeChains?.active_chains == null) continue;


            foreach (var chain in activeChains.active_chains)
            {
                chainsToSave.Add(new Chain
                {
                    ChainId = Guid.NewGuid(),
                    ChainName = chain.chain,
                    PortfolioId = portfolioId
                });
            }
        }

        foreach (var chain in chainsToSave)
        {

            //check if chain already exist in db 
            bool exists = await _dbcontext.Chains.AnyAsync(c =>
                c.PortfolioId == chain.PortfolioId &&
                c.ChainId == chain.ChainId);

            if (!exists)
            {
                _dbcontext.Chains.Add(chain);
            }
        }

        await _dbcontext.SaveChangesAsync();
    
    }

    public async Task<List<Chain>> GetAllUserChains(Guid PorfolioId)
    {
        return await _dbcontext.Chains.Where(c => c.PortfolioId == PorfolioId).ToListAsync();
    }


    // public async Task<decimal> GetTotalValueAsync(Guid userId)
    // {
    //      var user = await _dbcontext.Users
    //        .Include(u => u.Wallets)
    //         .ThenInclude(w => w.tokenHoldings)
    //         .ThenInclude(th => th.TokenMetadata)
    //        .FirstOrDefaultAsync(u => u.Id == userId);

    // if (user == null)
    //     throw new ArgumentException("User not found");

    // decimal totalValue = 0;

    // foreach (var wallet in user.Wallets)
    // {
    //     foreach (var holding in wallet.tokenHoldings)
    //     {

    //         var priceUsd = await _api.GetPriceCoinGecko(holding.TokenMetadata.Address, holding.TokenMetadata.Chain.ChainName);
    //         totalValue += holding.Balance * priceUsd;
    //     }
    // }

    // return totalValue;
    // }
}