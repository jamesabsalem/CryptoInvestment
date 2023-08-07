using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Savoir.Models.Models
{
    public class ReturnRate
    {
        [Key]
        public int LendingPlatformId { get; set; }
        public int ReturnRateObjectId { get; set; }
        public string Currency { get; set; }
        public decimal Rate { get; set; }
    }
}
