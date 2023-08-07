using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Savoir.Models.Models
{
    public class LendingPlatformRate
    {
        [Key]
        public int LendingPlatformRateId { get; set; }

        public DateTime Date { get; set; }

        public int LendingPlatformId { get; set; }
        public string InvestmentCurrencyName { get; set; }
        public int TermId { get; set; }

        public string ReturnCurrencyName1 { get; set; }
        public decimal ReturnCurrencyRate1 { get; set; }
        public string ReturnCurrencyName2 { get; set; }
        public decimal ReturnCurrencyRate2 { get; set; }
        public string ReturnCurrencyName3 { get; set; }
        public decimal ReturnCurrencyRate3 { get; set; }
        public string ReturnCurrencyName4 { get; set; }
        public decimal ReturnCurrencyRate4 { get; set; }
        public char LendorBorrow { get; set; }

    }
}
