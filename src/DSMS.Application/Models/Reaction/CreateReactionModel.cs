﻿namespace DSMS.Application.Models.Reaction;

public class CreateReactionModel
{
    public string FeedbackId { get; set; }

    public string StudentId { get; set; }

    public Boolean IsUseful { get; set; }
}

public class CreateReactionResponseModel : BaseResponseModel { }
