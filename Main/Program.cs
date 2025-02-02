using TestWebApp.Config;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddRazorPages();

builder.Services.ConfigureAppServices(builder.Configuration, builder.Environment);

builder.Build().ConfigAndRun();

return;

