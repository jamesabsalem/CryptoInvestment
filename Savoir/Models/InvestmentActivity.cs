using System.ComponentModel.DataAnnotations;

namespace Savoir.Models.Models
{
    public class InvestmentActivity
    {
        [Key]
        public int InvestmentActivityId { get; set; }
        public int OrganizationId { get; set; }
        public int CompanyId { get; set; }
        public int Month { get; set; }
        public int Year { get; set; }
        public int AssetAcquisition { get; set; }
        public int AssetDisposal { get; set; }
        public decimal AccumulatedDepreciation { get; set; }
        public decimal NetCashfromInvestmentActivities { get; set; }
    }
}
