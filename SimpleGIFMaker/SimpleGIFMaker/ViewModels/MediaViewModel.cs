using CommunityToolkit.Mvvm.ComponentModel;
using SimpleGIFMaker.Domains;
using SimpleGIFMaker.Domains.Repositories;

namespace SimpleGIFMaker.ViewModels
{
    internal partial class MediaViewModel : ObservableObject
    {
        [Flags]
        public enum StateType
        {
            Idle = 0b00000000,
            SourceLoaded = 0b00000001,
            ConditionEdit = 0b00000010,
            CropSettingEdit = SourceLoaded | ConditionEdit | 0b00000100,
            CutSettingEdit = SourceLoaded | ConditionEdit | 0b00001000,
            PlayingMedia = SourceLoaded | 0b00010000,
        }

        [ObservableProperty]
        private StateType state = StateType.Idle;

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
        }

        internal async void EntryCrop()
        {
            var condition = await this.convertConditionRepository.GetConvertConditionAsync(0);
            if (condition is null)
            {
                return;
            }

            this.ConvertCondition = condition;

            this.State = StateType.CropSettingEdit;
        }
    }
}
