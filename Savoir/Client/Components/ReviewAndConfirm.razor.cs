using Blazored.Toast.Services;
using Microsoft.AspNetCore.Components;
using Savoir.Client.Resources;
using Savoir.Client.Services;

namespace Savoir.Client.Components
{
    public partial class ReviewAndConfirm
    {
        [Inject] private IToastService _toastService { get; set; }
        [Inject] public IHomeService HomeService { get; set; }
        [Inject] NavigationManager _navigationManager { get; set; }
        [Inject] private IAuthService _authService { get; set; }
        [Inject] private ILocalStorageService _localStorageService { get; set; }
        private CompanyWise companyWise;
        private MarketsOverView marketsOverView;
        private ConfirmSelection confirmSelection = new();
        private string _confirmKeySelection = "confirmSelection";
        private string _terms;

        protected override async Task OnInitializedAsync()
        {
            confirmSelection = await _localStorageService.GetItem<ConfirmSelection>(_confirmKeySelection);
            if (confirmSelection.CompanyId != null)
            {
                var user = await _authService.GetUser(); 
                var data = (await HomeService.GetOrganizationCashForecast(user.UserId));
                companyWise = data.CompanyWise.Where(c => c.CompanyId == confirmSelection.CompanyId).First();
            }
            if (confirmSelection.PlatformId != null)
            {
                var data = (await HomeService.GetMarketsOverView());
                marketsOverView = data.Where(m => m.LendingPlatformId == confirmSelection.PlatformId && m.TermId==confirmSelection.TermId).First();
                _terms = marketsOverView.Term;
            }
        }
        private void OnClickEdit()
        {
            _navigationManager.NavigateTo("/Confirm");
        }
        private async Task OnClickConfirm()
        {
            var investmentModel = new InvestmentModel()
            {
                OrganizationId = companyWise.OrganizationId,
                CompanyId = companyWise.CompanyId,
                LendingPlatformId = marketsOverView.LendingPlatformId,
                LendingPlatformRateId = marketsOverView.lendingPlatformRateId,
                InvestmentTermId = (int)confirmSelection.TermId,
                Amount =(decimal) confirmSelection.DepositAmount,
                Currency = companyWise.Currency
            };
            var message = await HomeService.CreateInvestment(investmentModel);
            _navigationManager.NavigateTo("/");
            _toastService.ShowInfo(message);
        }
    }
}
