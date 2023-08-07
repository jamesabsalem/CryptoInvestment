using System.ComponentModel.DataAnnotations;

namespace Savoir.Models.Models
{
    public class OperatingActivity
    {
        [Key]
        public int OperationActivityId { get; set; }
        public int OrganizationId { get; set; }
        public int CompanyId { get; set; }
        public int Month { get; set; }
        public int Year { get; set; }
        public decimal AccountReceivables { get; set; }
        public decimal AccountPayables { get; set; }
        public decimal PrepaidExpenses { get; set; }
        public decimal DepreciationAmortisation { get; set; }
        public decimal NetCashflowFromOA { get; set; }

    }
}
