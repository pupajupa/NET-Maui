using Microsoft.Extensions.Logging;
using MauiAppAntikhovitch.View;
using MauiAppAntikhovitch.Services;
using System.Net.Http;
using Microsoft.Extensions.Http;
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
        builder.Services.AddHttpClient<IRateService, RateService>(opt => opt.BaseAddress = new Uri("https://www.nbrb.by/api/exrates/rates"));
        builder.Services.AddTransient<CurrencyConverterPage>(); 

#if DEBUG
        builder.Logging.AddDebug();
#endif

        return builder.Build();
    }
}
