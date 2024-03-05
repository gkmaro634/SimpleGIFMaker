using CommunityToolkit.Mvvm.DependencyInjection;
using SimpleGIFMaker.ViewModels;
using System.Windows;

namespace SimpleGIFMaker.Views
{
    /// <summary>
    /// Interaction logic for ConvertResultWindow.xaml
    /// </summary>
    public partial class ConvertResultWindow : Window
    {
        private ConvertResultWindowViewModel vm;

        public ConvertResultWindow()
        {
            InitializeComponent();

            this.vm = Ioc.Default.GetService<ConvertResultWindowViewModel>()!;
            this.DataContext = this.vm;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            this.vm.LoadedCommand.Execute("");
        }

        private void Window_Unloaded(object sender, RoutedEventArgs e)
        {
            this.vm.UnloadedCommand.Execute("");
        }
    }
}