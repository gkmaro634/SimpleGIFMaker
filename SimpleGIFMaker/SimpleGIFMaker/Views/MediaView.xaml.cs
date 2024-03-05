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
        private MediaViewModel vm;

        public MediaView()
        {
            InitializeComponent();

            this.vm = Ioc.Default.GetService<MediaViewModel>();
            this.DataContext = this.vm;
        }
    }
}
