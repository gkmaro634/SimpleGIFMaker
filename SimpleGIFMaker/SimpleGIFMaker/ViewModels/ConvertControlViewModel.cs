using CommunityToolkit.Mvvm.ComponentModel;
using SimpleGIFMaker.Domains.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleGIFMaker.ViewModels
{
    internal partial class ConvertControlViewModel : ObservableObject
    {
        private readonly IConvertConditionRepository convertConditionRepository;
        private readonly IGifFileRepository gifFileRepository;

        public ConvertControlViewModel(IConvertConditionRepository convertConditionRepository, IGifFileRepository gifFileRepository)
        {
            this.convertConditionRepository = convertConditionRepository;
            this.gifFileRepository = gifFileRepository;
        }
    }
}
