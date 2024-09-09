namespace TestWebApp.Config;

public static class AppConfigurationExtensions
{
    public static WebApplication ConfigureApp(this WebApplication app)
    {
        app.UseSession();
        app.UseRouting();
        app.MapRazorPages();

        return app;
    }
    
    public static void ConfigAndRun(this WebApplication app)
    {
        app.ConfigureApp().Run();
    }
}