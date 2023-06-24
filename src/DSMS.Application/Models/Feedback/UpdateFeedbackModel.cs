namespace DSMS.Application.Models.Feedback;

public class UpdateFeedbackModel
{
    public string Title { get; set; }

    public string Content { get; set; }

    public int Rating { get; set; }

    public bool IsAnonymous { get; set; }
}

public class UpdateFeedbackResponseModel : BaseResponseModel { }
