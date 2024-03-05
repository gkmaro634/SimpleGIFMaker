namespace SimpleGIFMaker.Domains
{
    public class MediaPlayer : IMediaPlayer
    {
        public Action<IMovie> MovieChanged { get; set; } = (_) => { };
        public Action<CropRect> CropRectChanged { get; set; } = (_) => { };
        public Action<CutRange> CutRangeChanged { get; set; } = (_) => { };

        private IMovie? movie;
        private CropRect cropRect = new CropRect(0, 1, 1, 1);
        private CutRange cutRange = new CutRange(TimeSpan.Zero, TimeSpan.FromSeconds(1));

        public MediaPlayer()
        {
        }

        public void SetMovie(IMovie movie)
        {
            this.movie = movie;
            var cropRect = new CropRect(0, 0, movie.Width, movie.Height);
            this.UpdateCropRect(cropRect);

            var cutRange = new CutRange(TimeSpan.Zero, this.movie.FrameLength);
            this.UpdateCutRange(cutRange);

            this.MovieChanged?.Invoke(this.movie);
        }

        public IMovie? GetCurrentMovie()
        {
            return this.movie;
        }

        public void UpdateCropRect(CropRect cropRect)
        {
            this.cropRect.X = cropRect.X;
            this.cropRect.Y = cropRect.Y;
            this.cropRect.Width = cropRect.Width;
            this.cropRect.Height = cropRect.Height;

            this.CropRectChanged?.Invoke(this.cropRect);
        }

        public CropRect GetCurrentCropRect()
        {
            return this.cropRect;
        }

        public void UpdateCutRange(CutRange cutRange)
        {
            this.cutRange.Start = cutRange.Start;
            this.cutRange.End = cutRange.End;

            this.CutRangeChanged?.Invoke(this.cutRange);
        }

        public CutRange GetCurrentCutRange()
        {
            return this.cutRange;
        }
    }
}
