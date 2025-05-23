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

    public DbSet<Token> Tokens { get; set; }

    public DbSet<Portfolio> Portfolios { get; set; }
    
    
     protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Portfolio>()
            .Property(p => p.TotalValueUsd)
            .HasColumnType("decimal(18,6)");  // default is (18,2)
    }
   
}