using CSharpFunctionalExtensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TestWebApp.Pages.Shared;
using TestWebApp.Services.LearningControllerService;
using TestWebApp.Services.WordService;
using TestWebApp.Services.WordService.Enums;
using TestWebApp.Services.WordService.Models;

namespace TestWebApp.Pages;

public class AddWords : PageModel
{
    private readonly IWordService _wordService;

    public AddWords(ILearningControllerService learningControllerService, IWordService wordService)
    {
        _wordService = wordService;
    }

    [BindProperty]
    public string Spelling { get; set; }

    [BindProperty]
    public string Translation { get; set; }

    [BindProperty]
    public WordEnum WordType { get; set; }

    [BindProperty]
    public GenderEnum? Gender { get; set; }
    
    public void OnGet(string wordType)
    {
        if (Enum.TryParse<WordEnum>(wordType, out var wordEnum))
        {
            WordType = wordEnum;
        }
        
        if (TempData["PopupMessage"] != null)
        {
            var message = TempData["PopupMessage"].ToString();
            ViewData["PopupMessage"] = message;
        }
    }

    public async Task<ActionResult> OnPost()
    {
        if (!ModelState.IsValid)
        {
            return Page();
        }
        var word = new WordDto( Id: null, Spelling, Translation, WordType, Gender);
 
        var message = 
            (await _wordService.AddWordAsync(word)).Match(
                onSuccess: () => "Word added successfully",
                onFailure: err => err
            );

        TempData["PopupMessage"] = message;
        Console.WriteLine(message);
        return Page();
    }
}