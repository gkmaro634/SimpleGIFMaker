using SimpleGIFMaker.Domains;

namespace SimpleGIFMaker.DataSource.FileSystem.Dto
{
    internal class MovieDto
    {
        public int Id { get; set; }

        public string Path { get; set; } = string.Empty;

        public int Width { get; set; }

        public int Height { get; set; }

        public int FrameCount { get; set; }

        public double FrameLengthSecond { get; set; }

        public double FrameRate { get; set; }

        public int Rotation { get; set; }

        internal static MovieDto CreateFrom(IMovie movie)
        {
            var dto = new MovieDto()
            {
                Id = movie.Id,
                Path = movie.Path,
                Width = movie.Width,
                Height = movie.Height,
                FrameCount = movie.FrameCount,
                FrameLengthSecond = movie.FrameLength.TotalSeconds,
                FrameRate = movie.FrameRate,
                Rotation = movie.Rotation,
            };

            return dto;
        }

        internal IMovie Create()
        {
            var movie = new Movie()
            {
                Id = Id,
                Path = Path,
                Width = Width,
                Height = Height,
                FrameCount = FrameCount,
                FrameLength = TimeSpan.FromSeconds(FrameLengthSecond),
                FrameRate = FrameRate,
                Rotation = Rotation,
            };
            return movie;
        }
    }
}
