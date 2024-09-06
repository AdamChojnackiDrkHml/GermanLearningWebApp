using TestWebApp.Pages.Shared;
using TestWebApp.Services.LearningService;

namespace TestWebApp.Pages;

public class Learn(ILearningService learningService) : BasePageModel(learningService)
{
    public void OnGet()
    {
        
    }
}