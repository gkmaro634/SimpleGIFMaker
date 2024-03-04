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

        private readonly IConvertConditionRepository convertConditionRepository;

        public CropSettingViewModel(IConvertConditionRepository convertConditionRepository)
        {
            this.convertConditionRepository = convertConditionRepository;
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
