using Microsoft.AspNetCore.Components;
using Savoir.Client.Resources;
using Savoir.Client.Services;

namespace Savoir.Client.Components
{
    public partial class InvestmentTab
    {
        [Inject] private IHomeService HomeService { get; set; }
        [Inject] private IAuthService AuthService { get; set; }
        private IEnumerable<InvestmentView> Investments = null;
        private IEnumerable<MarketsOverView> MarketsOverView = null;
        private IEnumerable<InvestmentView> PastTrades = null;
       
        [Parameter]
        public int? _terms { get; set; }

        protected override async Task OnInitializedAsync()
        {
            var user = await AuthService.GetUser();
            if(user != null)
            {
                Investments = (await HomeService.GetInvestment(user.UserId)).ToList();
                MarketsOverView = (await HomeService.GetMarketsOverView()).ToList();
                PastTrades = (await HomeService.GetPastTrades(user.UserId)).ToList();
            }
        }
        protected override async Task OnParametersSetAsync()
        {
            MarketsOverView = _terms != null ? (await HomeService.GetMarketsOverView()).Where(x => x.TermId == _terms).ToList() : (await HomeService.GetMarketsOverView()).ToList();
        }
        
    }
}
