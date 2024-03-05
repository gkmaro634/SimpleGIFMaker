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
        private ConvertControlViewModel vm;

        public ConvertControlView()
        {
            InitializeComponent();

            this.vm = Ioc.Default.GetService<ConvertControlViewModel>()!;
            this.DataContext = this.vm;
        }
    }
}
