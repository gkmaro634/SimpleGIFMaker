using CommunityToolkit.Maui;
using Microsoft.Extensions.Logging;
using SimpleGIFMaker.ViewModels;
using SimpleGIFMaker.Views;
using SimpleGIFMaker.Domains.Repositories;
using SimpleGIFMaker.DataSource.Fake;

namespace SimpleGIFMaker
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .UseMauiCommunityToolkit()
                .UseMauiCommunityToolkitMediaElement()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                });

            builder.Services.AddSingleton<EditPage>();
            builder.Services.AddSingleton<ContentGalleryPage>();
            builder.Services.AddSingleton<DirectoryExplorerPage>();

            builder.Services.AddSingleton<EditPageViewModel>();
            builder.Services.AddSingleton<ContentGalleryPageViewModel>();
            builder.Services.AddSingleton<DirectoryExplorerPageViewModel>();

            builder.Services.AddSingleton<IMovieRepository, FakeMovieRepository>();
            builder.Services.AddSingleton<IConvertConditionRepository, FakeConvertConditionRepository>();
            builder.Services.AddSingleton<IGifFileRepository, FakeGifFileRepository>();

#if DEBUG
            builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}
