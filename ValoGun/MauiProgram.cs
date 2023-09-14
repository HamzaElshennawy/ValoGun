using Microsoft.Extensions.Logging;
using ValoGun.ViewModels;
using CommunityToolkit.Maui;

namespace ValoGun
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder.UseMauiApp<App>().ConfigureFonts(fonts =>
            {
                fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
            }).UseMauiCommunityToolkit();
            builder.Services.AddSingleton<HomePageViewModel>();
            builder.Services.AddTransient<WeaponStatusViewModel>();
			builder.Services.AddSingleton<MainPage>();
#if DEBUG
            builder.Logging.AddDebug();
#endif
            return builder.Build();
        }
    }
}
