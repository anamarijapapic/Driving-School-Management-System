﻿#nullable disable

using DSMS.Application.Services;
using DSMS.Core.Entities.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.RazorPages;
using DSMS.Core.Entities;
using DSMS.Application.Models.Appointment;

namespace DSMS.Frontend.Pages.Appointments
{
    public class IndexModel : PageModel
    {
        private readonly UserManager<ApplicationUser> _userManager;

        private readonly IEnrollmentService _enrollmentService;

        private readonly IAppointmentService _appointmentService;

        public ApplicationUser ApplicationUser { get; set; }

        public string UserRole { get; set; }

        public IEnumerable<AppointmentResponseModel> Appointments { get; set; }

        public IndexModel(UserManager<ApplicationUser> userManager,
            IEnrollmentService enrollmentService,
            IAppointmentService appointmentService)
        {
            _userManager = userManager;
            _enrollmentService = enrollmentService;
            _appointmentService = appointmentService;
        }

        public async Task<PageResult> OnGetAsync()
        {
            ApplicationUser = await _userManager.GetUserAsync(User);

            var roles = await _userManager.GetRolesAsync(ApplicationUser);

            UserRole = roles.First();

            if(UserRole == "Administrator")
            {
                Appointments = await _appointmentService.GetAllAsync();
            }

            if (UserRole == "Instructor")
            {
                Appointments = await _appointmentService.GetByInstructorAsync(ApplicationUser);
            }

            if (UserRole == "Student")
            {
                Appointments = await _appointmentService.GetByStudentAsync(ApplicationUser);
            }

            return Page();
        }
    }
}
