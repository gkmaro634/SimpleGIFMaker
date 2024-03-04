using SimpleGIFMaker.Domains;

namespace SimpleGIFMaker.Tests.Domains
{
    public class MovieTests : IDisposable
    {
        private string movieFilePath;
        private string expGifFilePath;
        private bool disposedValue;

        public MovieTests()
        {
            this.movieFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "testdata", "V_20240229_162423_ES0.mp4");
            this.expGifFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "V_20240229_162423_ES0.gif");
            if (File.Exists(this.expGifFilePath))
            {
                File.Delete(this.expGifFilePath);
            }
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
            Assert.Equal(23.5, movie.FrameLength.TotalSeconds, 0.1);
            Assert.Equal(705, movie.FrameCount);
            Assert.Equal(30, movie.FrameRate);
            Assert.Equal(-90, movie.Rotation);
        }

        [Fact]
        public void CreateGifFileTest()
        {
            //
            var movie = new Movie(this.movieFilePath);
            var condition = new ConvertCondition()
            {
                RoiX = 600,
                RoiY = 400,
                RoiWidth = 800,
                RoiHeight = 600,
                StartFrame = TimeSpan.FromSeconds(5),
                EndFrame = TimeSpan.FromSeconds(10),
                GifScale = 0.5,
                GifFrameRate = 10,
            };
            var progress = new Progress<double>();

            //
            var gif = movie.CreateGifFile(condition, progress);

            //
            Assert.True(gif is GifFile);
            Assert.Equal(this.expGifFilePath, gif.Path);
            Assert.Equal(400, gif.Width);
            Assert.Equal(300, gif.Height);
            Assert.Equal(10, gif.FrameRate);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // TODO: マネージド状態を破棄します (マネージド オブジェクト)
                    //if (File.Exists(this.expGifFilePath)) 
                    //{
                    //    File.Delete(this.expGifFilePath);
                    //}
                }

                // TODO: アンマネージド リソース (アンマネージド オブジェクト) を解放し、ファイナライザーをオーバーライドします
                // TODO: 大きなフィールドを null に設定します
                disposedValue = true;
            }
        }

        // // TODO: 'Dispose(bool disposing)' にアンマネージド リソースを解放するコードが含まれる場合にのみ、ファイナライザーをオーバーライドします
        // ~MovieTests()
        // {
        //     // このコードを変更しないでください。クリーンアップ コードを 'Dispose(bool disposing)' メソッドに記述します
        //     Dispose(disposing: false);
        // }

        public void Dispose()
        {
            // このコードを変更しないでください。クリーンアップ コードを 'Dispose(bool disposing)' メソッドに記述します
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }
    }
}