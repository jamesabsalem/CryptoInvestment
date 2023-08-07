using Microsoft.AspNetCore.Components;
using Savoir.Client.Services;
using Savoir.Client.Resources;

namespace Savoir.Client.Components
{
    public partial class HomeComponent
    {
        [Inject] private IHomeService HomeService { get; set; }
        [Inject] private IAuthService AuthService { get; set; }
        private List<InvestmentTerm> _investmentTerms;
        private int _currentTrade;
        private int? _selectedTerm;


        private List<int> _periodFy;

        protected async override Task OnInitializedAsync()
        {
            _investmentTerms = (await HomeService.GetInvestmentTerms()).ToList();
            _periodFy = Enumerable.Range(DateTime.Now.Year, 5).ToList();
            var User =await AuthService.GetUser();
            _currentTrade = (await HomeService.GetInvestment(User.UserId)).ToList().Count();
        }

        protected void onSelectInvestmentTerm(int termsId)
        {
            this._selectedTerm = termsId;
            
        }
    }
}
