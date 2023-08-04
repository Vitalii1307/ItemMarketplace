namespace MarketplaceClientSide.Models.Domain
{
    public class AuctionContainer
    {
        public List<Auction> Values { get; set; }
    }

    public enum SortState
    {
        CreatedDtAsc,
        CreatedDtDesc,
        PriceAsc,
        PriceDesc,
    }
}
