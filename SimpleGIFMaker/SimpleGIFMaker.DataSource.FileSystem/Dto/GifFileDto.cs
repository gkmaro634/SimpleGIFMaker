using SimpleGIFMaker.Domains;

namespace SimpleGIFMaker.DataSource.FileSystem.Dto
{
    internal class GifFileDto
    {
        public int Id { get; set; }

        public string Path { get; set; } = string.Empty;

        public int Width { get; set; }

        public int Height { get; set; }

        public double FrameRate { get; set; }

        internal static GifFileDto CreateFrom(IGifFile gifFile)
        {
            var dto = new GifFileDto()
            {
                Id = gifFile.Id,
                Path = gifFile.Path,
                Width = gifFile.Width,
                Height = gifFile.Height,
                FrameRate = gifFile.FrameRate,
            };

            return dto;
        }

        internal IGifFile Create()
        {
            var gif = new GifFile()
            {
                Id = Id,
                Path = Path,
                Width = Width,
                Height = Height,
                FrameRate = FrameRate
            };

            return gif;
        }
    }
}
