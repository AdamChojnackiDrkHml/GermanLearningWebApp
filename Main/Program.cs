using Microsoft.EntityFrameworkCore;
using TestWebApp.Data;
using TestWebApp.Services.LearningService;
using TestWebApp.Services.LearningService.Implementation;
using TestWebApp.Services.WordsService;
using TestWebApp.Services.WordsService.Implementation;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<GermanLearningDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddRazorPages();

builder.Services.AddScoped<IWordService, WordService>();
builder.Services.AddScoped<ILearningService, LearningService>();

var app = builder.Build();

app.MapRazorPages();
// app.MapGet("/", () => "Hello World!");

app.Run();


