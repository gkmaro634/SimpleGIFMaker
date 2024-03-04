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
        private IConvertCondition? convertCondition;

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

            // TODO: cache movie instance to bind

            this.MediaState = MediaStateType.SourceLoaded;
        }

        [RelayCommand]
        internal async Task EntryCrop()
        {
            var condition = await this.convertConditionRepository.GetConvertConditionAsync(0);
            if (condition is null)
            {
                return;
            }

            this.ConvertCondition = condition;

            this.EditMode = EditModeType.CropSetting;
        }

        [RelayCommand]
        internal async Task EntryConvertControl()
        {
            var condition = await this.convertConditionRepository.GetConvertConditionAsync(0);
            if (condition is null)
            {
                return;
            }

            this.ConvertCondition = condition;

            this.EditMode = EditModeType.ConvertSetting;
        }
    }
}
