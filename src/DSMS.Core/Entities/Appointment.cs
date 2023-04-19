using DSMS.Core.Common;
using DSMS.Core.Entities.Identity;

namespace DSMS.Core.Entities
{
    public class Appointment : BaseEntity
    {
        public ApplicationUser Student { get; set; } = null!;

        public ScheduleSlot ScheduleSlot { get; set; }

        public Status Status { get; set; }
    }
}
