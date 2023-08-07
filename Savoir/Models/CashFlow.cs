using System.ComponentModel.DataAnnotations;

namespace Savoir.Models
{
    public class CashFlow
    {
        [Key]
        public int CashFlowId { get; set; }
        public int OrganizationId { get; set; }
        public int CompanyId { get; set; }
        public int Month { get; set; }
        public int Year { get; set; }
        public decimal NetIncome { get; set; }
        public decimal NetCashFlow { get; set; }
        public decimal OperatingBalanceInFiat { get; set; }
        public string Currency { get; set; }
        public decimal CashEndPeriod { get; set; }
        public decimal CashEndPeriodInUSD { get; set; }
    }
}
