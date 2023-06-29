using DSMS.Core.Entities;

namespace DSMS.DataAccess.Repositories
{
    public interface IReactionRepository : IBaseRepository<Reaction>
    {
        Task<IEnumerable<Reaction>> GetAllAsync();

        Task<Reaction> GetByIdAsync(string id);
    }
}
