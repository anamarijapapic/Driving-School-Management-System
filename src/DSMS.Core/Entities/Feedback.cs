using DSMS.Core.Common;

namespace DSMS.Core.Entities
{
    public class Feedback : BaseEntity, IAuditedEntity
    {
        private Guid FeedbackId { get; set; }

        private Guid InstructorId { get; set; }

        private Guid StudentId { get; set; }

        private string Title { get; set; }

        private string Content { get; set; }

        private int Rating { get; set; }

        private DateTime Created { get; set; }// Redundantno?

        private Boolean IsAnonymous { get; set; }

        public string CreatedBy { get; set; }

        public DateTime CreatedOn { get; set; }

        public string UpdatedBy { get; set; }

        public DateTime? UpdatedOn { get; set; }
    }
}
