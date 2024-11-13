using CSharpFunctionalExtensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TestWebApp.Pages.Shared;
using TestWebApp.Services.LearningControllerService;
using TestWebApp.Services.LearningControllerService.Models;
using TestWebApp.Services.LearningService.Enums;
using static TestWebApp.Extensions.TempDataExtensions;

namespace TestWebApp.Pages;

public class LearnPage : PageModel
{
    private readonly ILearningControllerService _learningControllerService;
    
    public TrainingLevelEnum? SelectedDifficulty { get; set; }

    public ExerciseDto Exercise;
    
    public LearnPage(ILearningControllerService learningControllerService)
    {
        _learningControllerService = learningControllerService;
    }
 
    public async Task<ActionResult> OnGet(TrainingLevelEnum? trainingLevelEnum)
    {
        if (!trainingLevelEnum.HasValue)
        {
            return Page();
        }

        SelectedDifficulty = trainingLevelEnum.Value;
        await _learningControllerService.PrepareTrainingAsync(TrainingLevelEnum.Easy);

        var res = await LoadExercise();

        if (res.IsFailure)
        {
            return RedirectToPage("/Learn");
        }
        
        return Page();
    }
    
    public async Task<ActionResult> OnPost(string answer)
    {
        var exercise = TempData.Get<ExerciseDto>("Exercise");
        var exerciseAnswer = new ExerciseAnswerDto(exercise!, answer);
        var result = _learningControllerService.CheckAnswerAsync(exerciseAnswer);

        //TODO evaluate result
        
        var loadExercise = await LoadExercise();
        
        if (loadExercise.IsFailure)
        {
            return RedirectToPage("/Learn");
        }
  
        return Page();
    }
    
    private async Task<Result> LoadExercise()
    {
        // TODO: Better error handling
        var loadExercise = _learningControllerService.GetNextWord();
        
        if (loadExercise.IsFailure)
        {
            // TODO evaluate result
            await _learningControllerService.SaveTrainingResultAsync();
            return Result.Failure(loadExercise.Error);
        }

        TempData.Put("Exercise", loadExercise.Value);
        Exercise = loadExercise.Value;
        return Result.Success();
    }
}