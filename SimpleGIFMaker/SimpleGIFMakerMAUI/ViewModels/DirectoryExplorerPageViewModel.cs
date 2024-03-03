using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using SimpleGIFMaker.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleGIFMaker.ViewModels
{
    [INotifyPropertyChanged]
    public partial class DirectoryExplorerPageViewModel
    {
        [ObservableProperty]
        string name = "Explorer";

        [RelayCommand]
        private void SelectDirectory()
        {
            Shell.Current.GoToAsync(nameof(ContentGalleryPage));
        }
    }
}
