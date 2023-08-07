using System.ComponentModel.DataAnnotations;

namespace Savoir.Models
{
    public class InvestmentTerm
    {
        [Key]
        public int TermId { get; set; }
        public int Term { get; set; }
        public string InvestmentTermDescription { get; set; }
    }
}
