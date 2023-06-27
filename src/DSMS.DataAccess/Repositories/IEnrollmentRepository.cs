using DSMS.Core.Entities;
using DSMS.Core.Entities.Identity;

namespace DSMS.DataAccess.Repositories
{
    public interface IEnrollmentRepository : IBaseRepository<Enrollment>
    {
        IQueryable<Enrollment> GetAll();

        IQueryable<Enrollment> GetByStudent(ApplicationUser student);

        Enrollment GetById(string id);
    }
}
