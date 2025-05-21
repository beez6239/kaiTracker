namespace KaiCryptoTracker.Models;
public class Chain
{

    public Guid ChainId { get; set; }
    public string ChainName { get; set; } = string.Empty;
    public List<TokenDetails> TokenDetails { get; set; } = new List<TokenDetails>();
}