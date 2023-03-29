using DSMS.Core.Common;

namespace DSMS.Core.Entities
{
    public class ScheduleSlot : BaseEntity, IAuditedEntity
    {
        private Guid SlotId { get; set; }

        private Guid InstructorId { get; set; }

        private DateTime StartTime { get; set; }

        private DateTime EndTime { get; set; }

        public string CreatedBy { get; set; }

        public DateTime CreatedOn { get; set; }

        public string UpdatedBy { get; set; }

        public DateTime? UpdatedOn { get; set; }
    }
}
