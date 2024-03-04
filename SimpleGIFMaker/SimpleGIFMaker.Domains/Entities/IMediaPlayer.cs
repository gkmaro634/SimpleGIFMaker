using System.Diagnostics;

namespace SimpleGIFMaker.Domains
{
    public interface IMediaPlayer
    {
        Action<IMovie> MovieChanged { get; set; }

        Action<CropRect> CropRectChanged { get; set; }

        void SetMovie(IMovie movie);

        IMovie? GetCurrentMovie();

        void UpdateCropRect(CropRect cropRect);

        CropRect GetCurrentCropRect();
    }
}
