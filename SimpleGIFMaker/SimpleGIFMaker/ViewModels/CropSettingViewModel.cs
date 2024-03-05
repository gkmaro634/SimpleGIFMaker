using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using SimpleGIFMaker.Domains;
using SimpleGIFMaker.Domains.Repositories;
using SimpleGIFMaker.Models;

namespace SimpleGIFMaker.ViewModels
{
    internal partial class CropSettingViewModel : ObservableObject
    {
        //[ObservableProperty]
        internal IConvertCondition? condition;

        [ObservableProperty]
        internal int cropRectX;

        [ObservableProperty]
        internal int cropRectY;

        [ObservableProperty]
        internal int cropRectWidth;

        [ObservableProperty]
        internal int cropRectHeight;

        [ObservableProperty]
        internal List<ScaleSelectItem> scaleSelectItems = new();

        [ObservableProperty]
        internal ScaleSelectItem selectedScale;

        private readonly IMediaPlayer mediaPlayer;
        private readonly IConvertConditionRepository convertConditionRepository;

        public CropSettingViewModel(IMediaPlayer mediaPlayer, IConvertConditionRepository convertConditionRepository)
        {
            this.mediaPlayer = mediaPlayer;
            this.convertConditionRepository = convertConditionRepository;

            var items = Enumerable.Range(1, 10).Select(v =>
            {
                var label = v == 1 ? "1" : $"1/{v}";
                return new ScaleSelectItem(label, 1d / (double)v);
            });
            this.scaleSelectItems.AddRange(items);
            this.SelectedScale = this.ScaleSelectItems[0];

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
            if (condition is null)
            {
                return;
            }

            this.condition = condition;

            this.CropRectX = condition!.RoiX;
            this.CropRectY = condition!.RoiY;
            this.CropRectWidth = condition!.RoiWidth;
            this.CropRectHeight = condition!.RoiHeight;
            this.SelectedScale = this.ScaleSelectItems.OrderBy(item => Math.Abs(item.Value - condition.GifScale)).First();
        }

        [RelayCommand]
        internal async Task Unloaded()
        {
            if (this.condition is null)
            {
                return;
            }

            this.condition.RoiX = this.CropRectX;
            this.condition.RoiY = this.CropRectY;
            this.condition.RoiWidth = this.CropRectWidth;
            this.condition.RoiHeight = this.CropRectHeight;
            this.condition.GifScale = this.SelectedScale.Value;

            await this.convertConditionRepository.UpdateConvertConditionAsync(0, this.condition);
        }

    }
}
