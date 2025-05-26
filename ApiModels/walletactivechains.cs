namespace KaiCryptoTracker.ApiModels;

public class Activechains
{
    public string address { get; set; } = string.Empty;
    public List<Chains> active_chains = new List<Chains>();

}

public class Chains
{
    public string chain { get; set; } = string.Empty;
    public string chain_id { get; set; } = string.Empty;
    public TransactionInfo? first_transaction { get; set; }
    public TransactionInfo? last_transaction { get; set; }

}

public class TransactionInfo
{
    public string block_number { get; set; } = string.Empty;
    public string block_timestamp { get; set; } = string.Empty;

    public string transaction_hash { get; set; } = string.Empty;
}