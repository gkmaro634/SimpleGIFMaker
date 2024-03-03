using CommunityToolkit.Mvvm.DependencyInjection;
using SimpleGIFMaker.ViewModels;
using System.Windows.Controls;

namespace SimpleGIFMaker.Views
{
    /// <summary>
    /// ConvertControlView.xaml の相互作用ロジック
    /// </summary>
    public partial class ConvertControlView : UserControl
    {
        public ConvertControlView()
        {
            InitializeComponent();

            Ioc.Default.GetService<ConvertControlViewModel>();

        }
    }
}
