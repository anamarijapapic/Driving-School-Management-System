using DSMS.Core.Entities.Identity;

namespace DSMS.Application.Models.Reaction
{
    public class ReactionResponseModel : BaseResponseModel
    {
        public Core.Entities.Feedback Feedback { get; set; } = null!;
        public ApplicationUser Student { get; set; } = null!;
        public bool IsUseful { get; set; }
    }
}
