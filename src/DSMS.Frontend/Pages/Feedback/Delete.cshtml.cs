using DSMS.Application.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace DSMS.Frontend.Pages.Feedback
{
    public class DeleteModel : PageModel
    {
        private readonly IFeedbackService _feedbackService;

        public DeleteModel(IFeedbackService feedbackService)
        {
            _feedbackService = feedbackService;
        }

        public async Task<IActionResult> OnGetAsync(string id)
        {
            var feedback = await _feedbackService.GetByIdAsync(id);
            if (feedback == null)
            {
                return base.NotFound($"Unable to load feedback with ID '{id}'.");
            }

            return Page();
        }

        public async Task<IActionResult> OnPostAsync(string id)
        {
            var feedback = await _feedbackService.GetByIdAsync(id);
            if (feedback == null)
            {
                return base.NotFound($"Unable to load feedback with ID '{id}'.");
            }

            await _feedbackService.DeleteAsync(feedback);

            return Redirect("~/Users/Index");
        }
    }
}
