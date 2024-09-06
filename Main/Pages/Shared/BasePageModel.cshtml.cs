using Microsoft.AspNetCore.Mvc.RazorPages;
using TestWebApp.Services.LearningService;

namespace TestWebApp.Pages.Shared;

public abstract class BasePageModel : PageModel
{
    protected readonly ILearningService LearningService;

    protected BasePageModel(ILearningService learningService)
    {
        LearningService = learningService;
    }
}