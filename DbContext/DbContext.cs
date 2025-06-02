using KaiCryptoTracker.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using KaiCryptoTracker.Models;
namespace KaiCryptoTracker.DbContext;

//db context class
public class ApplicationDbContext : IdentityDbContext<ApplicationUser, ApplicationRole, Guid>
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base
    (options)
    { }


    public DbSet<Chain> Chains { get; set; }

    public DbSet<Wallet> Wallets { get; set; }

    public DbSet<TokenMetadata> Tokens { get; set; }

    public DbSet<Portfolio> Portfolios { get; set; }

    public DbSet<Coins> Coins { get; set; }


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Portfolio>()

            .Property(p => p.TotalValueUsd)
            .HasColumnType("decimal(18,6)");  //set precision value

        modelBuilder.Entity<TokenHolding>()
            .HasOne(w => w.Wallet)  //has one wallet
            .WithMany(w => w.tokenHoldings) //wallet has many token holdings
            .HasForeignKey(w => w.WalletId)
            .OnDelete(DeleteBehavior.Cascade); //when wallet is deleted , delete token holdings

        modelBuilder.Entity<TokenHolding>()
         .HasOne(tmd => tmd.TokenMetadata) //has one token meta data 
         .WithMany(tmd => tmd.TokenHoldings) //token meta data has many tokenholdings
         .HasForeignKey(tmd => tmd.TokenMetadataId)
         .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Wallet>()
         .HasOne(w => w.User) //wallet is connected to one user 
         .WithMany(u => u.Wallets) //user can be connected to many wallets
         .HasForeignKey(u => u.UserId)
         .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<Wallet>()
        .HasOne(w => w.Portfolio)
        .WithMany(p => p.Wallets)
        .HasForeignKey(w => w.PortfolioId)
        .OnDelete(DeleteBehavior.Restrict);


    }


}