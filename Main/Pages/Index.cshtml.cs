using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TestWebApp.Services.LearningControllerService;

namespace TestWebApp.Pages;

public class Index : PageModel
{
    private readonly ILearningControllerService _learningControllerService;

    public List<string> Genders = [];
    
    public Index(ILearningControllerService learningControllerService)
    {
        _learningControllerService = learningControllerService;
    }

    public async Task<ActionResult> OnGet()
    {

        return Page();
    }
}