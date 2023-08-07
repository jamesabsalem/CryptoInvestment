using Microsoft.AspNetCore.Components;
using Savoir.Client.Resources;
using Savoir.Client.Shared;

namespace Savoir.Client.Components
{
    public partial class CurrentInvestmentsGrid
    {
        [Parameter]
        public IEnumerable<InvestmentView> AllInvestments { get; set; }
        private IEnumerable<InvestmentView> Investments = null;
        // private int pageIndex = 1;
        private int itemsPerPage = 3;
        private int totalPages = 1;
        [CascadingParameter]
        PageIndexStateProvider State { get; set; } = new();
        protected override void OnParametersSet()
        {
            if (AllInvestments != null)
            {
                // Initialize the number of "totalPages"
                totalPages = (int)(AllInvestments.Count() / itemsPerPage);
                // Initialize which will be displayed when the page loaded first time.
                var skipCount = itemsPerPage * (State.PageIndex - 1);
                Investments = AllInvestments.Skip(skipCount).Take(itemsPerPage);
            }
        }
        private void SelectedPage(int selectedPageIndex)
        {
            if (AllInvestments != null)
            {
                State.PageIndex = selectedPageIndex;
                var skipCount = itemsPerPage * (State.PageIndex - 1);
                Investments = AllInvestments.Skip(skipCount).Take(itemsPerPage);
            }
        }
    }
}
