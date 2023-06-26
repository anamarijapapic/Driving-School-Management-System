using DSMS.Core.Entities;
using DSMS.Core.Entities.Identity;

namespace DSMS.DataAccess.Repositories
{
    public interface IAppointmentRepository : IBaseRepository<Appointment>
    {
        Task<IEnumerable<Appointment>> GetAllAsync();

        Task<IEnumerable<Appointment>> GetByInstructorAsync(ApplicationUser instructor);

        Task<IEnumerable<Appointment>> GetByStudentAsync(ApplicationUser student);

        Appointment GetById(string id);

        Task<IEnumerable<TimeOnly>> GetReservedSlotsByInstructorAndDateAsync(ApplicationUser instructor, DateOnly date);
    }
}
