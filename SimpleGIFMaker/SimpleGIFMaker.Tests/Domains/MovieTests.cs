using SimpleGIFMaker.Domains;
using SimpleGIFMaker.Domains.Repositories;
using System;
using System.IO;
using Xunit;

namespace SimpleGIFMaker.Tests.Domains
{
    public class MovieTests
    {
        private string movieFilePath;

        public MovieTests()
        {
             this.movieFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "testdata", "V_20240229_162423_ES0.mp4");
        }

        [Fact]
        public void CreateGifFileTest()
        {
            //
            var movie = new Movie(this.movieFilePath);
            var condition = new ConvertCondition();
            var progress = new Progress<double>();

            //
            var gif = movie.CreateGifFile(condition, progress);

            //
            Assert.True(gif is GifFile);
        }
    }
}