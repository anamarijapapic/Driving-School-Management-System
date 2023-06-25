using DSMS.Core.Common;
using DSMS.Core.Entities.Identity;
using DSMS.Core.Enums;

namespace DSMS.Core.Entities
{
    public class Appointment : BaseEntity
    {
        public ApplicationUser Student { get; set; }

        public ApplicationUser Instructor { get; set; }

        public DateOnly Date { get; set; }

        public TimeOnly Start { get; set; }

        public TimeOnly End { get; set; }

        public AppointmentStatus Status { get; set; }
    }
}
