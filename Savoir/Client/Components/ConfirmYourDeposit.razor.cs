using Microsoft.AspNetCore.Components;
using Savoir.Client.Resources;
using Savoir.Client.Services;
using System;

namespace Savoir.Client.Components
{
    public partial class ConfirmYourDeposit
    {
        [Inject] public IHomeService HomeService { get; set; }
        [Inject] NavigationManager _navigationManager { get; set; }
        [Inject] private IAuthService _authService { get; set; }
        [Inject] private ILocalStorageService _localStorageService { get; set; }
        private CompanyWise companyWise;
        private MarketsOverView marketsOverView;
        private ConfirmSelection confirmSelection = new();
        private string _confirmKeySelection = "confirmSelection";
        private ConfirmDepositAmount confirmDepositAmount = new();
        private decimal _netCashFlow;
        private async Task OnValidSubmit()
        {
            var data = await _localStorageService.GetItem<ConfirmSelection>(_confirmKeySelection);
            data.DepositPercentage = Convert.ToDecimal(confirmDepositAmount.Percentage);
            data.DepositAmount = Convert.ToDecimal(confirmDepositAmount.Amount);
            await _localStorageService.SetItem(_confirmKeySelection, data);
            _navigationManager.NavigateTo("/ReviewAndConfirm");
        }
        protected override async Task OnInitializedAsync()
        {
            confirmSelection = await _localStorageService.GetItem<ConfirmSelection>(_confirmKeySelection);
            if(confirmSelection.CompanyId != null)
            {
                var user =await _authService.GetUser();
                var data = (await HomeService.GetOrganizationCashForecast(user.UserId));
                companyWise = data.CompanyWise.Where(c => c.CompanyId == confirmSelection.CompanyId).First();
                _netCashFlow = companyWise.NetCashFlow;
                var amount = AmountCalculation(_netCashFlow, Convert.ToDecimal(confirmDepositAmount.Percentage));
                confirmDepositAmount.Amount = amount.ToString();
            }
            if (confirmSelection.PlatformId != null)
            {
                var data = (await HomeService.GetMarketsOverView());
                marketsOverView = data.Where(m => m.LendingPlatformId == confirmSelection.PlatformId && m.TermId==confirmSelection.TermId).First();
            }
        }
        private decimal AmountCalculation(decimal netCashFlow, decimal depositPercentage) => Math.Round((netCashFlow * depositPercentage) / 100,2);
        private decimal PercentageCalculation(decimal netCashFlow, decimal equivalentAmount) => Math.Round((equivalentAmount * 100) / netCashFlow);
        private void OnChangeDeposit(ChangeEventArgs e)
        {
            if(!string.IsNullOrEmpty(e.Value?.ToString()))
            {
                var percentage = Convert.ToDecimal(e.Value);
                var amount = AmountCalculation(_netCashFlow, percentage);
                confirmDepositAmount.Percentage = percentage.ToString();
                confirmDepositAmount.Amount = amount.ToString();
            }  
        }
        private void OnChangeAmount(ChangeEventArgs e)
        {
            if (!string.IsNullOrEmpty(e.Value?.ToString()))
            {
                var amount = Convert.ToDecimal(e.Value);
                var percentage = PercentageCalculation(_netCashFlow, amount);
                confirmDepositAmount.Amount = amount.ToString();
                confirmDepositAmount.Percentage = percentage.ToString();
            }
        }
        protected void OnClickCompanyView()
        {
            _navigationManager.NavigateTo("/SelectAccount");
        }
        protected void OnClickMarkeView()
        {
            _navigationManager.NavigateTo("/");
        }
    }
}
