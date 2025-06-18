namespace KaiCryptoTracker.Models;

public class WalletStatDto
{
    public int nfts { get; set; }
    public int collections { get; set; }
    public Transaction transactions { get; set; }
    public NftTransfers nft_transfers { get; set; }
    public NftTransfers token_transfers { get; set; }
   

}

public class Transaction
{
    public decimal total { get; set; }
}

public class NftTransfers
{
    public decimal total { get; set; }
}

public class TokenTransfers
{ 
    public decimal total { get; set; } 
}