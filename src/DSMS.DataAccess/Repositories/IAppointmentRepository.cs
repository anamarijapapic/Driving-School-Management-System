using DSMS.Core.Entities;
using DSMS.Core.Entities.Identity;

namespace DSMS.DataAccess.Repositories
{
    public interface IAppointmentRepository : IBaseRepository<Appointment>
    {

        IQueryable<Appointment> GetAll();

        IQueryable<Appointment> GetByInstructor(ApplicationUser instructor);

        IQueryable<Appointment> GetByStudent(ApplicationUser student);

        Appointment GetById(string id);

        IQueryable<TimeOnly> GetReservedSlotsByInstructorAndDate(ApplicationUser instructor, DateOnly date);
    }
}
