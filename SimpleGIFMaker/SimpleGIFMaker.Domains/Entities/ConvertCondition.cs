namespace SimpleGIFMaker.Domains
{
    public class ConvertCondition
    {
        public static readonly int DefaultGifFrameCount = 10;
        public static readonly double DefaultGifScale = 1d;

        public int RoiX { get; set; } = 0;

        public int RoiY { get; set; } = 0;

        public int RoiWidth { get; set; } = 1;

        public int RoiHeight { get; set; } = 1;

        public int StartFrame { get; set; } = 0;

        public int EndFrame { get; set; } = 1;

        public int GifFrameCount { get; set; } = ConvertCondition.DefaultGifFrameCount;

        public double GifScale { get; set; } = ConvertCondition.DefaultGifScale;

        //public int GifWidth { get; set; }

        //public int GifHeight { get; set; }

        //public double GifFrameRate { get; set; }

        public ConvertCondition()
        {
        }

        public ConvertCondition(Movie movie)
        {
            this.RoiX = 0;
            this.RoiY = 0;
            this.RoiWidth = movie.Width;
            this.RoiHeight = movie.Height;
            this.StartFrame = 0;
            this.EndFrame = movie.FrameCount;
        }

        public void UpdateFrom(ConvertCondition other)
        {
            this.RoiX = other.RoiX;
            this.RoiY = other.RoiY;
            this.RoiWidth = other.RoiWidth;
            this.RoiHeight = other.RoiHeight;
            this.StartFrame = other.StartFrame;
            this.EndFrame = other.EndFrame;
            this.GifFrameCount = other.GifFrameCount;
            this.GifScale = other.GifScale;
        }
    }
}
