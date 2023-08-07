using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Savoir.Models
{
    public class LendingPlatform
    {
        [Key]
        public int LendingPlatformId { get; set; }
        public string Name { get; set; }
        public int BlockChainId { get; set; }
    }
}
