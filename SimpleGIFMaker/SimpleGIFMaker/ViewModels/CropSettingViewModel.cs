using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using SimpleGIFMaker.Domains;
using SimpleGIFMaker.Domains.Repositories;

namespace SimpleGIFMaker.ViewModels
{
    internal partial class CropSettingViewModel : ObservableObject
    {
        [ObservableProperty]
        private IConvertCondition? condition;

        [ObservableProperty]
        internal int cropRectX;

        [ObservableProperty]
        internal int cropRectY;

        [ObservableProperty]
        internal int cropRectWidth;

        [ObservableProperty]
        internal int cropRectHeight;

        private readonly IMediaPlayer mediaPlayer;
        private readonly IConvertConditionRepository convertConditionRepository;

        public CropSettingViewModel(IMediaPlayer mediaPlayer, IConvertConditionRepository convertConditionRepository)
        {
            this.mediaPlayer = mediaPlayer;
            this.convertConditionRepository = convertConditionRepository;

            this.mediaPlayer.CropRectChanged += OnCropRectChanged;
        }

        private void OnCropRectChanged(CropRect rect)
        {
            this.CropRectX = rect.X;
            this.CropRectY = rect.Y;
            this.CropRectWidth = rect.Width;
            this.CropRectHeight = rect.Height;
        }

        [RelayCommand]
        internal async Task Loaded()
        {
            var condition = await this.convertConditionRepository.GetConvertConditionAsync(0);
            this.Condition = condition;
        }

        [RelayCommand]
        internal async Task Unloaded()
        {
            if (this.Condition is not null)
            {
                await this.convertConditionRepository.UpdateConvertConditionAsync(0, this.Condition);
            }
        }

    }
}
