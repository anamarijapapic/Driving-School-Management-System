using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace DSMS.Application.Models.Feedback;

public class CreateFeedbackModel : BaseResponseModel
{
    [Required]
    [DataType(DataType.Text)]
    [Display(Name = "Instructor")]
    public string InstructorId { get; set; }

    [Required]
    [DataType(DataType.Text)]
    [Display(Name = "Student")]
    public string  StudentId { get; set; }

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

    [Required]
    [DataType(DataType.DateTime)]
    [Display(Name = "Created")]
    public DateTime Created { get; set; }

    [Required]
    [Display(Name = "IsAnonymous")]
    public Boolean IsAnonymous { get; set; }
}

public class CreateFeedbackResponseModel : BaseResponseModel { }
