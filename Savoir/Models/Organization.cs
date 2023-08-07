using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Savoir.Models.Models
{
    public class Organization
    {
        [Key]
        public int OrganizationId { get; set; }
        public string Name { get; set; }
        public string ABN { get; set; }
        public string ACN { get; set; }
        public string Address { get; set; }
        public string Country { get; set; }
        public string BankBSB { get; set; }
        public string BankAccount { get; set; }
        public string BankSwiftCode { get; set; }
        public int DefaultCurrencyId { get; set; }
        public string WalletId { get; set; }

    }
}
