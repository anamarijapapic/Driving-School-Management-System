using DSMS.Core.Entities;
using DSMS.DataAccess.Persistence;
using Microsoft.EntityFrameworkCore;

namespace DSMS.DataAccess.Repositories.Impl
{
    public class VehicleRepository : BaseRepository<Vehicle>, IVehicleRepository
    {
        public VehicleRepository(DatabaseContext context) : base(context) { }

        public async Task<IEnumerable<Vehicle>> GetAllAsync()
        {
            return await DbSet.Include(v => v.Instructor).ToListAsync();
        }
    }
}
