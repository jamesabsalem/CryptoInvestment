using Microsoft.AspNetCore.Components;
using Savoir.Client.Resources;
using Savoir.Client.Services;
using Savoir.Client.Shared;

namespace Savoir.Client.Components
{
    public partial class MarketOverViewGrid
    {
        [Parameter]
        public IEnumerable<MarketsOverView> AllMarketsOverView { get; set; }
        public IEnumerable<MarketsOverView> MarketsOverView = null;
        [Inject] private ILocalStorageService _localStorageService { get; set; }
        [Inject] NavigationManager NavigationManager { get; set; }
        private string _confirmKeySelection = "confirmSelection";
        // private int pageIndex = 1;
        private int itemsPerPage = 3;
        private int totalPages = 1;
        [CascadingParameter]
        PageIndexStateProvider State { get; set; } = new();
        protected override void OnParametersSet()
        {
            if (AllMarketsOverView != null)
            {
                itemsPerPage = 3;
                totalPages = 1;
                State = new();
                // Initialize the number of "totalPages"
                totalPages = (int)(AllMarketsOverView.Count() / itemsPerPage);
                // Initialize which will be displayed when the page loaded first time.
                var skipCount = itemsPerPage * (State.PageIndex - 1);
                MarketsOverView = AllMarketsOverView.Skip(skipCount).Take(itemsPerPage);
            }
        }
        private async Task OnClickDiposit(int LendingPlatformId, int TermId)
        {
            var data = await _localStorageService.GetItem<ConfirmSelection>(_confirmKeySelection);
            if (data != null)
            {
                data.PlatformId = LendingPlatformId;
                data.TermId = TermId;
                await _localStorageService.SetItem(_confirmKeySelection, data);
            }
            else
            {
                var confirmSelect = new ConfirmSelection
                {
                    TermId = TermId,
                    PlatformId = LendingPlatformId
                };
                await _localStorageService.SetItem(_confirmKeySelection, confirmSelect);
            }
            NavigationManager.NavigateTo($"/SelectAccount");
        }
        private void SelectedPage(int selectedPageIndex)
        {
            if (AllMarketsOverView != null)
            {
                State.PageIndex = selectedPageIndex;
                var skipCount = itemsPerPage * (State.PageIndex - 1);
                MarketsOverView = AllMarketsOverView.Skip(skipCount).Take(itemsPerPage);
            }
        }
    }
}
