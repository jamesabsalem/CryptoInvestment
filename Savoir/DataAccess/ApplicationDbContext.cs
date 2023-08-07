
using Microsoft.EntityFrameworkCore;
using Savoir.Models;
using Savoir.Models.Models;

namespace Savoir.DataAccess
{
    public class ApplicationDbContext : DbContext
    {

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<CashFlow> CashFlows { get; set; }
        public DbSet<Company> Companies { get; set; }
        public DbSet<FinancialActivity> FinancialActivities { get; set; }
        public DbSet<Investment> Investments { get; set; }
        public DbSet<InvestmentActivity> InvestmentActivities { get; set; }
        public DbSet<InvestmentHistory> InvestmentHistories { get; set; }
        public DbSet<LendingPlatform> LendingPlatforms { get; set; }
        public DbSet<LendingPlatformLiquidationStatsHistory> LendingPlatformLiquidationStatsHistories { get; set; }
        public DbSet<LendingPlatformRate> LendingPlatformRates { get; set; }
        public DbSet<OperatingActivity> OperatingActivities { get; set; }
        public DbSet<Organization> Organizations { get; set; }
        public DbSet<ReturnRate> ReturnRates { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<UserRole> UserRoles { get; set; }
        public DbSet<InvestmentTerm> InvestmentTerms { get; set; }
        public DbSet<Currency> Currency { get; set; }
        public DbSet<OpeningBalance> OpeningBalance { get; set; }

    }
}
