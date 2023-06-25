using DSMS.Application.Models.Appointment;
using DSMS.Core.Entities.Identity;

namespace DSMS.Application.Services
{
    public interface IAppointmentService
    {
        Task<AppointmentResponseModel> CreateAsync(AppointmentModel model);

        Task<IEnumerable<AppointmentResponseModel>> GetAllAsync();

        Task<IEnumerable<AppointmentResponseModel>> GetByInstructorAsync(ApplicationUser instructor);

        Task<IEnumerable<TimeOnly>> GetReservedSlotsByInstructorAndDateAsync(ApplicationUser instructor, DateOnly date);
    }
}
