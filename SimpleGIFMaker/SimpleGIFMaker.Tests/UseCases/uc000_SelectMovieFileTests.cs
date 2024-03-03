using NSubstitute;
using SimpleGIFMaker.Domains;
using SimpleGIFMaker.Domains.Repositories;
using SimpleGIFMaker.ViewModels;
using System;
using Xunit;

namespace SimpleGIFMaker.Tests.Domains
{
    public class uc000_SelectMovieFileTests : IDisposable
    {
        private bool disposedValue;
        private ConvertControlViewModel vm;
        private IMediaPlayer mediaPlayer;
        private IConvertConditionRepository convertConditionRepository;
        private IGifFileRepository gifFileRepository;
        private IMovieRepository movieRepository;

        public uc000_SelectMovieFileTests()
        {
            this.mediaPlayer = Substitute.For<IMediaPlayer>();
            this.convertConditionRepository = Substitute.For<IConvertConditionRepository>();
            this.gifFileRepository = Substitute.For<IGifFileRepository>();
            this.movieRepository = Substitute.For<IMovieRepository>();

            this.vm = new ConvertControlViewModel(this.mediaPlayer, this.movieRepository, this.convertConditionRepository, this.gifFileRepository);
        }

        [Fact]
        public async void FileSelect()
        {
            //
            var movieMock = Substitute.For<IMovie>();
            this.vm.selectMovieFileFunc = () => movieMock;

            //
            await this.vm.SelectFile();

            //
            await this.movieRepository.Received().AddMovieAsync(movieMock);
            this.mediaPlayer.Received().SetMovie(movieMock);
        }

        [Fact]
        public async void FileSelectCancel()
        {
            //
            this.vm.selectMovieFileFunc = () => null;

            //
            await this.vm.SelectFile();

            //
            await this.movieRepository.DidNotReceive().AddMovieAsync(Arg.Any<IMovie>());
            this.mediaPlayer.DidNotReceive().SetMovie(Arg.Any<IMovie>());
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