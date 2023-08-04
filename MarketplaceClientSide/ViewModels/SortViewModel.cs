using MarketplaceClientSide.Models.Domain;

namespace MarketplaceClientSide.ViewModels
{
    public class SortViewModel
    {
        public SortState CreatedDtSort { get; }
        public SortState PriceSort { get; }
        public SortState Current { get; }

        public SortViewModel(SortState sortOrder)
        {
            CreatedDtSort = sortOrder == SortState.CreatedDtAsc ? SortState.CreatedDtDesc : SortState.CreatedDtAsc;
            PriceSort = sortOrder == SortState.PriceAsc ? SortState.PriceDesc : SortState.PriceAsc;
            Current = sortOrder;
        }
    }
}
