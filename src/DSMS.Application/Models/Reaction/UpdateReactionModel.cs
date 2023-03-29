namespace DSMS.Application.Models.Reaction;

public class UpdateReactionModel
{
    Guid ReactionId { get; set; }

    Guid StudentId { get; set; }

    Boolean IsUseful { get; set; }
}

public class UpdateReactionResponseModel : BaseResponseModel { }
