using CommunityToolkit.Maui;
using SkiaSharp.Views.Maui.Controls.Hosting;
using ValoGun.Pages;
using ValoGun.ViewModels;



namespace ValoGun
{
	public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder.UseMauiApp<App>().UseSkiaSharp().ConfigureFonts(fonts =>
            {
                fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                fonts.AddFont("BowlbyRegular.ttf", "BowlbyRegular");
				fonts.AddFont("AllertaStencil-Regular.ttf", "Allerta");
            }).UseMauiCommunityToolkit();

            builder.Services.AddSingleton<HomePageViewModel>();
            builder.Services.AddTransient<WeaponsViewModel>();
            builder.Services.AddSingleton<HomePage>();



            return builder.Build();
        }
    }
}
