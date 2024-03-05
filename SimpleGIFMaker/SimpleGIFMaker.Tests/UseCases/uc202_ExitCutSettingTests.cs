using NSubstitute;
using SimpleGIFMaker.Domains;
using SimpleGIFMaker.Domains.Repositories;
using SimpleGIFMaker.Models;
using SimpleGIFMaker.ViewModels;
using static SimpleGIFMaker.Models.Definitions;

namespace SimpleGIFMaker.Tests.UseCases
{
    public class uc202_ExitCutSettingTests : IDisposable
    {
        private bool disposedValue;

        private MediaViewModel vm;
        private CutSettingViewModel subVm;

        private IMediaPlayer mediaPlayer;
        private IConvertConditionRepository convertConditionRepository;
        private IMovieRepository movieRepository;

        public uc202_ExitCutSettingTests()
        {
            this.mediaPlayer = Substitute.For<IMediaPlayer>();
            this.convertConditionRepository = Substitute.For<IConvertConditionRepository>();
            this.movieRepository = Substitute.For<IMovieRepository>();

            this.vm = new MediaViewModel(this.mediaPlayer, this.movieRepository, this.convertConditionRepository);
            this.subVm = new CutSettingViewModel(this.convertConditionRepository);
        }

        [Fact]
        public async void ExitCutSetting()
        {
            //
            var conditionMock = Substitute.For<IConvertCondition>();
            this.convertConditionRepository.GetConvertConditionAsync(0).Returns(conditionMock);

            this.vm.SelectedTabIndex = 2;
            await this.vm.SelectTabCommand.ExecuteAsync("");
            this.subVm.LoadedCommand.Execute("");

            //
            this.vm.SelectedTabIndex = 0;
            await this.vm.SelectTabCommand.ExecuteAsync("");
            this.subVm.UnloadedCommand.Execute("");

            //
            await this.convertConditionRepository.Received().UpdateConvertConditionAsync(0, this.subVm.Condition!);
            Assert.Equal(EditModeType.ConvertSetting, this.vm.EditMode);
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