namespace SimpleGIFMaker.Domains
{
    public class Movie
    {
        public string Path { get; set; }

        public int Width { get; set; }

        public int Height { get; set; }

        //public TimeSpan TimeRange { get; set; }

        public int FrameCount { get; set; }

        public double FrameRate { get; set; }

        public Movie(string path)
        {
            this.Path = path;

            // TODO: load movie info
            // TODO: set width, height, frameCount,,,
        }

        public GifFile CreateGifFile(ConvertCondition condition, IProgress<double> progress)
        {
            // todo: valid file path
            var gif = new GifFile("dummy/path");
            return gif;
        }
    }
}
