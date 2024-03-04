using CommunityToolkit.Mvvm.DependencyInjection;
using SimpleGIFMaker.Models;
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

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e.AddedItems.Count > 0 && this.vm.Condition is not null)
            {
                var selected = e.AddedItems[0] as ScaleSelectItem;
                this.vm.UpdateGifScaleCommand.Execute(selected!.Value);
            }
        }
    }
}
