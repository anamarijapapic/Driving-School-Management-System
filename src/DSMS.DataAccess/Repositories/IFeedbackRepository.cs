using DSMS.Core.Entities;

namespace DSMS.DataAccess.Repositories
{
    public interface IFeedbackRepository : IBaseRepository<Feedback>
    {
        IQueryable<Feedback> GetAll();
    }
}
