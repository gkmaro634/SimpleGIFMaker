using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using SimpleGIFMaker.Domains;
using SimpleGIFMaker.Domains.Repositories;
using static SimpleGIFMaker.Models.Definitions;

namespace SimpleGIFMaker.ViewModels
{
    internal partial class MediaViewModel : ObservableObject
    {
        [ObservableProperty]
        private EditModeType editMode = EditModeType.ConvertSetting;

        [ObservableProperty]
        private MediaStateType mediaState = MediaStateType.Empty;

        [ObservableProperty]
        internal int mediaWidth;

        [ObservableProperty]
        internal int mediaHeight;

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

        //[ObservableProperty]
        //internal int cropRectWidth;

        //[ObservableProperty]
        //internal int cropRectHeight;

        [ObservableProperty]
        internal string movieFilePath = string.Empty;

        //[ObservableProperty]
        //private IConvertCondition? convertCondition;

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

            this.MovieFilePath = movie.Path;
            //this.MediaWidth = (movie.Rotation % 180) == 0 ? movie.Width : movie.Height;
            //this.MediaHeight = (movie.Rotation % 180) == 0 ? movie.Height : movie.Width;
            this.MediaWidth = movie.Width;
            this.MediaHeight = movie.Height;
            this.CropRectStartX = 0;
            this.CropRectStartY = 0;
            this.CropRectEndX = this.MediaWidth;
            this.CropRectEndY = this.MediaHeight;
            this.Rotation = movie.Rotation * -1;
            this.CenterX = movie.Width / 2;
            this.CenterY = movie.Height / 2;

            this.MediaState = MediaStateType.SourceLoaded;
        }

        [RelayCommand]
        internal async Task EntryConvertControl()
        {
            //var condition = await this.convertConditionRepository.GetConvertConditionAsync(0);
            //if (condition is null)
            //{
            //    return;
            //}

            //this.ConvertCondition = condition;

            this.EditMode = EditModeType.ConvertSetting;
        }

        [RelayCommand]
        internal async Task EntryCrop()
        {
            //var condition = await this.convertConditionRepository.GetConvertConditionAsync(0);
            //if (condition is null)
            //{
            //    return;
            //}

            //this.ConvertCondition = condition;

            this.EditMode = EditModeType.CropSetting;
        }

        [RelayCommand]
        internal async Task EntryCut()
        {
            //var condition = await this.convertConditionRepository.GetConvertConditionAsync(0);
            //if (condition is null)
            //{
            //    return;
            //}

            //this.ConvertCondition = condition;

            this.EditMode = EditModeType.CutSetting;
        }

        [RelayCommand]
        internal void StartPlayingMovie()
        {
            if (this.MediaState.HasFlag(MediaStateType.SourceLoaded) == false)
            {
                return;
            }

            this.MediaState = MediaStateType.Playing;
        }

        [RelayCommand]
        internal void StopPlayingMovie()
        {
            if (this.MediaState != MediaStateType.Playing)
            {
                return;
            }

            this.MediaState = MediaStateType.Pause;
        }

        [RelayCommand]
        internal void UpdateCropRect(CropRect modifiedCropRect)
        {
            if (modifiedCropRect is not null)
            {
                this.mediaPlayer.UpdateCropRect(modifiedCropRect);
            }
        }
    }
}
