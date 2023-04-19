using DSMS.Core.Common;
using DSMS.Core.Entities.Identity;

namespace DSMS.Core.Entities
{
    public class Feedback : BaseEntity, IAuditedEntity
    {
        public ApplicationUser Instructor { get; set; } = null!;

        public ApplicationUser Student { get; set; }

        public string Title { get; set; }

        public string Content { get; set; }

        public int Rating { get; set; }

        public bool IsAnonymous { get; set; }

        public string CreatedBy { get; set; }

        public DateTime CreatedOn { get; set; }

        public string UpdatedBy { get; set; }

        public DateTime? UpdatedOn { get; set; }

        public ICollection<Reaction> Reactions { get; } = new List<Reaction>();
    }
}
