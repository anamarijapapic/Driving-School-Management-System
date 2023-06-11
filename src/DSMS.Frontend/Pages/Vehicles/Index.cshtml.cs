#nullable disable

using DSMS.Application.Models;
using DSMS.Application.Models.Vehicle;
using DSMS.Application.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace DSMS.Frontend.Pages.Vehicles
{
    [Authorize(Roles = ("Administrator"))]
    public class IndexModel : PageModel
    {
        private readonly IVehicleService _vehicleService;

        public PaginatedList<VehicleResponseModel> Vehicles { get; set; }

        public IndexModel(IVehicleService vehicleService)
        {
            _vehicleService = vehicleService;
        }

        public async Task<PageResult> OnGetAsync(string searchString, string currentFilter, int? pageIndex)
        {
            if (searchString != null)
            {
                pageIndex = 1;
            }

            var pageSize = 5;
            var vehicles = await _vehicleService.GetAllAsync();

            ViewData["Keyword"] = searchString;
            vehicles = _vehicleService.Search(vehicles, searchString);

            ViewData["CurrentFilter"] = currentFilter;
            vehicles = _vehicleService.Filter(vehicles, currentFilter);

            Vehicles = PaginatedList<VehicleResponseModel>.Create(vehicles, pageIndex ?? 1, pageSize);

            return Page();
        }
    }
}
