using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace MarketplaceClientSide.Models.Domain
{
    public class Auction
    {
        public int Id { get; set; }
        public int ItemId { get; set; }
        public DateTime CreatedDt { get; set; }
        public DateTime FinishedDt { get; set; }
        public decimal Price { get; set; }
        public MarketStatus Status { get; set; }
        public string? Seller { get; set; }
        public string? Buyer { get; set; }

        //[JsonIgnore]
        public Item? Item { get; set; }
    }
    public enum MarketStatus
    {
        None,
        Canceled,
        Finished,
        Active
    }
}
