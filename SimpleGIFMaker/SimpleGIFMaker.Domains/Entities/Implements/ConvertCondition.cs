namespace SimpleGIFMaker.Domains
{
    public class ConvertCondition : IConvertCondition
    {
        public static readonly int DefaultGifFrameRate = 10;
        public static readonly double DefaultGifScale = 1d;

        public int Id { get; set; }

        public int RoiX { get; set; } = 0;

        public int RoiY { get; set; } = 0;

        public int RoiWidth { get; set; } = 1;

        public int RoiHeight { get; set; } = 1;

        public TimeSpan StartFrame { get; set; } = TimeSpan.FromSeconds(0);

        public TimeSpan EndFrame { get; set; } = TimeSpan.FromSeconds(1);

        public TimeSpan TrimLength => EndFrame - StartFrame;

        public int GifFrameRate { get; set; } = ConvertCondition.DefaultGifFrameRate;

        public double GifScale { get; set; } = ConvertCondition.DefaultGifScale;

        public int ScaledWidth => (int)(this.RoiWidth * GifScale);

        public int ScaledHeight => (int)(this.RoiHeight * GifScale);

        public ConvertCondition()
        {
        }

        public ConvertCondition(IMovie movie)
        {
            this.RoiX = 0;
            this.RoiY = 0;
            this.RoiWidth = movie.Width;
            this.RoiHeight = movie.Height;
            this.StartFrame = TimeSpan.FromSeconds(0);
            this.EndFrame = movie.FrameLength;
        }

        public void UpdateFrom(IConvertCondition other)
        {
            this.RoiX = other.RoiX;
            this.RoiY = other.RoiY;
            this.RoiWidth = other.RoiWidth;
            this.RoiHeight = other.RoiHeight;
            this.StartFrame = other.StartFrame;
            this.EndFrame = other.EndFrame;
            this.GifFrameRate = other.GifFrameRate;
            this.GifScale = other.GifScale;
        }
    }
}
