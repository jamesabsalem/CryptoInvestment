namespace Savoir.Client.Resources
{
    public class CashFlowView
    {
        public string OrganizationName { get; set; }
        public string BankAccount { get; set; }
        public string Currency { get; set; }
        public decimal NetCashFlow { get; set; }
    }
}
