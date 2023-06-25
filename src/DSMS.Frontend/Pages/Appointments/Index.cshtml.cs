using AutoMapper;
using DSMS.Application.Models.Appointment;
using DSMS.Application.Services;
using DSMS.Core.Entities.Identity;
using DSMS.DataAccess.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace DSMS.Frontend.Pages.Appointments
{
    public class IndexModel : PageModel
    {
        private readonly IAppointmentService _appointmentService;
        private readonly IEnrollmentService _enrollmentService;
        private readonly UserManager<ApplicationUser> _userManager;
        public List<string> slots = new List<string> { "10:00", "11:00", "12:00", "13:00", "14:00", "15:00", "16:00", "17:00" };

        public IndexModel(IAppointmentService appointmentService, 
            IEnrollmentService enrollmentService,
            UserManager<ApplicationUser> userManager)
        {
            _appointmentService = appointmentService;
            _enrollmentService = enrollmentService;
            _userManager = userManager;
        }

        public async Task<PageResult> OnGetAsync()
        {
            var student = await _userManager.GetUserAsync(User);
            var enrollments = _enrollmentService.GetByStudentAsync(student);

            //var appointments = slots.ForEach(slot => _appointmentService.GetByInstructorAsync(enrollments.Result.First().Instructor));
            return Page();
        }
    }
}
