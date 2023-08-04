using MarketplaceClientSide.Models.Domain;

namespace MarketplaceClientSide.ViewModels
{
    public class IndexViewModel
    {
        public IEnumerable<Auction> Auctions { get; }
        public SortViewModel SortViewModel { get; }
        public FilterViewModel FilterViewModel { get; }
        public PageViewModel PageViewModel { get; }

        public IndexViewModel(IEnumerable<Auction> Auctions, PageViewModel pageViewModel, FilterViewModel filterViewModel, SortViewModel sortViewModel)
        {
            this.Auctions = Auctions;
            this.PageViewModel = pageViewModel;
            this.SortViewModel = sortViewModel;
            this.FilterViewModel = filterViewModel;
        }
    }
}
