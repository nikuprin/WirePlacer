using Microsoft.Extensions.Logging;
using WirePlacer.Services;
using WirePlacer.ViewModels;
using WirePlacer.Views;

namespace WirePlacer;

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
        builder.Services.AddTransient<IInputDataReader, InputDataReader>();
        builder.Services.AddTransient<ISolutionVisualizer, SolutionVisualizer>();
        builder.Services.AddTransient<MainPage>();
        builder.Services.AddTransient<IMainViewModel, MainViewModel>();


#if DEBUG
        builder.Logging.AddDebug();
#endif

        return builder.Build();
    }
}
