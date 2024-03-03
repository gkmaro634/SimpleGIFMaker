using CommunityToolkit.Mvvm.ComponentModel;
using SimpleGIFMaker.Domains.Repositories;

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
