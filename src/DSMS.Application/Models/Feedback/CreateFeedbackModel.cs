namespace DSMS.Application.Models.Feedback;

public class CreateFeedbackModel
{
    private Guid FeedbackId { get; set; }

    private Guid InstructorId { get; set; }

    private Guid StudentId { get; set; }

    private string Title { get; set; }

    private string Content { get; set; }

    private int Rating { get; set; }

    private DateTime Created { get; set; }

    private Boolean IsAnonymous { get; set; }
}

public class CreateFeedbackResponseModel : BaseResponseModel { }
