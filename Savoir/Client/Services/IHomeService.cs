using Savoir.Client.Resources;

namespace Savoir.Client.Services
{
    public interface IHomeService
    {
        Task<IEnumerable<CashFlow>> GetCashFlow();
        Task<IEnumerable<InvestmentView>> GetInvestment(int userId);
        Task<IEnumerable<MarketsOverView>> GetMarketsOverView();
        Task<IEnumerable<InvestmentView>> GetPastTrades(int userId);
        Task<IEnumerable<InvestmentTerm>> GetInvestmentTerms();
        Task<OrganizationCashForecastView> GetOrganizationCashForecast(int userId);
        Task<IEnumerable<CompanyWiseForecast>> GetCompanyWiseCashForecast(int companyId);
        Task<string> CreateInvestment(InvestmentModel investmentModel);
    }
}
