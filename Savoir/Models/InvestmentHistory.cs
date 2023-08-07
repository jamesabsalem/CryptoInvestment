
using System.ComponentModel.DataAnnotations;

namespace Savoir.Models.Models
{
    public class InvestmentHistory
    {
        [Key]
        public int InvestmentHistId { get; set; }
        public int InvestmentId { get; set; }
        public int OrganizationId { get; set; }
        public int CompanyId { get; set; }
        public int LendingPlatformId { get; set; }
        public int LendingRateId { get; set; }
        public DateTime StartingDate { get; set; }
        public DateTime MaturityDate { get; set; }
        public decimal Amount { get; set; }
        public string Currency { get; set; }
        public decimal ReturnAmount { get; set; }
        public DateTime ActualMaturityDate { get; set; }
        public string Reason { get; set; }
        public bool Status { get; set; }
        public string SpecialNote { get; set; }
    }
}
