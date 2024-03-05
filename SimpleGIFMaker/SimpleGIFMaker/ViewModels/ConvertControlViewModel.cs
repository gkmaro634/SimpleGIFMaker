using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.WindowsAPICodePack.Dialogs;
using SimpleGIFMaker.Domains;
using SimpleGIFMaker.Domains.Repositories;
using SimpleGIFMaker.Views;
using static SimpleGIFMaker.Models.Definitions;

namespace SimpleGIFMaker.ViewModels
{
    internal partial class ConvertControlViewModel : ObservableObject
    {
        private readonly IMediaPlayer mediaPlayer;
        private readonly IMovieRepository movieRepository;
        private readonly IConvertConditionRepository convertConditionRepository;
        private readonly IGifFileRepository gifFileRepository;

        [ObservableProperty]
        private string filePath = string.Empty;

        [ObservableProperty]
        [NotifyCanExecuteChangedFor(nameof(ExecConvertCommand))]
        private MediaStateType mediaState = MediaStateType.Empty;

        internal Func<IMovie?> selectMovieFileFunc;

        internal Action showResultWindowAction = () => { };

        public IAsyncRelayCommand LoadedCommand { get; private set; }
        public IAsyncRelayCommand UnloadedCommand { get; private set; }

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
            this.showResultWindowAction = this.ShowConvertResult;

            this.LoadedCommand = new AsyncRelayCommand(this.Loaded);
            this.UnloadedCommand = new AsyncRelayCommand(this.Unloaded);
        }

        //[RelayCommand]
        internal async Task Loaded()
        {
            await Task.CompletedTask;
        }

        //[RelayCommand]
        internal async Task Unloaded()
        {
            await Task.CompletedTask;
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

            this.FilePath = movie.Path;

            var condition = new ConvertCondition(movie);

            await this.convertConditionRepository.AddConvertConditionAsync(condition);
            await this.movieRepository.AddMovieAsync(movie);

            this.mediaPlayer.SetMovie(movie);

            this.MediaState = MediaStateType.SourceLoaded;
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
            if (gif is null)
            {
                return;
            }

            await this.gifFileRepository.AddGifFileAsync(gif);

            this.showResultWindowAction?.Invoke();
        }

        internal void ShowConvertResult()
        {
            var resultWindow = new ConvertResultWindow();
            _ = resultWindow.ShowDialog();
        }

        private bool CanConvert()
        {
            return this.MediaState.HasFlag(MediaStateType.SourceLoaded);
        }
    }
}
