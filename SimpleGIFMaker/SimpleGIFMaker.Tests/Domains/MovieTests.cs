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
        public void MovieTest()
        {
            //
            var movie = new Movie(this.movieFilePath);

            //
            Assert.Equal(this.movieFilePath, movie.Path);
            Assert.Equal(1920, movie.Width);
            Assert.Equal(1080, movie.Height);
            //Assert.Equal(this.movieFilePath, movie.FrameCount);
            Assert.Equal(30.01, movie.FrameRate);

            //
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