using Microsoft.Extensions.Logging;

namespace Chatter
{
    /* 
     * Each platform specific starting point calls CreateMauiApp()
     * this method can be used to configure the application using an app builder object
     * UseMauiApp<App>() is a generic method of the app builder object
     * it contains methods for various tasks including registering fonts, registering custom handlers for controls, etc.
    */
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                });

#if DEBUG
    		builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}
