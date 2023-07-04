#nullable disable

using DSMS.Application.Models.Appointment;
using DSMS.Core.Enums;
using DSMS.Application.Services;
using DSMS.Core.Entities;
using DSMS.Core.Entities.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.RazorPages;
using AutoMapper;

namespace DSMS.Frontend.Pages.Appointments
{
    public class IndexModel : PageModel
    {
        private readonly IAppointmentService _appointmentService;

        private readonly IMapper _mapper;

        private readonly UserManager<ApplicationUser> _userManager;

        public ApplicationUser ApplicationUser { get; set; }

        public string UserRole { get; set; }

        public IEnumerable<AppointmentResponseModel> Appointments { get; set; }

        public IndexModel(IAppointmentService appointmentService,
            UserManager<ApplicationUser> userManager,
            IMapper mapper)
        {
            _appointmentService = appointmentService;
            _mapper = mapper;
            _userManager = userManager;
        }

        public async Task<PageResult> OnGetAsync(string searchString, string currentFilter)
        {
            ApplicationUser = await _userManager.GetUserAsync(User);

            var roles = await _userManager.GetRolesAsync(ApplicationUser);

            UserRole = roles.First();

            await _appointmentService.AppointmentsToCompleteAsync();

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


            ViewData["Keyword"] = searchString;
            Appointments = _appointmentService.Search(Appointments, searchString);

            ViewData["CurrentFilter"] = currentFilter;
            Appointments = _appointmentService.Filter(Appointments, currentFilter);
          
            
            return Page();
        }


    }
}
