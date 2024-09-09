using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TestWebApp.Services.LearningService;

namespace TestWebApp.Pages;

public class Index : PageModel
{
    private readonly ILearningService _learningService;

    public List<string> Genders = [];
    
    public Index(ILearningService learningService)
    {
        _learningService = learningService;
    }

    public async Task<ActionResult> OnGet()
    {

        return Page();
    }
}