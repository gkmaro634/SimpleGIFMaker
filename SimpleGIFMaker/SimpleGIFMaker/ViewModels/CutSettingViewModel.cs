using CommunityToolkit.Mvvm.ComponentModel;
using SimpleGIFMaker.Domains.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleGIFMaker.ViewModels
{
    internal partial class CutSettingViewModel : ObservableObject
    {
        private readonly IConvertConditionRepository convertConditionRepository;

        public CutSettingViewModel(IConvertConditionRepository convertConditionRepository)
        {
            this.convertConditionRepository = convertConditionRepository;
        }
    }
}
