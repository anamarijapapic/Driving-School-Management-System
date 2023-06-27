using DSMS.Core.Entities;

namespace DSMS.DataAccess.Repositories
{
    public interface IFeedbackRepository : IBaseRepository<Feedback>
    {
        IQueryable<Feedback> GetAll();
        Task<IEnumerable<Feedback>> GetAllAsync();

        Task<Feedback> GetByIdAsync(string id);
    }
}
