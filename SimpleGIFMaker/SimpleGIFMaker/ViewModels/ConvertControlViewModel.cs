using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.WindowsAPICodePack.Dialogs;
using SimpleGIFMaker.Domains;
using SimpleGIFMaker.Domains.Repositories;


namespace SimpleGIFMaker.ViewModels
{
    internal partial class ConvertControlViewModel : ObservableObject
    {
        private readonly IMediaPlayer mediaPlayer;
        private readonly IMovieRepository movieRepository;
        private readonly IConvertConditionRepository convertConditionRepository;
        private readonly IGifFileRepository gifFileRepository;

        internal Func<IMovie?> selectMovieFileFunc;

        public ConvertControlViewModel(
            IMediaPlayer mediaPlayer,
            IMovieRepository movieRepository,
            IConvertConditionRepository convertConditionRepository, 
            IGifFileRepository gifFileRepository)
        {
            this.mediaPlayer = mediaPlayer;
            this.movieRepository = movieRepository;
            this.convertConditionRepository = convertConditionRepository;
            this.gifFileRepository = gifFileRepository;

            this.selectMovieFileFunc = this.SelectMovieFile;
        }

        internal IMovie? SelectMovieFile()
        {
            using (var dialog = new CommonOpenFileDialog())
            {
                var result = dialog.ShowDialog();
                if (result != CommonFileDialogResult.Ok)
                {
                    return null;
                }

                var filePath = dialog.FileName;
                var movie = new Movie(filePath);
                return movie;
            }
        }

        [RelayCommand]
        internal async Task SelectFile()
        {
            var movie = this.selectMovieFileFunc.Invoke();
            if (movie is null)
            {
                return;
            }

            var condition = new ConvertCondition(movie);
            await this.convertConditionRepository.AddConvertConditionAsync(condition);
            await this.movieRepository.AddMovieAsync(movie);

            this.mediaPlayer.SetMovie(movie);
        }

        [RelayCommand(CanExecute = nameof(CanConvert))]
        internal async Task ExecConvert()
        {
            var movie = await this.movieRepository.GetMovieAsync(0);
            if (movie is null)
            {
                return;
            }

            var condition = await this.convertConditionRepository.GetConvertConditionAsync(0);
            if (condition is null)
            {
                return;
            }

            var progress = new Progress<double>();
            var gif = movie.CreateGifFile(condition, progress);

            await this.gifFileRepository.AddGifFileAsync(gif);
        }

        private bool CanConvert()
        {
            var movie = this.mediaPlayer.GetCurrentMovie();
            return movie is object;
        }
    }
}
