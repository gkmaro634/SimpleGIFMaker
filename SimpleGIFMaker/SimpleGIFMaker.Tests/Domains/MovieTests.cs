using SimpleGIFMaker.Domains;
using System;
using Xunit;

namespace SimpleGIFMaker.Tests.Domains
{
    public class MovieTests
    {
        public MovieTests()
        {
                
        }

        [Fact]
        public void CreateGifFileTest()
        {
            //
            var movie = new Movie();
            var condition = new ConvertCondition();
            var progress = new Progress<double>();

            //
            var gif = movie.CreateGifFile(condition, progress);

            //
            Assert.True(gif is GifFile);
        }

        [Fact]
        public void CreateThumbnailImageTest()
        {
            //
            var movie = new Movie();

            //
            var ret = movie.CreateThumbnailImage(0);

            //
            Assert.True(ret is Thumbnail);
        }
    }
}