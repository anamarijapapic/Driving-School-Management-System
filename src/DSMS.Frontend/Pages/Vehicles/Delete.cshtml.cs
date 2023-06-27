#nullable disable

using DSMS.Application.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace DSMS.Frontend.Pages.Vehicles
{
    public class DeleteModel : PageModel
    {
        private readonly IVehicleService _vehicleService;

        public DeleteModel(IVehicleService vehicleService)
        {
            _vehicleService = vehicleService;
        }

        public string Description { get; set; }

        public async Task<IActionResult> OnGetAsync(string id)
        {
            var vehicle = await _vehicleService.GetByIdAsync(id);
            if (vehicle == null)
            {
                return base.BadRequest($"Unable to load vehicle with ID '{id}'.");
            }

            Description = vehicle.Description;

            return Page();
        }

        public async Task<IActionResult> OnPostAsync(string id)
        {
            var vehicle = await _vehicleService.GetByIdAsync(id);
            if (vehicle == null)
            {
                return base.BadRequest($"Unable to load vehicle with ID '{id}'.");
            }

            await _vehicleService.DeleteAsync(vehicle);

            return Redirect("~/Vehicles/Index");
        }
    }
}
