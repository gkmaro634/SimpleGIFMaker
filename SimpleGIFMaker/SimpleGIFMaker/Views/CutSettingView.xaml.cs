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
        public CutSettingView()
        {
            InitializeComponent();

            this.DataContext = Ioc.Default.GetService<CutSettingViewModel>();
        }
    }
}
