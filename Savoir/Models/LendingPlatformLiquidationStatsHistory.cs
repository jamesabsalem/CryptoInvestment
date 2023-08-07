using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Savoir.Models.Models
{
    public class LendingPlatformLiquidationStatsHistory
    {
        [Key]
        public int LendingPlatformId { get; set; }
        public DateTime Date { get; set; }
        public int TotalMarketSize { get; set; }
        public int TotalBorrow { get; set; }
        public int LentOut { get; set; }

    }
}
