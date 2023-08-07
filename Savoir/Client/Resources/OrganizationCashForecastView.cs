namespace Savoir.Client.Resources
{
    public class OrganizationCashForecastView
    {
        public string OrganizationName { get; set; }
        public string BankAccount { get; set; }
        public string Currency { get; set; }
        public decimal NetCashFlow { get; set; }
        public List<CompanyWise> CompanyWise { get; set; }
    }
    public class CompanyWise
    {
        public int OrganizationId { get; set; }
        public string OrganiaztionName { get; set; }
        public int CompanyId { get; set; }
        public string CompanyName { get; set; }
        public string BankAccount { get; set; }
        public decimal NetCashFlow { get; set; }
        public int currencyId { get; set; }
        public string Currency { get; set; }
    }
}
