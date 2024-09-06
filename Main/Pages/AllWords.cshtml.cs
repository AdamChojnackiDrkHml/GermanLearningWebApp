using System.Text.Encodings.Web;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using TestWebApp.Data.Models.Words;
using TestWebApp.Pages.Shared;
using TestWebApp.Services.LearningService;
using TestWebApp.Services.WordsService;
using TestWebApp.Services.WordsService.Implementation;
using TestWebApp.Services.WordsService.Models;

namespace TestWebApp.Pages;

public class AllWords : BasePageModel
{
    public AllWords(ILearningService learningService, IWordService wordService) : base(learningService)
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
        Console.WriteLine(string.Join(", ", Words));
        Console.WriteLine(SelectedGender);
        Console.WriteLine(Words.First().Gender.Equals(SelectedGender));
        
        Words = Words
            .Where(x => x.Type == WordEnum.Noun && x.Gender.Equals(SelectedGender));
        
        Console.WriteLine(string.Join(", ", Words));

        var page = Page();
        return page;
    }
    
}