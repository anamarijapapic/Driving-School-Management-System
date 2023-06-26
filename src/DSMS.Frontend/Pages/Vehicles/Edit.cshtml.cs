#nullable disable

using DSMS.Application.Models.User;
using DSMS.Application.Models.Vehicle;
using DSMS.Application.Services;
using DSMS.Core.Entities;
using DSMS.Core.Entities.Identity;
using DSMS.Core.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace DSMS.Frontend.Pages.Vehicles
{
    [Authorize(Roles = ("Administrator"))]
    public class EditModel : PageModel
    {
        private readonly IVehicleService _vehicleService;

        private readonly IUserService _userService;

        private readonly UserManager<ApplicationUser> _userManager;

        public IEnumerable<UserResponseModel> Instructors { get; private set; } = new List<UserResponseModel>();

        public EditModel(
            IVehicleService vehicleService,
            IUserService userService,
            UserManager<ApplicationUser> userManager)
        {
            _vehicleService = vehicleService;
            _userService = userService;
            _userManager = userManager;
        }

        [TempData]
        public string StatusMessage { get; set; }

        [BindProperty]
        public UpdateVehicleModel Input { get; set; }

        private async Task LoadAsync(Vehicle vehicle)
        {
            Instructors = await _userService.GetAllUsersByRoleAsync(ApplicationRole.Instructor);

            var instructorId = vehicle.Instructor?.Id;
            var category = vehicle.Category;
            var description = vehicle.Description;
            var photo = vehicle.Photo;

            Input = new UpdateVehicleModel
            {
                InstructorId = instructorId,
                Category = category,
                Description = description,
                Photo = photo
            };
        }

        public async Task<IActionResult> OnGetAsync(string id)
        {
            var vehicle = await _vehicleService.GetByIdAsync(id);
            if (vehicle == null)
            {
                return base.NotFound($"Unable to load vehicle with ID '{id}'.");
            }

            await LoadAsync(vehicle);

            return Page();
        }

        public async Task<IActionResult> OnPostAsync(string id)
        {
            var vehicle = await _vehicleService.GetByIdAsync(id);
            if (vehicle == null)
            {
                return base.NotFound($"Unable to load vehicle with ID '{id}'.");
            }

            if (!ModelState.IsValid)
            {
                await LoadAsync(vehicle);
                return Page();
            }

            var instructorId = vehicle.Instructor?.Id;
            if (Input.InstructorId != instructorId)
            {
                var instructor = await _userManager.FindByIdAsync(Input.InstructorId);
                vehicle.Instructor = instructor;
                await _vehicleService.UpdateAsync(vehicle);
            }

            var category = vehicle.Category;
            if (Input.Category != category)
            {
                vehicle.Category = Input.Category;
                await _vehicleService.UpdateAsync(vehicle);
            }

            var description = vehicle.Description;
            if (Input.Description != description)
            {
                vehicle.Description = Input.Description;
                await _vehicleService.UpdateAsync(vehicle);
            }

            if (Request.Form.Files.Count > 0)
            {
                var file = Request.Form.Files.FirstOrDefault();
                using (var dataStream = new MemoryStream())
                {
                    await file.CopyToAsync(dataStream);
                    vehicle.Photo = dataStream.ToArray();
                }
                await _vehicleService.UpdateAsync(vehicle);
            }

            StatusMessage = "Vehicle details have been updated";

            return Page();
        }
    }
}
