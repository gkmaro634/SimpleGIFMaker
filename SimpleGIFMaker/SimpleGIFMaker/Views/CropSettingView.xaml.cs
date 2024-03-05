using CommunityToolkit.Mvvm.DependencyInjection;
using SimpleGIFMaker.ViewModels;
using System.Windows.Controls;

namespace SimpleGIFMaker.Views
{
    /// <summary>
    /// CropSettingView.xaml の相互作用ロジック
    /// </summary>
    public partial class CropSettingView : UserControl
    {
        private CropSettingViewModel vm;

        public CropSettingView()
        {
            InitializeComponent();

            this.vm = Ioc.Default.GetService<CropSettingViewModel>()!;
            this.DataContext = vm;
        }

        private void UserControl_Loaded(object sender, System.Windows.RoutedEventArgs e)
        {
            this.vm.LoadedCommand.Execute(string.Empty);
        }

        private void UserControl_Unloaded(object sender, System.Windows.RoutedEventArgs e)
        {
            this.vm.UnloadedCommand.Execute(string.Empty);
        }
    }
}
