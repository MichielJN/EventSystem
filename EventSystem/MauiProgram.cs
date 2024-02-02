using EventSystem.Data;
using EventSystem.Models;
using Microsoft.Extensions.Logging;

namespace EventSystem
{
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
            builder.Services.AddSingleton<BaseRepository<HappeningMaker>>();
            builder.Services.AddSingleton<BaseRepository<Happening>>();
            builder.Services.AddSingleton<BaseRepository<Invitee>>();
#if DEBUG
    		builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}
