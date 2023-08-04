﻿using System.ComponentModel.DataAnnotations;

namespace MarketPlaceAPI.Models
{
    public class Item
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Name is required.")]
        [MaxLength(100, ErrorMessage = "Name cannot exceed 100 characters.")]
        public string? Name { get; set; }
        public string? Description { get; set; }
        public string? Metadata { get; set; }
        public List<Auction>? Auctions { get; set; }
    }
}
