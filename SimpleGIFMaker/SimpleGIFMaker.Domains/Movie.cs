
namespace SimpleGIFMaker.Domains
{
    public class Movie
    {
        public GifFile CreateGifFile(ConvertCondition condition, IProgress<double> progress)
        {
            var gif = new GifFile();
            return gif;
        }

        public Thumbnail CreateThumbnailImage(int frameIndex)
        {
            var thumbnail = new Thumbnail();
            return thumbnail;
        }
    }
}
