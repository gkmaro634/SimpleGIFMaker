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
            Assert.Equal(300, gif.Width);
            Assert.Equal(400, gif.Height);
            Assert.Equal(10, gif.FrameRate);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // TODO: �}�l�[�W�h��Ԃ�j�����܂� (�}�l�[�W�h �I�u�W�F�N�g)
                    //if (File.Exists(this.expGifFilePath)) 
                    //{
                    //    File.Delete(this.expGifFilePath);
                    //}
                }

                // TODO: �A���}�l�[�W�h ���\�[�X (�A���}�l�[�W�h �I�u�W�F�N�g) ��������A�t�@�C�i���C�U�[���I�[�o�[���C�h���܂�
                // TODO: �傫�ȃt�B�[���h�� null �ɐݒ肵�܂�
                disposedValue = true;
            }
        }

        // // TODO: 'Dispose(bool disposing)' �ɃA���}�l�[�W�h ���\�[�X���������R�[�h���܂܂��ꍇ�ɂ̂݁A�t�@�C�i���C�U�[���I�[�o�[���C�h���܂�
        // ~MovieTests()
        // {
        //     // ���̃R�[�h��ύX���Ȃ��ł��������B�N���[���A�b�v �R�[�h�� 'Dispose(bool disposing)' ���\�b�h�ɋL�q���܂�
        //     Dispose(disposing: false);
        // }

        public void Dispose()
        {
            // ���̃R�[�h��ύX���Ȃ��ł��������B�N���[���A�b�v �R�[�h�� 'Dispose(bool disposing)' ���\�b�h�ɋL�q���܂�
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }
    }
}