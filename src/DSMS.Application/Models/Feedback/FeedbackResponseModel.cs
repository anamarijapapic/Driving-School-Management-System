using DSMS.Core.Entities.Identity;

namespace DSMS.Application.Models.Feedback
{
    public class FeedbackResponseModel : BaseResponseModel
    {
        public ApplicationUser Instructor { get; set; }

        public ApplicationUser Student { get; set; }

        public string Title { get; set; }

        public string Content { get; set; }

        public int Rating { get; set; }

        public bool IsAnonymous { get; set; }

        public string CreatedBy { get; set; }

        public DateTime CreatedOn { get; set; }

        public string UpdatedBy { get; set; }

        public DateTime? UpdatedOn { get; set; }
    }
}
