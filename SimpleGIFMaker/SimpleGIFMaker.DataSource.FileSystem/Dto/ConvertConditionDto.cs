using SimpleGIFMaker.Domains;

namespace SimpleGIFMaker.DataSource.FileSystem.Dto
{
    internal class ConvertConditionDto
    {
        public int Id { get; set; }

        public int RoiX { get; set; }

        public int RoiY { get; set; }

        public int RoiWidth { get; set; }

        public int RoiHeight { get; set; }

        public double StartFrameSecond { get; set; }

        public double EndFrameSecond { get; set; }

        public int GifFrameRate { get; set; }

        public double GifScale { get; set; }

        internal static ConvertConditionDto CreateFrom(IConvertCondition convertCondition)
        {
            var dto = new ConvertConditionDto()
            {
                Id = convertCondition.Id,
                RoiX = convertCondition.RoiX,
                RoiY = convertCondition.RoiY,
                RoiWidth = convertCondition.RoiWidth,
                RoiHeight = convertCondition.RoiHeight,
                StartFrameSecond = convertCondition.StartFrame.TotalSeconds,
                EndFrameSecond = convertCondition.EndFrame.TotalSeconds,
                GifFrameRate = convertCondition.GifFrameRate,
                GifScale = convertCondition.GifScale,
            };
            return dto;
        }

        internal IConvertCondition Create()
        {
            var condition = new ConvertCondition()
            {
                Id = Id,
                RoiX = RoiX,
                RoiY = RoiY,
                RoiWidth = RoiWidth,
                RoiHeight = RoiHeight,
                StartFrame = TimeSpan.FromSeconds(StartFrameSecond),
                EndFrame = TimeSpan.FromSeconds(EndFrameSecond),
                GifFrameRate = GifFrameRate,
                GifScale = GifScale,
            };

            return condition;
        }
    }
}
