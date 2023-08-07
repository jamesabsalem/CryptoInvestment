namespace Savoir.Client.Resources
{
    public class CompanyWiseForecast
    {
        public int Serial { get; set; }
        public string Reason { get; set; }
        public List<Expence> Expence { get; set; }
    }
    public class Expence
    {
        public int Year { get; set; }
        public int Month { get; set; }
        public string MonthYear { get; set; }
        public decimal? Amount { get; set; }
    }
}
