#nullable disable

using DSMS.Application.Models.Appointment;
using DSMS.Application.Services;
using DSMS.Core.Entities.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace DSMS.Frontend.Pages.Appointments
{
    public class IndexModel : PageModel
    {
        private readonly IAppointmentService _appointmentService;

        private readonly UserManager<ApplicationUser> _userManager;

        public ApplicationUser ApplicationUser { get; set; }

        public string UserRole { get; set; }

        public IEnumerable<AppointmentResponseModel> Appointments { get; set; }

        public IndexModel(IAppointmentService appointmentService,
            UserManager<ApplicationUser> userManager)
        {
            _appointmentService = appointmentService;
            _userManager = userManager;
        }

        public async Task<PageResult> OnGetAsync()
        {
            ApplicationUser = await _userManager.GetUserAsync(User);

            var roles = await _userManager.GetRolesAsync(ApplicationUser);

            UserRole = roles.First();

            if (UserRole == "Administrator")
            {
                Appointments = await _appointmentService.GetAllAsync();
            }
            else if (UserRole == "Instructor")
            {
                Appointments = await _appointmentService.GetByInstructorAsync(ApplicationUser);
            }
            else
            {
                Appointments = await _appointmentService.GetByStudentAsync(ApplicationUser);
            }

            return Page();
        }
    }
}
