using System.Diagnostics;

namespace SimpleGIFMaker.Domains
{
    public class MediaPlayer : IMediaPlayer
    {
        public Action<IMovie> MovieChanged { get; set; } = (_) => { };

        private IMovie movie;

        public MediaPlayer()
        {
        }

        public void SetMovie(IMovie movie)
        {
            this.movie = movie;

            this.MovieChanged?.Invoke(this.movie);
        }

        public IMovie? GetCurrentMovie()
        {
            return this.movie;
        }
    }
}
