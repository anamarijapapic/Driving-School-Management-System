#nullable disable

using AutoMapper;
using DSMS.Application.Models;
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

        private readonly IMapper _mapper;

        private readonly UserManager<ApplicationUser> _userManager;
        private IEnumerable<AppointmentResponseModel> appointments;

        public ApplicationUser ApplicationUser { get; set; }

        public string UserRole { get; set; }

        public PaginatedList<AppointmentResponseModel> Appointments { get; set; }

        public IndexModel(IAppointmentService appointmentService,
            UserManager<ApplicationUser> userManager,
            IMapper mapper)
        {
            _appointmentService = appointmentService;
            _mapper = mapper;
            _userManager = userManager;
        }

        public async Task<PageResult> OnGetAsync(string searchString, string currentFilter, int? pageIndex)
        {
            if (searchString != null)
            {
                pageIndex = 1;
            }

            var pageSize = 3;

            ApplicationUser = await _userManager.GetUserAsync(User);

            var roles = await _userManager.GetRolesAsync(ApplicationUser);

            UserRole = roles.First();

            await _appointmentService.AppointmentsToCompleteAsync();

            if (UserRole == "Administrator")
            {
                appointments = await _appointmentService.GetAllAsync();
            }
            else if (UserRole == "Instructor")
            {
                appointments = await _appointmentService.GetByInstructorAsync(ApplicationUser);
            }
            else
            {
                appointments = await _appointmentService.GetByStudentAsync(ApplicationUser);
            }

            ViewData["Keyword"] = searchString;
            appointments = _appointmentService.Search(Appointments, searchString);

            ViewData["CurrentFilter"] = currentFilter;
            appointments = _appointmentService.Filter(Appointments, currentFilter);

            Appointments = PaginatedList<AppointmentResponseModel>.Create(appointments, pageIndex ?? 1, pageSize);


            return Page();
        }


    }
}
