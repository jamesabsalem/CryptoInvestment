using Microsoft.AspNetCore.Components;
using Savoir.Client.Resources;
using Savoir.Client.Services;

namespace Savoir.Client.Components
{
    public partial class CompanyWiseCashForecastComponent
    {
        [Inject] public IHomeService HomeService { get; set; }
        private List<CompanyWiseForecast> _cashForecast;
        List<Expence> Expence=new List<Expence>();
        [Parameter]
        public int _companyId { get; set; }
        protected override async Task OnInitializedAsync()
        {
            _cashForecast = (await HomeService.GetCompanyWiseCashForecast(2)).ToList();
        }
        protected override async Task OnParametersSetAsync()
        {
            _cashForecast = (await HomeService.GetCompanyWiseCashForecast(_companyId)).ToList();
            foreach (var cashForecast in _cashForecast)
            {
                foreach (var item in cashForecast.Expence)
                {
                    Expence.Add(item);
                }
            }
        }
    }
}
