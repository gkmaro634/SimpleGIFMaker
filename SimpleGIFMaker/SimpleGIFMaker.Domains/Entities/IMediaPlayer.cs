using System.Diagnostics;

namespace SimpleGIFMaker.Domains
{
    public interface IMediaPlayer
    {
        Action<IMovie> MovieChanged { get; set; }

        Action<CropRect> CropRectChanged { get; set; }

        Action<CutRange> CutRangeChanged { get; set; }

        void SetMovie(IMovie movie);

        IMovie? GetCurrentMovie();

        void UpdateCropRect(CropRect cropRect);

        CropRect GetCurrentCropRect();

        void UpdateCutRange(CutRange cutRange);

        CutRange GetCurrentCutRange();

    }
}
