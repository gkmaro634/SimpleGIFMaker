using NSubstitute;
using SimpleGIFMaker.Domains;
using SimpleGIFMaker.Domains.Repositories;
using SimpleGIFMaker.Models;
using SimpleGIFMaker.ViewModels;

namespace SimpleGIFMaker.Tests.UseCases
{
    public class uc201_EntryCutSettingTests : IDisposable
    {
        private bool disposedValue;

        private MediaViewModel vm;
        private CutSettingViewModel subVm;

        private IMediaPlayer mediaPlayer;
        private IConvertConditionRepository convertConditionRepository;
        private IMovieRepository movieRepository;

        public uc201_EntryCutSettingTests()
        {
            this.mediaPlayer = Substitute.For<IMediaPlayer>();
            this.convertConditionRepository = Substitute.For<IConvertConditionRepository>();
            this.movieRepository = Substitute.For<IMovieRepository>();

            this.vm = new MediaViewModel(this.mediaPlayer, this.movieRepository, this.convertConditionRepository);
            this.subVm = new CutSettingViewModel(this.mediaPlayer, this.convertConditionRepository);
        }

        [Fact]
        public async void EntryCutSetting()
        {
            //
            var conditionMock = Substitute.For<IConvertCondition>();
            this.convertConditionRepository.GetConvertConditionAsync(0).Returns(conditionMock);

            //
            this.vm.SelectedTabIndex = 2;
            await this.vm.SelectTabCommand.ExecuteAsync("");
            this.subVm.LoadedCommand.Execute("");

            //
            Assert.Equal(Definitions.EditModeType.CutSetting, this.vm.EditMode);
            //Assert.Same(conditionMock, this.vm.ConvertCondition);
            Assert.Same(conditionMock, this.subVm.condition);
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
            // ���̃R�[�h��ύX���Ȃ��ł��������B�N���[���A�b�v �R�[�h�� 'Dispose(bool disposing)' ���\�b�h�ɋL�q���܂�
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }

    }
}