using CommunityToolkit.Mvvm.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;
using SimpleGIFMaker.DataSource.Fake;
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
                .AddSingleton<IConvertConditionRepository, FakeConvertConditionRepository>()
                .AddSingleton<IMovieRepository, FakeMovieRepository>()
                .AddSingleton<IGifFileRepository, FakeGifFileRepository>()
                .AddTransient<MediaViewModel>()
                .AddTransient<ConvertControlViewModel>()
                .AddTransient<CropSettingViewModel>()
                .AddTransient<CutSettingViewModel>()
                .BuildServiceProvider());
        }
    }
}