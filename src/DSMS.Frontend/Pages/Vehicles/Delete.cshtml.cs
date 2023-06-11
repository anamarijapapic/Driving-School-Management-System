#nullable disable

using DSMS.DataAccess.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace DSMS.Frontend.Pages.Vehicles
{
    public class DeleteModel : PageModel
    {
        private readonly IVehicleRepository _vehicleRepository;

        public DeleteModel(IVehicleRepository vehicleRepository)
        {
            _vehicleRepository = vehicleRepository;
        }

        public string Description { get; set; }

        public async Task<IActionResult> OnGetAsync(string Id)
        {
            var result = await _vehicleRepository.GetAllAsync(v => v.Id.ToString() == Id);
            var vehicle = result.First();
            if (vehicle == null)
            {
                return base.NotFound($"Unable to load vehicle with ID '{Id}'.");
            }

            Description = vehicle.Description;

            return Page();
        }

        public async Task<IActionResult> OnPostAsync(string Id)
        {
            var result = await _vehicleRepository.GetAllAsync(v => v.Id.ToString() == Id);
            var vehicle = result.First();
            if (vehicle == null)
            {
                return base.NotFound($"Unable to load vehicle with ID '{Id}'.");
            }

            await _vehicleRepository.DeleteAsync(vehicle);

            return Redirect("~/Vehicles/Index");
        }
    }
}
