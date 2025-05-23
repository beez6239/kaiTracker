namespace KaiCryptoTracker.Models;

public class Chain
{
    public Guid ChainId { get; set; }
    public string ChainName { get; set; } = string.Empty;

    //FK
    public Guid PortfolioId { get; set; }
    
    public List<Token> TokenDetails { get; set; } = new List<Token>();
    
    public Portfolio Portfolio { get; set; }
}