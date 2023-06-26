using DSMS.Core.Entities;
using DSMS.Core.Entities.Identity;

namespace DSMS.DataAccess.Repositories
{
    public interface IEnrollmentRepository : IBaseRepository<Enrollment>
    {
        Task<IEnumerable<Enrollment>> GetAllAsync();

        Task<IEnumerable<Enrollment>> GetByStudentAsync(ApplicationUser student);

        Enrollment GetById(string id);
    }
}
