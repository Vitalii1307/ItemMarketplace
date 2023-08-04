using MarketPlaceAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace MarketPlaceAPI.Data
{
    public class MarketplaceAPIDbContext : DbContext
    {
        public MarketplaceAPIDbContext(DbContextOptions option) : base(option){}

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Auction>()
                .HasOne(a => a.Item)
                .WithMany(i => i.Auctions)
                .HasForeignKey(a => a.ItemId);
        }

        public DbSet<Auction> Auctions { get; set; }
        public DbSet<Item> Items { get; set; }
    }
}
