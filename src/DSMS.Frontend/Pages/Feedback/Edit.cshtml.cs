#nullable disable

using DSMS.Application.Models.Feedback;
using DSMS.DataAccess.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace DSMS.Frontend.Pages.Feedback
{
    public class EditModel : PageModel
    {
        private readonly IFeedbackRepository _feedbackRepository;

        public EditModel(IFeedbackRepository feedbackRepository)
        {
            _feedbackRepository = feedbackRepository;
        }

        [TempData]
        public string StatusMessage { get; set; }

        [BindProperty]
        public UpdateFeedbackModel Input { get; set; }

        private void Load(Core.Entities.Feedback feedback)
        {
            var title = feedback.Title;
            var rating = feedback.Rating;
            var content = feedback.Content;

            Input = new UpdateFeedbackModel
            {
                Title = title,
                Rating = rating,
                Content = content
            };
        }

        public async Task<IActionResult> OnGetAsync(string Id)
        {
            var result = await _feedbackRepository.GetAllAsync(f => f.Id.ToString() == Id);
            var feedback = result.First();
            if (feedback == null)
            {
                return base.NotFound($"Unable to load feedback with ID '{Id}'.");
            }

            Load(feedback);

            return Page();
        }

        public async Task<IActionResult> OnPostAsync(string Id)
        {
            var result = await _feedbackRepository.GetAllAsync(f => f.Id.ToString() == Id);
            var feedback = result.First();
            if (feedback == null)
            {
                return base.NotFound($"Unable to load feedback with Id '{Id}'.");
            }

            if (!ModelState.IsValid)
            {
                Load(feedback);
                return Page();
            }

            feedback.Title = Input.Title;
            feedback.Content = Input.Content;
            feedback.Rating = Input.Rating;

            await _feedbackRepository.UpdateAsync(feedback);

            StatusMessage = "Feedback details have been sucessfully updated.";

            return Page();
        }
    }
}
