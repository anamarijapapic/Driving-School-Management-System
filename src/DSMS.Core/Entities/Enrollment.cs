using DSMS.Core.Common;

namespace DSMS.Core.Entities
{
    public class Enrollment : BaseEntity, IAuditedEntity
    {
        private Guid EnrollmentId { get; set; }

        private Guid StudentId { get; set; }

        private Guid CategoryId { get; set; }

        private Guid InstructorId { get; set; }

        public string CreatedBy { get; set; }

        public DateTime CreatedOn { get; set; }

        public string UpdatedBy { get; set; }

        public DateTime? UpdatedOn { get; set; }
    }
}
