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

            await this.vm.EntryCut();
            this.subVm.LoadedCommand.Execute("");

            //
            await this.vm.EntryConvertControl();
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
            // ���̃R�[�h��ύX���Ȃ��ł��������B�N���[���A�b�v �R�[�h�� 'Dispose(bool disposing)' ���\�b�h�ɋL�q���܂�
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }

    }
}