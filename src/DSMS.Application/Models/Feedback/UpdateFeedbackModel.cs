namespace DSMS.Application.Models.Feedback;

public class UpdateFeedbackModel
{
    private string Title { get; set; }

    private string Content { get; set; }

    private int Rating { get; set; }

    private Boolean IsAnonymous { get; set; }
}

public class UpdateFeedbackResponseModel : BaseResponseModel { }
