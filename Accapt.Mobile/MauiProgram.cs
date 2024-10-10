using Accapt.Mobile.Controls;
using Accapt.Mobile.Platforms;
using Accapt.Mobile.Platforms.Android;
using Microsoft.Extensions.Logging;

namespace Accapt.Mobile
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
                    fonts.AddFont("Vazir-Bold-FD-WOL.ttf", "VazirFont");
                });

#if DEBUG
    		builder.Logging.AddDebug();
#endif

            Microsoft.Maui.Handlers.ElementHandler.ElementMapper.AppendToMapping("Classic", (handler, view) =>
            {
                if (view is CustomeEntry)
                {
                    CustomEntryMapper.Map(handler, view);
                }
            });

            return builder.Build();
        }
    }
}
