using Microsoft.AspNetCore.Components;
using Savoir.Client.Resources;
using Savoir.Client.Shared;

namespace Savoir.Client.Components
{
    public partial class PastTradesGrid
    {
        [Parameter]
        public IEnumerable<InvestmentView> AllPastTrades { get; set; }
        public IEnumerable<InvestmentView> PastTrades = null;
        // private int pageIndex = 1;
        private int itemsPerPage = 5;
        private int totalPages = 1;
        [CascadingParameter]
        PageIndexStateProvider State { get; set; } = new();
        protected override void OnParametersSet()
        {
            if (AllPastTrades != null)
            {
                // Initialize the number of "totalPages"
                totalPages = (int)(AllPastTrades.Count() / itemsPerPage);
                // Initialize which will be displayed when the page loaded first time.
                var skipCount = itemsPerPage * (State.PageIndex - 1);
                PastTrades = AllPastTrades.Skip(skipCount).Take(itemsPerPage);
            }
        }
        private void SelectedPage(int selectedPageIndex)
        {
            if (AllPastTrades != null)
            {
                State.PageIndex = selectedPageIndex;
                var skipCount = itemsPerPage * (State.PageIndex - 1);
                PastTrades = AllPastTrades.Skip(skipCount).Take(itemsPerPage);
            }
        }
    }
}
