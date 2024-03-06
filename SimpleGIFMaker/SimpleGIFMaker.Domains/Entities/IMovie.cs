namespace SimpleGIFMaker.Domains
{
    public interface IMovie
    {
        int Id { get; set; }

        string Path { get; set; }

        int Width { get; set; }

        int Height { get; set; }

        int FrameCount { get; set; }

        TimeSpan FrameLength { get; set; }

        double FrameRate { get; set; }

        int Rotation { get; set; }

        IGifFile CreateGifFile(IConvertCondition condition, IProgress<double> progress);
    }
}
