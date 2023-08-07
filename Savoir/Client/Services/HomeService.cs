using System.Net.Http.Json;
using Microsoft.AspNetCore.Components;
using Savoir.Client.Resources;

namespace Savoir.Client.Services
{
    public class HomeService: IHomeService
    {
        private readonly HttpClient _httpClient;
        private ILocalStorageService _localStorageService;
        private const string ControllerName = "api/Home";
        public HomeService(HttpClient httpClient, ILocalStorageService localStorageService)
        {
            _httpClient = httpClient;
            _localStorageService = localStorageService;
        }

        public async Task<IEnumerable<CashFlow>> GetCashFlow()
        {
            return await _httpClient.GetFromJsonAsync<IEnumerable<CashFlow>>($"{ControllerName}/GetCashFlow");
        }
        public async Task<IEnumerable<InvestmentView>> GetInvestment(int userId)
        {
            return await _httpClient.GetFromJsonAsync<IEnumerable<InvestmentView>>($"{ControllerName}/GetInvestment?UserId={userId}");
        }
        public async Task<IEnumerable<MarketsOverView>> GetMarketsOverView()
        {
            return await _httpClient.GetFromJsonAsync<IEnumerable<MarketsOverView>>($"{ControllerName}/GetMarketsOverView");
        }
        public async Task<IEnumerable<InvestmentView>> GetPastTrades(int userId)
        {
            return await _httpClient.GetFromJsonAsync<IEnumerable<InvestmentView>>($"{ControllerName}/GetPastTrades?UserId={userId}");
        }

        public async Task<IEnumerable<InvestmentTerm>> GetInvestmentTerms()
        {
            return await _httpClient.GetFromJsonAsync<IEnumerable<InvestmentTerm>>($"{ControllerName}/GetInvestmentTerms");
        }
        public async Task<OrganizationCashForecastView> GetOrganizationCashForecast(int userId)
        {
            return await _httpClient.GetFromJsonAsync<OrganizationCashForecastView>($"{ControllerName}/OrganizationCashForecast?UserId={userId}");
        }
        public async Task<IEnumerable<CompanyWiseForecast>> GetCompanyWiseCashForecast(int companyId)
        {
            return await _httpClient.GetFromJsonAsync<IEnumerable<CompanyWiseForecast>>($"{ControllerName}/GetCompanyWiseCashForecast?CompanyId={companyId}");
        }

        public async Task<string> CreateInvestment(InvestmentModel investmentModel)
        {
            var message = "";
            var response = await _httpClient.PostAsJsonAsync($"{ControllerName}/CreateInvestment", investmentModel);
            if (response.IsSuccessStatusCode)
            {
                message = "Save Successfully";
                await _localStorageService.RemoveItem("confirmSelection");
            } else
            {
                message = response.StatusCode.ToString();
            }
            return message;
        }
    }
}
