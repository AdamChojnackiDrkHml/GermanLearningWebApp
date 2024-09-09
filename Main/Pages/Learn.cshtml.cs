using Microsoft.AspNetCore.Mvc.RazorPages;
using TestWebApp.Pages.Shared;
using TestWebApp.Services.LearningControllerService;

namespace TestWebApp.Pages;

public class LearnPage : PageModel
{
    private readonly ILearningControllerService _learningControllerService;
    
    public LearnPage(ILearningControllerService learningControllerService)
    {
        _learningControllerService = learningControllerService;
    }

    public void OnGet()
    {
        
    }
}