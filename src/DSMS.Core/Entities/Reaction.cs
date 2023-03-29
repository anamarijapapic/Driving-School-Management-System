using DSMS.Core.Common;

namespace DSMS.Core.Entities
{
    public class Reaction : BaseEntity
    {
        Guid StudentId { get; set; }

        Boolean IsUseful { get; set; }
    }
}
