using KaiCryptoTracker.AllApiCalls;
using KaiCryptoTracker.DbContext;
using KaiCryptoTracker.TokenService;

namespace KaiCryptoTracker.SeedData;

public class CoinMetaData
{
    private readonly ApplicationDbContext _dbcontext;
    private readonly ITokenService _tokenservice;
    private readonly ILogger<CoinMetaData> _logger;
    public CoinMetaData(ApplicationDbContext dbcontext, ITokenService tokenservice, ILogger<CoinMetaData> logger)
    {
        _dbcontext = dbcontext;
        _tokenservice = tokenservice;
        _logger = logger;
    }

    public async Task AddCoinSeedData()
    {
        try
        {
            if (!_dbcontext.Coins.Any())
            { 
              var coin = await _tokenservice.SupportedCoinsAfterMergeAsync();
              await _dbcontext.AddRangeAsync(coin);
              await _dbcontext.SaveChangesAsync();
            }

        }
        catch (Exception ex)
        {
            _logger.LogCritical(ex, "Error saving supported tokens metadata to db");
        }
       

       
    }
}