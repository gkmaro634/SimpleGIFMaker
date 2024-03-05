using CommunityToolkit.Mvvm.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;
using SimpleGIFMaker.DataSource.FileSystem;
using SimpleGIFMaker.Domains;
using SimpleGIFMaker.Domains.Repositories;
using SimpleGIFMaker.ViewModels;
using System.Windows;

namespace SimpleGIFMaker
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public App()
        {
            Ioc.Default.ConfigureServices(
                new ServiceCollection()
                .AddSingleton<IMediaPlayer, MediaPlayer>()
                .AddSingleton<IConvertConditionRepository, FsConvertConditionRepository>()
                .AddSingleton<IMovieRepository, FsMovieRepository>()
                .AddSingleton<IGifFileRepository, FsGifFileRepository>()
                .AddTransient<MediaViewModel>()
                .AddTransient<ConvertControlViewModel>()
                .AddTransient<CropSettingViewModel>()
                .AddTransient<CutSettingViewModel>()
                .AddTransient<ConvertResultWindowViewModel>()
                .BuildServiceProvider());
        }
    }
}