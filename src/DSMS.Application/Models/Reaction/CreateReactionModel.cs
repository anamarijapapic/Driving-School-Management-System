namespace DSMS.Application.Models.Reaction;

public class CreateReactionModel
{
    Guid ReactionId { get; set; }

    Guid StudentId { get; set; }

    Boolean IsUseful { get; set; }
}

public class CreateReactionResponseModel : BaseResponseModel { }
