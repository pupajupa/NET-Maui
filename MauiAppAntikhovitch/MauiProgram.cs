using Microsoft.Extensions.Logging;
using MauiAppAntikhovitch.View;
using MauiAppAntikhovitch.Services;

namespace MauiAppAntikhovitch;

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
        builder.Services.AddTransient<IDbService, SQLiteService>();
        builder.Services.AddTransient<HospitalRoomPage>();
#if DEBUG
        builder.Logging.AddDebug();
#endif

        return builder.Build();
    }
}
