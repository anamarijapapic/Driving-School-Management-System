using DSMS.Core.Entities.Identity;
using DSMS.Core.Entities;
namespace DSMS.Application.Models.Reaction
{
    public class ReactionResponseModel : BaseResponseModel
    {
        public DSMS.Core.Entities.Feedback Feedback { get; set; } = null!;
        public ApplicationUser Student { get; set; } = null!;
        public bool IsUseful { get; set; }
    }
}
