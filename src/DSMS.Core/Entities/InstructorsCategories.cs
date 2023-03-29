using DSMS.Core.Common;

namespace DSMS.Core.Entities
{
    public class InstructorsCategories : BaseEntity, IAuditedEntity
    {
        private Guid InstructorId { get; set; }

        private Guid CategoryId { get; set; }

        public string CreatedBy { get; set; }

        public DateTime CreatedOn { get; set; }

        public string UpdatedBy { get; set; }

        public DateTime? UpdatedOn { get; set; }
    }
}
