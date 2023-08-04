using System.ComponentModel.DataAnnotations;

namespace MarketPlaceAPI.Models
{
    public class Auction
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Item ID is required.")]
        public int ItemId { get; set; }

        [Required(ErrorMessage = "Created Date is required.")]
        public DateTime CreatedDt { get; set; }

        [Required(ErrorMessage = "Finished Date is required.")]
        public DateTime FinishedDt { get; set; }

        [Required(ErrorMessage = "Price is required.")]
        [Range(0, double.MaxValue, ErrorMessage = "Price must be greater than or equal to 0.")]
        public decimal Price { get; set; }

        [Required(ErrorMessage = "Status is required.")]
        public MarketStatus Status { get; set; }
        public string? Seller { get; set; }
        public string? Buyer { get; set; }

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
