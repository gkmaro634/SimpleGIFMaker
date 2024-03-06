using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using SimpleGIFMaker.Domains;
using SimpleGIFMaker.Domains.Repositories;
using SimpleGIFMaker.Models;

namespace SimpleGIFMaker.ViewModels
{
    internal partial class CutSettingViewModel : ObservableObject
    {
        internal IConvertCondition? condition;

        [ObservableProperty]
        internal string startText = string.Empty;

        [ObservableProperty]
        internal string endText = string.Empty;

        [ObservableProperty]
        internal List<SelectableItem> gifFrameRateItems = new();

        [ObservableProperty]
        internal SelectableItem selectedGifFrameRate;

        private TimeSpan start = TimeSpan.Zero;
        private TimeSpan end = TimeSpan.Zero;

        private readonly IMediaPlayer mediaPlayer;
        private readonly IConvertConditionRepository convertConditionRepository;

        public CutSettingViewModel(IMediaPlayer mediaPlayer, IConvertConditionRepository convertConditionRepository)
        {
            this.mediaPlayer = mediaPlayer;
            this.convertConditionRepository = convertConditionRepository;

            var items = Enumerable.Range(0, 30).Select(v =>
            {
                var frameRate = 30 - v;
                var label = $"{frameRate}";
                return new SelectableItem(label, frameRate);
            });
            this.gifFrameRateItems.AddRange(items);
            this.SelectedGifFrameRate = this.GifFrameRateItems[0];

            this.mediaPlayer.CutRangeChanged += OnCutRangeChanged;
        }

        private void OnCutRangeChanged(CutRange range)
        {
            this.start = range.Start;
            this.end = range.End;

            this.StartText = range.Start.ToString(@"hh\:mm\:ss");
            this.EndText = range.End.ToString(@"hh\:mm\:ss");
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

            this.start = condition.StartFrame;
            this.end = condition.EndFrame;

            this.StartText = condition.StartFrame.ToString(@"hh\:mm\:ss");
            this.EndText = condition.EndFrame.ToString(@"hh\:mm\:ss");

            this.SelectedGifFrameRate = this.GifFrameRateItems.OrderBy(item => Math.Abs(item.Value - condition.GifFrameRate)).First();
        }

        [RelayCommand]
        internal async Task Unloaded()
        {
            if (this.condition is null)
            {
                return;
            }

            this.condition.StartFrame = this.start;
            this.condition.EndFrame = this.end;
            this.condition.GifFrameRate = (int)this.SelectedGifFrameRate.Value;

            await this.convertConditionRepository.UpdateConvertConditionAsync(0, this.condition);
        }

    }
}
