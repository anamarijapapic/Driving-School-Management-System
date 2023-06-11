#nullable disable

using DSMS.Application.Models.User;
using DSMS.Application.Services;
using DSMS.Core.Entities;
using DSMS.Core.Entities.Identity;
using DSMS.Core.Enums;
using DSMS.DataAccess.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;

namespace DSMS.Frontend.Pages.Vehicles
{
    [Authorize(Roles = ("Administrator"))]
    public class EditModel : PageModel
    {
        private readonly IVehicleRepository _vehicleRepository;
        private readonly IUserService _userService;
        private readonly UserManager<ApplicationUser> _userManager;

        public IEnumerable<UserResponseModel> Instructors { get; private set; } = new List<UserResponseModel>();


        public EditModel(
            IVehicleRepository vehicleRepository,
            IUserService userService,
            UserManager<ApplicationUser> userManager)
        {
            _vehicleRepository = vehicleRepository;
            _userService = userService;
            _userManager = userManager;
        }

        [TempData]
        public string StatusMessage { get; set; }

        [BindProperty]
        public InputModel Input { get; set; }

        public class InputModel
        {
            [Display(Name = "Instructor")]
            public string InstructorId { get; set; }

            [Required]
            [Display(Name = "Category")]
            public Category Category { get; set; }

            [Required]
            [Display(Name = "Description")]
            public string Description { get; set; }

            [Display(Name = "Photo")]
            public byte[] Photo { get; set; }
        }

        private async Task LoadAsync(Vehicle vehicle)
        {
            Instructors = await _userService.GetAllInstructorsAsync();

            var instructorId = vehicle.Instructor?.Id;
            var category = vehicle.Category;
            var description = vehicle.Description;
            var photo = vehicle.Photo;

            Input = new InputModel
            {
                InstructorId = instructorId,
                Category = category,
                Description = description,
                Photo = photo
            };
        }

        public async Task<IActionResult> OnGetAsync(string Id)
        {
            var result = await _vehicleRepository.GetAllAsync(v => v.Id.ToString() == Id);
            var vehicle = result.First();
            if (vehicle == null)
            {
                return base.NotFound($"Unable to load vehicle with ID '{Id}'.");
            }

            await LoadAsync(vehicle);

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
                await _vehicleRepository.UpdateAsync(vehicle);
            }

            var category = vehicle.Category;
            if (Input.Category != category)
            {
                vehicle.Category = Input.Category;
                await _vehicleRepository.UpdateAsync(vehicle);
            }

            var description = vehicle.Description;
            if (Input.Description != description)
            {
                vehicle.Description = Input.Description;
                await _vehicleRepository.UpdateAsync(vehicle);
            }

            if (Request.Form.Files.Count > 0)
            {
                var file = Request.Form.Files.FirstOrDefault();
                using (var dataStream = new MemoryStream())
                {
                    await file.CopyToAsync(dataStream);
                    vehicle.Photo = dataStream.ToArray();
                }
                await _vehicleRepository.UpdateAsync(vehicle);
            }

            StatusMessage = "Vehicle details have been updated";

            return Page();
        }
    }
}
