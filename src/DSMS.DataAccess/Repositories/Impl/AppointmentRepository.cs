using DSMS.Core.Entities;
using DSMS.Core.Entities.Identity;
using DSMS.Core.Enums;
using DSMS.DataAccess.Persistence;
using Microsoft.EntityFrameworkCore;

namespace DSMS.DataAccess.Repositories.Impl
{
    public class AppointmentRepository : BaseRepository<Appointment>, IAppointmentRepository
    {
        public AppointmentRepository(DatabaseContext context) : base(context) { }

        public async Task<IEnumerable<Appointment>> GetAllAsync()
        {
            return await DbSet
                .Include(a => a.Instructor)
                .Include(a => a.Student)
                .OrderBy(a => a.Status)
                    .ThenBy(a => a.Date)
                        .ThenBy(a => a.Start)
                .ToListAsync();
        }

        public async Task<IEnumerable<Appointment>> GetByInstructorAsync(ApplicationUser instructor)
        {
            return await DbSet
                .Include(a => a.Instructor)
                .Include(a => a.Student)
                .Where(a => a.Instructor == instructor)
                .OrderBy(a => a.Status)
                    .ThenBy(a => a.Date)
                        .ThenBy(a => a.Start)
                .ToListAsync();
        }

        public async Task<IEnumerable<Appointment>> GetByStudentAsync(ApplicationUser student)
        {
            return await DbSet
                .Include(a => a.Instructor)
                .Include(a => a.Student)
                .Where(a => a.Student == student)
                .OrderBy(a => a.Status)
                    .ThenBy(a => a.Date)
                        .ThenBy(a => a.Start)
                .ToListAsync();
        }

        public Appointment GetById(string id)
        {
            return DbSet
                .Include(a => a.Instructor)
                .Include(a => a.Student)
                .Where(a => a.Id.ToString() == id)
                .FirstOrDefault();
        }

        public async Task<IEnumerable<TimeOnly>> GetReservedSlotsByInstructorAndDateAsync(ApplicationUser instructor,
            DateOnly date)
        {
            return await DbSet
                .Include(a => a.Instructor)
                .Include(a => a.Student)
                .Where(a => a.Instructor == instructor)
                .Where(a => a.Date == date)
                .Where(a => a.Status == AppointmentStatus.Reserved)
                .OrderBy(a => a.Date)
                    .ThenBy(a => a.Start)
                .Select(a => a.Start)
                .ToListAsync();
        }
    }
}
