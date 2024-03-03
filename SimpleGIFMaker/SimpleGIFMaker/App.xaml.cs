using System.Configuration;
using System.Data;
using System.Net.Http;
using System.Windows;
using CommunityToolkit.Mvvm.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;
using SimpleGIFMaker.ViewModels;

namespace SimpleGIFMaker
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        ///// <summary>
        ///// Gets the current <see cref="App"/> instance in use
        ///// </summary>
        //public new static App Current => (App)Application.Current;

        ///// <summary>
        ///// Gets the <see cref="IServiceProvider"/> instance to resolve application services.
        ///// </summary>
        //public IServiceProvider Services { get; }

        public App()
        {
            Ioc.Default.ConfigureServices(
                new ServiceCollection()
                .AddTransient<MediaViewModel>()
                .AddTransient<ConvertControlViewModel>()
                .AddTransient<CropSettingViewModel>()
                .AddTransient<CutSettingViewModel>()
                .BuildServiceProvider());
        }
        //public App()
        //{
        //    Services = ConfigureServices();

        //    this.InitializeComponent();
        //}

        ///// <summary>
        ///// Configures the services for the application.
        ///// </summary>
        //private static IServiceProvider ConfigureServices()
        //{
        //    var services = new ServiceCollection();

        //    //services.AddSingleton<IFilesService, FilesService>();
        //    //services.AddSingleton<ISettingsService, SettingsService>();
        //    //services.AddSingleton<IClipboardService, ClipboardService>();
        //    //services.AddSingleton<IShareService, ShareService>();
        //    //services.AddSingleton<IEmailService, EmailService>();

        //    return services.BuildServiceProvider();
        //}
    }
}