using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleGIFMaker.ViewModels
{
    internal partial class MediaViewModel : ObservableObject
    {
        [ObservableProperty]
        private string debug;

        public MediaViewModel() 
        {
            debug = "Test";
        }
    }
}
