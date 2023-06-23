using DSMS.DataAccess.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace DSMS.Frontend.Pages.Feedback
{
    public class DeleteModel : PageModel
    {
        private readonly IFeedbackRepository _feedbackRepository;
        public Core.Entities.Feedback Feedback { get; set; }

        public DeleteModel(IFeedbackRepository feedbackRepository)
        {
            _feedbackRepository= feedbackRepository;
        }

        public async Task<object> LoadAsync(string Id)
        {
            var result = await _feedbackRepository.GetAllAsync(f => f.Id.ToString() == Id);
            var feedback = result.FirstOrDefault();
            if (feedback == null)
            {
                return base.NotFound($"Unable to find feedback with ID '{Id}'.");
            }
            Feedback = feedback;
            return null;

        }

        public async Task<IActionResult> OnGetAsync(string Id)
        {
            await LoadAsync(Id);
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(string Id)
        {
            await LoadAsync(Id);
            await _feedbackRepository.DeleteAsync(Feedback);
            return Page();

        }
    }
}
