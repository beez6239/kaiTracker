public interface IWalletService
{

    //create a dto for this later 
    Task AddWalletAsync(Guid userId, string walletAddress, string chain, string label);
    Task RemoveWalletAsync(Guid userId, string walletAddress, string chain);
    Task<List<Wallet>> GetUserWalletsAsync(string userId);
    Task<Wallet?> GetWalletDetailsAsync(Guid userId, string walletAddress, string chain);
}