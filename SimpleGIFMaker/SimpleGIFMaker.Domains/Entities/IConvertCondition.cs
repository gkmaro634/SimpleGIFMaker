namespace SimpleGIFMaker.Domains
{
    public interface IConvertCondition
    {
        int Id { get; set; }

        int RoiX { get; set; }

        int RoiY { get; set; }

        int RoiWidth { get; set; }

        int RoiHeight { get; set; }

        TimeSpan StartFrame { get; set; }

        TimeSpan EndFrame { get; set; }

        TimeSpan TrimLength => EndFrame - StartFrame;

        int GifFrameRate { get; set; }

        double GifScale { get; set; }

        int ScaledWidth => (int)(this.RoiWidth * GifScale);

        int ScaledHeight => (int)(this.RoiHeight * GifScale);

        void UpdateFrom(IConvertCondition other);
    }
}
