#nullable disable

using AutoMapper;
using DSMS.Application.Models.User;
using DSMS.Application.Models.Vehicle;
using DSMS.Application.Services;
using DSMS.Core.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace DSMS.Frontend.Pages.Vehicles
{
    [Authorize(Roles = ("Administrator"))]
    public class CreateModel : PageModel
    {
        private readonly IMapper _mapper;
        private readonly IVehicleService _vehicleService;
        private readonly IUserService _userService;

        public IEnumerable<UserResponseModel> Instructors { get; private set; } = new List<UserResponseModel>();


        public CreateModel(
            IMapper mapper,
            IVehicleService vehicleService,
            IUserService userService)
        {
            _mapper = mapper;
            _vehicleService = vehicleService;
            _userService = userService;
        }

        [BindProperty]
        public CreateVehicleModel Input { get; set; }

        public async Task<IActionResult> OnGetAsync()
        {
            Instructors = await _userService.GetAllInstructorsAsync();

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid) { return Page(); }

            if (Request.Form.Files.Count > 0)
            {
                var file = Request.Form.Files.FirstOrDefault();
                using var dataStream = new MemoryStream();
                await file.CopyToAsync(dataStream);
                Input.Photo = dataStream.ToArray();
            }

            var vehicle = _mapper.Map<Vehicle>(Input);
            try
            {
                await _vehicleService.CreateAsync(Input);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return Redirect("~/Vehicles/Index");
        }
    }
}
