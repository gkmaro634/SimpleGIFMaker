using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommunityToolkit.Maui;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using SimpleGIFMaker.Views;

namespace SimpleGIFMaker.ViewModels
{
    [INotifyPropertyChanged]
    public partial class ContentGalleryPageViewModel
    {
        [ObservableProperty]
        private string name = "Garllery";

        [RelayCommand]
        private void Back()
        {
            Shell.Current.GoToAsync(nameof(DirectoryExplorerPage));
        }

        [RelayCommand]
        private void SelectContent()
        {
            Shell.Current.GoToAsync(nameof(EditPage));
        }

    }
}
