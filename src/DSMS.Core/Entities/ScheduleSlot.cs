using DSMS.Core.Common;

namespace DSMS.Core.Entities
{
    public class ScheduleSlot : BaseEntity
    {
        private Guid InstructorId { get; set; }

        private DateTime StartTime { get; set; }

        private DateTime EndTime { get; set; }
    }
}
