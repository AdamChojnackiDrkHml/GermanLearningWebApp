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
        
    public IEnumerable<WordDto> Words { get; set; }

    [BindProperty]
    public WordEnum? SelectedWordTypes { get; set; }

    [BindProperty]
    public GenderEnum? SelectedGender { get; set; }
    
    public async Task OnGet()
    {
        Words = await _wordService.GetAllWords();
    }
    
    public async Task OnPostWordType()
    {
        if (SelectedWordTypes == null)
        {
            Words = await _wordService.GetAllWords();
        }
        
        Words = await _wordService.GetWordsAsync(SelectedWordTypes!.Value);

        if (SelectedGender == null)
        {
            return;
        }
        
        Words = Words
            .Where(x => x.Type == WordEnum.Noun && x.Gender! == SelectedGender);
        
        SelectedGender = null;
    }

    public async Task OnPost()
    {
        // Console.WriteLine(SelectedCategories);
        // if (SelectedCategories.Count == 0)
        // {
        //     Words = await _wordService.GetWordsAsync();
        // }
        //
        // Words = await _wordService.GetWordsAsync(SelectedCategories);
    }
}