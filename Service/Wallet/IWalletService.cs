using KaiCryptoTracker.Models;
using KaiCryptoTracker.ApiModels;
namespace KaiCryptoTracker.WalletService;

public interface IWalletService
{
    //create a dto for this later 
    Task AddWalletAsync(Guid userId, string walletaddress, string chain, string Walletname);
    Task<bool> RemoveWalletAsync(Guid WalletId);
    Task<List<Wallet>> GetUserWalletsAsync(Guid userId);
    Task<Wallet> GetWalletDetailsAsync(Guid userId, string walletAddress, string chain);

    Task<WalletPnlSummary> GetWalletPNLAsync(string walletaddress, string chain);

    Task<TokenBalanceByWallet> GetTokenBalanceByWallet(string walletaddress, string chain);

    Task<Activechains?> GetWalletActiveChains(string walletaddress);

    

    
}