using DSMS.Core.Common;
using DSMS.Core.Entities.Identity;

namespace DSMS.Core.Entities
{
    public class ScheduleSlot : BaseEntity
    {
        public ApplicationUser Instructor { get; set; } = null!;

        public DateTime StartTime { get; set; }

        public DateTime EndTime { get; set; }

        public ICollection<Appointment> Appointments { get; } = new List<Appointment>();
    }
}
