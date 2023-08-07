using System.ComponentModel.DataAnnotations;

namespace Savoir.Client.Resources
{
    public class InvestmentView
    {
        public string Exchange { get; set; }
        public decimal DepositRate { get; set; }
        public string Term { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public DateTime MaturityDate { get; set; }
        public decimal InvestedAmount { get; set; }
        public decimal ReturnAmount { get; set; }
    }
}
