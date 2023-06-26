#nullable disable

using DSMS.Application.Models.Appointment;
using DSMS.Application.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace DSMS.Frontend.Pages.Appointments
{
    public class CancelModel : PageModel
    {
        private readonly IAppointmentService _appointmentService;

        public AppointmentResponseModel Appointment;

        public CancelModel(IAppointmentService appointmentService)
        {
            _appointmentService = appointmentService;
        }

        public async Task<IActionResult> OnGetAsync(string id)
        {
            var appointment = await _appointmentService.GetByIdAsync(id);
            if (appointment == null)
            {
                return base.NotFound($"Unable to find appointment with ID '{id}'.");
            }

            return Page();
        }

        public async Task<IActionResult> OnPostAsync(string id)
        {
            var appointment = await _appointmentService.GetByIdAsync(id);
            if (appointment == null)
            {
                return Page();
            }

            appointment.Status = Core.Enums.AppointmentStatus.Canceled;

            await _appointmentService.UpdateAsync(appointment);

            return Redirect("~/Appointments/Index");
        }
    }
}
