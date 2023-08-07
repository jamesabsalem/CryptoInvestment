using Microsoft.AspNetCore.Components;
using Savoir.Client.Resources;
using Savoir.Client.Services;

namespace Savoir.Client.Components
{
    public partial class SelectAccount
    {
        [Inject] public IHomeService HomeService { get; set; }
        [Inject] NavigationManager _navigationManager { get; set; }
        [Inject] private IAuthService _authService { get; set; }
        [Inject] private ILocalStorageService _localStorageService { get; set; }
        private OrganizationCashForecastView _cashFlows;
        private string _confirmKeySelection = "confirmSelection";

        private bool _status = false;
        private int _companyId;
        protected override async Task OnInitializedAsync()
        {
            var user = await _authService.GetUser();
            _cashFlows = (await HomeService.GetOrganizationCashForecast(user.UserId));
        }
        private void OnClickArrow(int companyId)
        {
            _status = !_status;
            _companyId = companyId; 
        }
        private async void OnClickSelect(int companyId)
        {
            var date =await _localStorageService.GetItem<ConfirmSelection>(_confirmKeySelection);
            if (date != null)
            {
                date.CompanyId = companyId;
                await _localStorageService.SetItem(_confirmKeySelection, date);
            }
            else
            {
                var confirmSelect = new ConfirmSelection
                {
                    CompanyId = companyId
                };
                await _localStorageService.SetItem(_confirmKeySelection, confirmSelect);
            }
            _navigationManager.NavigateTo($"/Confirm");
        }
    }
}
