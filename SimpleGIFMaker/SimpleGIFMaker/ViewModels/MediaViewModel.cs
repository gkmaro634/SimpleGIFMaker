using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using SimpleGIFMaker.Domains;
using SimpleGIFMaker.Domains.Repositories;
using System.Windows;
using static SimpleGIFMaker.Models.Definitions;

namespace SimpleGIFMaker.ViewModels
{
    internal partial class MediaViewModel : ObservableObject
    {
        [ObservableProperty]
        private EditModeType editMode = EditModeType.ConvertSetting;

        [ObservableProperty]
        [NotifyCanExecuteChangedFor(nameof(StartPlayingMovieCommand))]
        [NotifyCanExecuteChangedFor(nameof(StopPlayingMovieCommand))]
        private MediaStateType mediaState = MediaStateType.Empty;

        [ObservableProperty]
        internal IMovie? currentMovie;

        [ObservableProperty]
        internal int cropRectStartX;

        [ObservableProperty]
        internal int cropRectStartY;

        [ObservableProperty]
        internal int cropRectEndX;

        [ObservableProperty]
        internal int cropRectEndY;

        [ObservableProperty]
        internal int rotation;

        [ObservableProperty]
        internal int centerX;

        [ObservableProperty]
        internal int centerY;

        [ObservableProperty]
        internal double scale = 1d;

        [ObservableProperty]
        internal double scaleInv = 1d;

        [ObservableProperty]
        internal int selectedTabIndex = 0;

        [ObservableProperty]
        internal double startPosition = 0d;

        [ObservableProperty]
        internal double endPosition = 0d;

        private readonly IMediaPlayer mediaPlayer;
        private readonly IMovieRepository movieRepository;
        private readonly IConvertConditionRepository convertConditionRepository;

        public MediaViewModel(IMediaPlayer mediaPlayer, IMovieRepository movieRepository, IConvertConditionRepository convertConditionRepository)
        {
            this.mediaPlayer = mediaPlayer;
            this.movieRepository = movieRepository;
            this.convertConditionRepository = convertConditionRepository;

            this.mediaPlayer.MovieChanged += this.OnMovieChanged;
        }

        private void OnMovieChanged(IMovie movie)
        {
            if (movie is null)
            {
                return;
            }

            if (this.MediaState == MediaStateType.Playing)
            {
                this.StopPlayingMovie();
            }

            this.CurrentMovie = movie;
            this.CropRectStartX = 0;
            this.CropRectStartY = 0;
            this.CropRectEndX = movie.Width;
            this.CropRectEndY = movie.Height;
            this.Rotation = movie.Rotation * -1;
            this.CenterX = movie.Width / 2;
            this.CenterY = movie.Height / 2;

            this.MediaState = MediaStateType.SourceLoaded;
        }

        [RelayCommand]
        internal async Task SelectTab()
        {
            if (this.SelectedTabIndex == 0)
            {
                await this.EntryConvertControl();
            }
            else if (this.SelectedTabIndex == 1)
            {
                await this.EntryCrop();
            }
            else if (this.SelectedTabIndex == 2)
            {
                await this.EntryCut();
            }
            else
            {
                return;
            }
        }

        internal async Task EntryConvertControl()
        {
            //var condition = await this.convertConditionRepository.GetConvertConditionAsync(0);
            //if (condition is null)
            //{
            //    return;
            //}

            //this.ConvertCondition = condition;

            this.EditMode = EditModeType.ConvertSetting;
            await Task.CompletedTask;
        }

        internal async Task EntryCrop()
        {
            //var condition = await this.convertConditionRepository.GetConvertConditionAsync(0);
            //if (condition is null)
            //{
            //    return;
            //}

            //this.ConvertCondition = condition;

            this.EditMode = EditModeType.CropSetting;
            await Task.CompletedTask;
        }

        internal async Task EntryCut()
        {
            //var condition = await this.convertConditionRepository.GetConvertConditionAsync(0);
            //if (condition is null)
            //{
            //    return;
            //}

            //this.ConvertCondition = condition;

            this.EditMode = EditModeType.CutSetting;
            await Task.CompletedTask;
        }

        [RelayCommand(CanExecute = nameof(CanStartPlaying))]
        internal void StartPlayingMovie()
        {
            if (this.MediaState.HasFlag(MediaStateType.SourceLoaded) == false)
            {
                return;
            }

            this.MediaState = MediaStateType.Playing;
        }

        private bool CanStartPlaying()
        {
            return this.MediaState.HasFlag(MediaStateType.SourceLoaded);
        }

        [RelayCommand(CanExecute = nameof(CanStopPlaying))]
        internal void StopPlayingMovie()
        {
            if (this.MediaState != MediaStateType.Playing)
            {
                return;
            }

            this.MediaState = MediaStateType.Pause;
        }

        private bool CanStopPlaying()
        {
            return this.MediaState.HasFlag(MediaStateType.Playing);
        }

        /// <summary>
        /// クロップ範囲更新
        /// </summary>
        /// <param name="modifiedCropRect"></param>
        /// <remarks>
        /// ドラッグ操作完了時に呼び出される
        /// </remarks>
        [RelayCommand]
        internal void UpdateCropRect()
        {
            if (this.CurrentMovie is null)
            {
                return;
            }

            var width = this.CropRectEndX - this.CropRectStartX;
            var height = this.CropRectEndY - this.CropRectStartY;
            var x = this.CropRectStartX;
            var y = this.CropRectStartY;
            var modifiedCropRect = new CropRect(x, y, width, height);
            this.mediaPlayer.UpdateCropRect(modifiedCropRect);
        }

        /// <summary>
        /// 拡大縮小率更新
        /// </summary>
        /// <param name="e"></param>
        /// <remarks>
        /// ViewBoxのSizeChangedEventで呼び出される
        /// </remarks>
        [RelayCommand]
        internal void UpdateScale(SizeChangedEventArgs e)
        {
            if (this.CurrentMovie is not null)
            {
                var scaledWidth = e.NewSize.Width;
                this.Scale = scaledWidth / this.CurrentMovie.Width;
                this.ScaleInv = 1d / this.Scale;
            }
        }

        [RelayCommand]
        internal void UpdateCutRange()
        {
            if (this.CurrentMovie is not null)
            {
                var length = this.CurrentMovie.FrameLength.TotalSeconds;
                var start = this.StartPosition * length;
                var end = this.EndPosition * length;
                var modifiedCutRange = new CutRange(TimeSpan.FromSeconds(start), TimeSpan.FromSeconds(end));
                this.mediaPlayer.UpdateCutRange(modifiedCutRange);
            }
        }

    }
}
