using DSMS.Core.Entities;

namespace DSMS.DataAccess.Repositories
{
    public interface IEnrollmentRepository : IBaseRepository<Enrollment>
    {
        Task<IEnumerable<Enrollment>> GetAllAsync();
    }
}
