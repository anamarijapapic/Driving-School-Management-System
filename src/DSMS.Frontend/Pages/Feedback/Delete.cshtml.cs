using DSMS.DataAccess.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace DSMS.Frontend.Pages.Feedback
{
    public class DeleteModel : PageModel
    {
        private readonly IFeedbackRepository _feedbackRepository;

        public DeleteModel(IFeedbackRepository feedbackRepository)
        {
            _feedbackRepository = feedbackRepository;
        }

        public async Task<IActionResult> OnGetAsync(string Id)
        {
            var result = await _feedbackRepository.GetAllAsync(f => f.Id.ToString() == Id);
            var feedback = result.First();
            if (feedback == null)
            {
                return base.NotFound($"Unable to find feedback with ID '{Id}'.");
            }

            return Page();
        }

        public async Task<IActionResult> OnPostAsync(string Id)
        {
            var result = await _feedbackRepository.GetAllAsync(f => f.Id.ToString() == Id);
            var feedback = result.First();
            if (feedback == null)
            {
                return base.NotFound($"Unable to find feedback with ID '{Id}'.");
            }

            await _feedbackRepository.DeleteAsync(feedback);

            return Redirect("~/Users/Index");
        }
    }
}
