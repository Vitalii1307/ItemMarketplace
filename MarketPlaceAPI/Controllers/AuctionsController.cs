using MarketPlaceAPI.Data;
using MarketPlaceAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;
using System.Text.Json;
using System;

namespace MarketPlaceAPI.Controllers
{
    [ApiController]
    [Route("api/v0.1/auctions")]
    public class AuctionsController : ControllerBase
    {
        private readonly MarketplaceAPIDbContext dbContext;

        public AuctionsController(MarketplaceAPIDbContext dbContext)
        {
            this.dbContext = dbContext;
        }


        [HttpGet("{id}")]
        public async Task<IActionResult> GetAuction(int id)
        {
            var auction = await dbContext.Auctions
                .Include(a => a.Item) // Include the related Item entity
                .FirstOrDefaultAsync(a => a.Id == id); // Retrieve the auction by its ID

            if (auction == null)
            {
                return NotFound();
            }

            var options = new JsonSerializerOptions
            {
                ReferenceHandler = ReferenceHandler.Preserve
            };

            // Serialize the list of auctions to JSON with the configured options
            var json = JsonSerializer.Serialize(auction, options);

            // Return the serialized JSON
            return Content(json, "application/json");
        }

        // GET api/v0.1/auctions
        [HttpGet]
        public IActionResult GetAuctions(
        [FromQuery] string? itemName = null,
        [FromQuery] MarketStatus? status = null,
        [FromQuery] string? sort_order = "asc",
        [FromQuery] string? sort_key = "id",
        [FromQuery] int? limit = 100)
        {
            var query = dbContext.Auctions
                .Include(a => a.Item) // Include the related Item entity
                .AsQueryable();

            if (!string.IsNullOrEmpty(itemName))
            {
                query = query.Where(a => a.Item.Name == itemName);
            }

            if (status.HasValue)
            {
                query = query.Where(a => a.Status == status.Value);
            }

            if (sort_key == "price")
            {
                if (sort_order.ToLower() == "desc")
                {
                    query = query.OrderByDescending(a => a.Price);
                }
                else
                {
                    query = query.OrderBy(a => a.Price);
                }
            }
            else if (sort_key == "createddt")
            {
                if (sort_order.ToLower() == "desc")
                {
                    query = query.OrderByDescending(a => a.CreatedDt);
                }
                else
                {
                    query = query.OrderBy(a => a.CreatedDt);
                }
            }
            else
            {
                // We sort by default by auction ID
                if (sort_order.ToLower() == "desc")
                {
                    query = query.OrderByDescending(a => a.Id);
                }
                else
                {
                    query = query.OrderBy(a => a.Id);
                }
            }

            var auctions = query.Take(limit ?? 100).ToList();
            var options = new JsonSerializerOptions
            {
                ReferenceHandler = ReferenceHandler.Preserve
            };

            // Serialize the list of auctions to JSON with the configured options
            var json = JsonSerializer.Serialize(auctions, options);

            // Return the serialized JSON
            return Content(json, "application/json");
        }


        // POST: api/v0.1/auctions
        [HttpPost]
        public async Task<IActionResult> AddAuction([FromBody] Auction auction)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }            
            dbContext.Auctions.Add(auction);
            await dbContext.SaveChangesAsync();

            return Ok(auction);
        }
    }
}
