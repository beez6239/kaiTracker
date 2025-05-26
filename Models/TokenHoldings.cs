using System.ComponentModel.DataAnnotations.Schema;

namespace KaiCryptoTracker.Models;

public class TokenHolding
{
       public Guid TokenHoldingId { get; set; }

       public string ContractAddress { get; set; } = string.Empty;

        [Column(TypeName = "decimal(38, 18)")]
        public decimal Balance { get; set; }

        // public decimal? PriceUsd { get; set; } 

        //fk's
        public Guid WalletId { get; set; }
        public Wallet Wallet { get; set; }

        //fk's
        public Guid TokenMetadataId { get; set; }
        public TokenMetadata TokenMetadata { get; set; }

}