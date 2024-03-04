using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleGIFMaker.Models
{
    internal class Definitions
    {
        [Flags]
        public enum EditModeType
        {
            ConvertSetting = 0b00,
            CropSetting = 0b01,
            CutSetting = 0b10,
        }

        [Flags]
        public enum MediaStateType
        {
            Empty = 0b0000,
            SourceLoaded = 0b0001,
            Pause = SourceLoaded,
            Playing = SourceLoaded | 0b0010,
        }
    }
}
