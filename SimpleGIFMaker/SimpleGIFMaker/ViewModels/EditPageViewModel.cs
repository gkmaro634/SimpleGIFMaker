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
    public partial class EditPageViewModel
    {
        [ObservableProperty]
        private string name = "Edit";

        [RelayCommand]
        private void Back()
        {
            Shell.Current.GoToAsync(nameof(ContentGalleryPage));
        }
    }
}
