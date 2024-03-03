using CommunityToolkit.Mvvm.ComponentModel;
using SimpleGIFMaker.Domains.Repositories;

namespace SimpleGIFMaker.ViewModels
{
    internal partial class CropSettingViewModel : ObservableObject
    {
        private readonly IConvertConditionRepository convertConditionRepository;

        public CropSettingViewModel(IConvertConditionRepository convertConditionRepository)
        {
            this.convertConditionRepository = convertConditionRepository;
        }
    }
}
