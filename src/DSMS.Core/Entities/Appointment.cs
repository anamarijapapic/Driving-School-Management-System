using DSMS.Core.Common;

namespace DSMS.Core.Entities
{
    public class Appointment : BaseEntity, IAuditedEntity
    {
        private Guid AppointmentId { get; set; }

        private Guid StudentId { get; set; }

        private Guid SlotId { get; set; }

        private Guid StatusId { get; set; }

        public string CreatedBy { get; set; }

        public DateTime CreatedOn { get; set; }

        public string UpdatedBy { get; set; }

        public DateTime? UpdatedOn { get; set; }
    }
}
