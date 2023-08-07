using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace Savoir.Models
{
    [Keyless]
    public class OpeningBalance
    {
        public int OrganizationId { get; set; }
        public int CompanyId { get; set; }
        public int Month { get; set; }
        public int Year { get; set; }
        public decimal? Balance { get; set; }
        public int Currency { get; set; }
    }
}
