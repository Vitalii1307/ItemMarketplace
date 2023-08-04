using MarketplaceClientSide.Data;
using Microsoft.AspNetCore.Mvc;
using MarketplaceClientSide.Models.Domain;
using Microsoft.Extensions.FileSystemGlobbing;
using MarketplaceClientSide.ViewModels;

namespace MarketplaceClientSide.Controllers
{
    public class MarketplaceController : Controller
    {
        APIConnection apiConnection;
        public MarketplaceController()
        {
            apiConnection = new APIConnection("https://localhost:7156/api/v0.1");
        }

        [HttpGet]
        public async Task<IActionResult> Index(string name, int status = 0, int page = 1, SortState sortOrder = SortState.CreatedDtAsc)
        {
            int pageSize = 10;

            string statusStr = "";

            List<Auction>? actions = await apiConnection.GetAuctions("auctions");

            if (status != 0)
            {
                statusStr = $"status={status - 1}";
            }


            switch (sortOrder)
            {
                case SortState.CreatedDtAsc:
                    actions = await apiConnection.GetAuctions("auctions?" + statusStr + "&sort_order=asc&sort_key=createddt");
                    break;
                case SortState.CreatedDtDesc:
                    actions = await apiConnection.GetAuctions("auctions?" + statusStr + "&sort_order=desc&sort_key=createddt");
                    break;
                case SortState.PriceAsc:
                    actions = await apiConnection.GetAuctions("auctions?" + statusStr + "&sort_order=asc&sort_key=price");
                    break;
                case SortState.PriceDesc:
                    actions = await apiConnection.GetAuctions("auctions?" + statusStr + "&sort_order=desc&sort_key=price");
                    break;
            }

            if (!string.IsNullOrEmpty(name))
            {
                actions = actions.Where(p => p.Seller != null && p.Seller.ToLower().Contains(name.ToLower())).ToList();
            }

            var count = actions.Count();
            var item = actions.Skip((page - 1) * pageSize).Take(pageSize).ToList();

            IndexViewModel viewModel = new IndexViewModel
                (
                item,
                new PageViewModel(count, page, pageSize),
                new FilterViewModel(status, name),
                new SortViewModel(sortOrder)
                );

            return View(viewModel);
        }
    }
}
