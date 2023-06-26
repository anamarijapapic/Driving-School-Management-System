using DSMS.Application.Models.Appointment;
using DSMS.Core.Entities;
using DSMS.Core.Entities.Identity;

namespace DSMS.Application.Services
{
    public interface IAppointmentService
    {
        Task<AppointmentResponseModel> CreateAsync(CreateAppointmentModel createAppointmentModel);

        Task<IEnumerable<AppointmentResponseModel>> GetAllAsync();

        Task<IEnumerable<AppointmentResponseModel>> GetByInstructorAsync(ApplicationUser instructor);

        Task<IEnumerable<AppointmentResponseModel>> GetByStudentAsync(ApplicationUser student);

        Task<Appointment> GetByIdAsync(string id);

        Task<IEnumerable<TimeOnly>> GetReservedSlotsByInstructorAndDateAsync(ApplicationUser instructor, DateOnly date);

        Task<Appointment> UpdateAsync(Appointment appointment);
    }
}
