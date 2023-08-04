using MarketPlaceAPI.Data;
using MarketPlaceAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace MarketPlaceAPI.Controllers
{
    [ApiController]
    [Route("api/v0.1/items")]
    public class ItemsController : ControllerBase
    {
        private readonly MarketplaceAPIDbContext dbContext;

        public ItemsController(MarketplaceAPIDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        // GET: api/v0.1/items
        [HttpGet]
        public async Task<IActionResult> GetItems()
        {
            var items = await dbContext.Items.ToListAsync();
            return Ok(items);
        }

        // GET: api/v0.1/items/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetItem(int id)
        {
            var item = await dbContext.Items.FindAsync(id);
            if (item == null)
            {
                return NotFound();
            }
            return Ok(item);
        }

        // POST: api/v0.1/items
        [HttpPost]
        public async Task<IActionResult> AddItem([FromBody] Item item)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // Add the item to the database using Entity Framework
            dbContext.Items.Add(item);
            await dbContext.SaveChangesAsync();

            // Return the newly created item with its generated ID
            return Ok(item);
        }
    }
}
