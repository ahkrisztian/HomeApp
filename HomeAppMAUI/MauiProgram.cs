using HomeAppMAUI.DataServices;
using HomeAppMAUI.Pages;
using HomeAppMAUI.ViewModels;
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
        builder.Services.AddHttpClient<IUserDataService, UserDataService>();

        builder.Services.AddSingleton<MainViewModel>();
        builder.Services.AddTransient<CreateViewModel>();
        builder.Services.AddTransient<HomeDetailsViewModel>();

        builder.Services.AddSingleton<MainPage>();

        builder.Services.AddTransient<HomeDetails>();
        builder.Services.AddTransient<CreateHomePage>();

        return builder.Build();
    }
}