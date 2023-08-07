using System.ComponentModel.DataAnnotations;

namespace Savoir.Client.Resources
{
    public class ConfirmDepositAmount
    {
        [Required]
        public string Percentage { get; set; } = "20";
        [Required]
        public string Amount { get; set; }
    }
}
