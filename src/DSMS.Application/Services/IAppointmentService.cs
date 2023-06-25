using DSMS.Application.Models.Appointment;
using DSMS.Core.Entities.Identity;

namespace DSMS.Application.Services
{
    public interface IAppointmentService
    {
        Task<AppointmentResponseModel> CreateAsync(AppointmentModel model);

        Task<IEnumerable<AppointmentResponseModel>> GetAllAsync();

        Task<AppointmentResponseModel> UpdateAsync(AppointmentModel update);

        Task<IEnumerable<AppointmentResponseModel>> GetByIdAsync(string id);

        Task<IEnumerable<AppointmentResponseModel>> GetByInstructorAsync(ApplicationUser instructor);

        Task<IEnumerable<AppointmentResponseModel>> GetByStudentAsync(ApplicationUser student);

        Task<IEnumerable<TimeOnly>> GetReservedSlotsByInstructorAndDateAsync(ApplicationUser instructor, DateOnly date);
    }
}
