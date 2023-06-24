using System.ComponentModel.DataAnnotations;

namespace DSMS.Application.Models.Feedback;

public class CreateFeedbackModel : BaseResponseModel
{
    [DataType(DataType.Text)]
    [Display(Name = "Instructor")]
    public string InstructorId { get; set; }

    [DataType(DataType.Text)]
    [Display(Name = "Student")]
    public string StudentId { get; set; }

    [Required]
    [DataType(DataType.Text)]
    [Display(Name = "Title")]
    public string Title { get; set; }

    [Required]
    [DataType(DataType.Text)]
    [Display(Name = "Content")]
    public string Content { get; set; }

    [Required]
    [Display(Name = "Rating")]
    public int Rating { get; set; }

    [DataType(DataType.DateTime)]
    [Display(Name = "Created On")]
    public DateTime CreatedOn { get; set; }

    [Display(Name = "Is Anonymous")]
    public bool IsAnonymous { get; set; }
}

public class CreateFeedbackResponseModel : BaseResponseModel { }
