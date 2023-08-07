using Microsoft.AspNetCore.Components;
using Savoir.Client.Resources;
using Savoir.Client.Services;

namespace Savoir.Client.Components
{
    public partial class CashFlowTab
    {
        [Inject] public IHomeService HomeService { get; set; }
        [Inject] NavigationManager _navigationManager { get; set; }
        [Inject] private ILocalStorageService _localStorageService { get; set; }
        [Inject] private IAuthService AuthService { get; set; }
        private OrganizationCashForecastView _cashFlows;
        private string _confirmKeySelection = "confirmSelection";

        private bool _status = false;
        private int _companyId;
        protected override async Task OnInitializedAsync()
        {
            var user =await AuthService.GetUser();
            _cashFlows = (await HomeService.GetOrganizationCashForecast(user.UserId));
        }
        private void OnClickArrow(int companyId)
        {
            _status = !_status;
            _companyId = companyId; 
        }
    }
}
