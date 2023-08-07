using System.ComponentModel.DataAnnotations;


namespace Savoir.Models
{
    public class Investment
    {
        [Key]
        public int InvestmentId { get; set; }
        public int OrganizationId { get; set; }
        public int CompanyId { get; set; }
        public int LendingPlatformId { get; set; }
        public int LendingPlatformRateId { get; set; }
        public int InvestmentTermId { get; set; }
        public DateTime StartingDate { get; set; }
        public DateTime MaturityDate { get; set; }
        public DateTime? EndDate { get; set; }
        public decimal Amount { get; set; }
        public string Currency { get; set; }
        public char Status { get; set; }
    }
}
