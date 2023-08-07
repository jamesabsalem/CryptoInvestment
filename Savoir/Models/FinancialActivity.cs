using System.ComponentModel.DataAnnotations;

namespace Savoir.Models.Models
{
    public class FinancialActivity
    {
        [Key]
        public int FinancialActivityId { get; set; }
        public int OrganizationId { get; set; }
        public int CompanyId { get; set; }
        public int Month { get; set; }
        public int Year { get; set; }
        public int LoanProceeds { get; set; }
        public int RepaymentOfDebentures { get; set; }
        public int NetCashFromFinancialActivities { get; set; }
    }
}
