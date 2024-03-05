using NSubstitute;
using SimpleGIFMaker.Domains;
using SimpleGIFMaker.Domains.Repositories;
using SimpleGIFMaker.ViewModels;
using static SimpleGIFMaker.Models.Definitions;

namespace SimpleGIFMaker.Tests.UseCases
{
    public class uc102_ExitCropSettingTests : IDisposable
    {
        private bool disposedValue;

        private MediaViewModel vm;
        private CropSettingViewModel subVm;

        private IMediaPlayer mediaPlayer;
        private IConvertConditionRepository convertConditionRepository;
        private IMovieRepository movieRepository;

        public uc102_ExitCropSettingTests()
        {
            this.mediaPlayer = Substitute.For<IMediaPlayer>();
            this.convertConditionRepository = Substitute.For<IConvertConditionRepository>();
            this.movieRepository = Substitute.For<IMovieRepository>();

            this.vm = new MediaViewModel(this.mediaPlayer, this.movieRepository, this.convertConditionRepository);
            this.subVm = new CropSettingViewModel(this.mediaPlayer, this.convertConditionRepository);
        }

        [Fact]
        public async void ExitCropSetting()
        {
            //
            var conditionMock = Substitute.For<IConvertCondition>();
            this.convertConditionRepository.GetConvertConditionAsync(0).Returns(conditionMock);

            this.vm.SelectedTabIndex = 1;
            await this.vm.SelectTabCommand.ExecuteAsync("");
            this.subVm.LoadedCommand.Execute("");

            //
            this.vm.SelectedTabIndex = 0;
            await this.vm.SelectTabCommand.ExecuteAsync("");
            this.subVm.UnloadedCommand.Execute("");

            //
            await this.convertConditionRepository.Received().UpdateConvertConditionAsync(0, this.subVm.condition!);
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