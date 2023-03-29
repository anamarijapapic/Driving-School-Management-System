using DSMS.Core.Common;

namespace DSMS.Core.Entities
{
    public class Reaction : BaseEntity, IAuditedEntity
    {
        Guid ReactionId { get; set; }

        Guid StudentId { get; set; }

        Boolean IsUseful { get; set; }

        public string CreatedBy { get; set; }

        public DateTime CreatedOn { get; set; }

        public string UpdatedBy { get; set; }

        public DateTime? UpdatedOn { get; set; }
    }
}
