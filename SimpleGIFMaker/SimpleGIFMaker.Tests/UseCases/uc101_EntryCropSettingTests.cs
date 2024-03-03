using NSubstitute;
using SimpleGIFMaker.Domains;
using SimpleGIFMaker.Domains.Repositories;
using SimpleGIFMaker.ViewModels;

namespace SimpleGIFMaker.Tests.Domains
{
    public class uc101_EntryCropSettingTests : IDisposable
    {
        private bool disposedValue;

        private MediaViewModel vm;
        private CropSettingViewModel subVm;

        private IMediaPlayer mediaPlayer;
        private IConvertConditionRepository convertConditionRepository;
        private IMovieRepository movieRepository;

        public uc101_EntryCropSettingTests()
        {
            this.mediaPlayer = Substitute.For<IMediaPlayer>();
            this.convertConditionRepository = Substitute.For<IConvertConditionRepository>();
            this.movieRepository = Substitute.For<IMovieRepository>();

            this.vm = new MediaViewModel(this.mediaPlayer, this.movieRepository, this.convertConditionRepository);
            this.subVm = new CropSettingViewModel(this.convertConditionRepository);
        }

        [Fact]
        public void EntryCropSetting()
        {
            //
            var conditionMock = Substitute.For<IConvertCondition>();
            this.convertConditionRepository.GetConvertConditionAsync(0).Returns(conditionMock);

            //
            this.vm.EntryCrop();
            this.subVm.LoadedCommand.Execute("");

            //
            Assert.Equal(MediaViewModel.StateType.CropSettingEdit, this.vm.State);
            Assert.Same(conditionMock, this.vm.ConvertCondition);
            Assert.Same(conditionMock, this.subVm.Condition);
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