using System.Diagnostics;

namespace SimpleGIFMaker.Domains
{
    public interface IMediaPlayer
    {
        Action<IMovie> MovieChanged { get; set; }

        void SetMovie(IMovie movie);

        IMovie? GetCurrentMovie();
    }
}
