using NSubstitute;
using SimpleGIFMaker.Domains;
using SimpleGIFMaker.Domains.Repositories;
using SimpleGIFMaker.Models;
using SimpleGIFMaker.ViewModels;

namespace SimpleGIFMaker.Tests.UseCases
{
    public class uc103_EditCropSettingTests : IDisposable
    {
        private bool disposedValue;

        private MediaViewModel vm;
        private CropSettingViewModel subVm;

        private IMediaPlayer mediaPlayer;
        private IConvertConditionRepository convertConditionRepository;
        private IMovieRepository movieRepository;

        public uc103_EditCropSettingTests()
        {
            this.mediaPlayer = Substitute.For<IMediaPlayer>();
            this.convertConditionRepository = Substitute.For<IConvertConditionRepository>();
            this.movieRepository = Substitute.For<IMovieRepository>();

            this.vm = new MediaViewModel(this.mediaPlayer, this.movieRepository, this.convertConditionRepository);
            this.subVm = new CropSettingViewModel(this.mediaPlayer, this.convertConditionRepository);
        }

        [Fact]
        public void EditCropSetting()
        {
            //
            var cropRect = new CropRect(0, 0, 800, 600);
            this.mediaPlayer.GetCurrentCropRect().Returns(cropRect);
            this.mediaPlayer
                .When(x => x.UpdateCropRect(Arg.Any<CropRect>()))
                .Do(x => this.mediaPlayer.CropRectChanged?.Invoke(x.Arg<CropRect>()));

            //
            var modifiedCropRect = new CropRect(10, 20, 400, 300);
            this.vm.UpdateCropRect(modifiedCropRect);

            //
            this.mediaPlayer.Received().UpdateCropRect(modifiedCropRect);
            Assert.Equal(10, this.subVm.CropRectX);
            Assert.Equal(20, this.subVm.CropRectY);
            Assert.Equal(400, this.subVm.CropRectWidth);
            Assert.Equal(300, this.subVm.CropRectHeight);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                }

                disposedValue = true;
            }
        }

        public void Dispose()
        {
            // このコードを変更しないでください。クリーンアップ コードを 'Dispose(bool disposing)' メソッドに記述します
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }

    }
}