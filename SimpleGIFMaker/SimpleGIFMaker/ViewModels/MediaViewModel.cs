using CommunityToolkit.Mvvm.ComponentModel;
using SimpleGIFMaker.Domains.Repositories;
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
        private readonly IMovieRepository movieRepository;

        [ObservableProperty]
        private string debug;

        public MediaViewModel(IMovieRepository movieRepository) 
        {
            debug = "Test";
            this.movieRepository = movieRepository;
        }
    }
}
