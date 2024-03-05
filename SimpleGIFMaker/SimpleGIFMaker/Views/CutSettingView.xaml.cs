using CommunityToolkit.Mvvm.DependencyInjection;
using SimpleGIFMaker.ViewModels;
using System.Windows.Controls;

namespace SimpleGIFMaker.Views
{
    /// <summary>
    /// CutSettingView.xaml の相互作用ロジック
    /// </summary>
    public partial class CutSettingView : UserControl
    {
        private CutSettingViewModel? vm;

        public CutSettingView()
        {
            InitializeComponent();

            this.vm = Ioc.Default.GetService<CutSettingViewModel>();
            this.DataContext = this.vm;
        }

        private void UserControl_Loaded(object sender, System.Windows.RoutedEventArgs e)
        {
            this.vm?.LoadedCommand.Execute(string.Empty);
        }

        private void UserControl_Unloaded(object sender, System.Windows.RoutedEventArgs e)
        {
            this.vm?.UnloadedCommand.Execute(string.Empty);
        }
    }
}
