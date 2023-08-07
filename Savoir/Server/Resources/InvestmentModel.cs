namespace Savoir.Server.Resources
{
    public class InvestmentModel
    {
        public int OrganizationId { get; set; }
        public int CompanyId { get; set; }
        public int LendingPlatformId { get; set; }
        public int LendingPlatformRateId { get; set; }
        public int InvestmentTermId { get; set; }
        public decimal Amount { get; set; }
        public string Currency { get; set; }
    }
}
