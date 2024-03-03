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
        public CropSettingView()
        {
            InitializeComponent();

            this.DataContext = Ioc.Default.GetService<CropSettingViewModel>();
        }
    }
}
