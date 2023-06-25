using AutoMapper;
using DSMS.Application.Models.Appointment;
using DSMS.Application.Services;
using DSMS.Core.Entities;
using DSMS.DataAccess.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace DSMS.Frontend.Pages.Appointments
{
    public class CancelModel : PageModel
    {
        private IMapper _mapper;
        private readonly IAppointmentService _appointmentService;
        private readonly IAppointmentRepository _appointmentRepository;
        public AppointmentResponseModel Appointment;

        public CancelModel(IAppointmentService appointmentService, IAppointmentRepository appointmentRepository, IMapper mapper)
        {
            _appointmentService = appointmentService;
            _mapper = mapper;
            _appointmentRepository = appointmentRepository; 
        }

        public async Task<IActionResult> OnGetAsync(string id)
        {
            var result = await _appointmentService.GetByIdAsync(id);
            Appointment = result.FirstOrDefault();

            return Page();
            

        }

        public async Task<IActionResult> OnPostAsync(string id)
        {
            var result = await _appointmentService.GetByIdAsync(id);
            Appointment = result.FirstOrDefault();
            if (result == null) { 
                return Page();
            }
            var updatedAppointment = _mapper.Map<AppointmentModel>(Appointment);
            updatedAppointment.Status = Core.Enums.AppointmentStatus.Canceled;
            await _appointmentService.UpdateAsync(updatedAppointment);

            return Redirect("~/Appointments/Index");
        }
    }
}
