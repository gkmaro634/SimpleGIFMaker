using CommunityToolkit.Mvvm.DependencyInjection;
using SimpleGIFMaker.ViewModels;
using System.Windows.Controls;

namespace SimpleGIFMaker.Views
{
    /// <summary>
    /// MediaView.xaml の相互作用ロジック
    /// </summary>
    public partial class MediaView : UserControl
    {
        public MediaView()
        {
            InitializeComponent();

            this.DataContext = Ioc.Default.GetService<MediaViewModel>();
        }
    }
}
