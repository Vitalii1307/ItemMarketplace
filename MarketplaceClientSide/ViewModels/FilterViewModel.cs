using MarketplaceClientSide.Models.Domain;
using Microsoft.AspNetCore.Mvc.Rendering;
namespace MarketplaceClientSide.ViewModels
{
    public class FilterViewModel
    {
        public FilterViewModel(int status, string name)
        {
            var statusDropList = new List<StatusDropList>()
            {
                new StatusDropList{ Id = 0, Status = "All"},
                new StatusDropList{ Id = 1, Status = "None"},
                new StatusDropList{ Id = 2, Status = "Canceled"},
                new StatusDropList{ Id = 3, Status = "Finished"},
                new StatusDropList{ Id = 4, Status = "Active"}
            };

            StatusList = new SelectList(statusDropList, "Id", "Status", status);
            SelectedStatus = status;
            SelectedName = name;
        }
        public SelectList StatusList { get; }
        public int SelectedStatus { get; }
        public string SelectedName { get; }
    }
}
