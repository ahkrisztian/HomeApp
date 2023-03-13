using HomeAppMAUI.DataServices;
using HomeAppMAUI.Pages;
using Microsoft.Extensions.Logging;

namespace HomeAppMAUI;

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

        builder.Services.AddHttpClient<IHomeDataService, HomeDataService>();

        builder.Services.AddSingleton<MainPage>();
        builder.Services.AddTransient<CreateHomePage>();
        builder.Services.AddTransient<UpdateHomePage>();

        return builder.Build();
    }
}