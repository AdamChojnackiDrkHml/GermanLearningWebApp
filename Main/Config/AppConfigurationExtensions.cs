namespace TestWebApp.Config;

public static class AppConfigurationExtensions
{
    public static WebApplication ConfigureApp(this WebApplication app)
    {
        app.UseSession();
        app.UseRouting();
        app.MapRazorPages();
        if (!app.Environment.IsDevelopment())
        {
            app.UseExceptionHandler("/Error");
            app.UseHsts();
        }
        app.UseAuthentication();

        return app;
    }
    
    public static void ConfigAndRun(this WebApplication app)
    {
        app.ConfigureApp().Run();
    }
}