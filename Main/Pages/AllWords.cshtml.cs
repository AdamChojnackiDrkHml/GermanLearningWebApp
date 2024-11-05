using Azure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TestWebApp.Pages.Shared;
using TestWebApp.Services.LearningControllerService;
using TestWebApp.Services.WordService;
using TestWebApp.Services.WordService.Enums;
using TestWebApp.Services.WordService.Models;

namespace TestWebApp.Pages;

public class AllWords : PageModel
{
    public AllWords(ILearningControllerService learningControllerService, IWordService wordService)
    {
        _wordService = wordService;
    }

    private IWordService _wordService;
        
    [BindProperty]
    public IEnumerable<WordDto> Words { get; set; }

    [BindProperty]
    public WordEnum? SelectedWordTypes { get; set; }

    [BindProperty]
    public GenderEnum? SelectedGender { get; set; }
    
    public async Task<ActionResult> OnGet()
    {
        Words = await _wordService.GetAllWords();
        return Page();
    }
    
    public async Task<ActionResult> OnPostWordType()
    {
        if (SelectedWordTypes == null)
        {
            Words = await _wordService.GetAllWords();
        }
        
        Words = await _wordService.GetWordsAsync(SelectedWordTypes!.Value);

        if (SelectedGender == null)
        {
            return Page();
        }
        
        Words = Words
            .Where(x => x.Type == WordEnum.Noun && x.Gender.Equals(SelectedGender));
        
        Console.WriteLine(string.Join(", ", Words));

        return Page();
    }
    
}