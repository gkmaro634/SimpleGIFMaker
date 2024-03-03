using NSubstitute;
using SimpleGIFMaker.Domains;
using SimpleGIFMaker.Domains.Repositories;
using SimpleGIFMaker.ViewModels;
using System;
using Xunit;

namespace SimpleGIFMaker.Tests.Domains
{
    public class uc001_ExecConvertTests : IDisposable
    {
        private bool disposedValue;

        private ConvertControlViewModel vm;
        private IMediaPlayer mediaPlayer;
        private IConvertConditionRepository convertConditionRepository;
        private IGifFileRepository gifFileRepository;
        private IMovieRepository movieRepository;

        public uc001_ExecConvertTests()
        {
            this.mediaPlayer = Substitute.For<IMediaPlayer>();
            this.convertConditionRepository = Substitute.For<IConvertConditionRepository>();
            this.gifFileRepository = Substitute.For<IGifFileRepository>();
            this.movieRepository = Substitute.For<IMovieRepository>();

            this.vm = new ConvertControlViewModel(this.mediaPlayer, this.movieRepository, this.convertConditionRepository, this.gifFileRepository);
        }

        [Fact]
        public async void ExecConvert()
        {
            //
            var movieMock = Substitute.For<IMovie>();
            this.movieRepository.GetMovieAsync(0).Returns(movieMock);

            var conditionMock = Substitute.For<IConvertCondition>();
            this.convertConditionRepository.GetConvertConditionAsync(0).Returns(conditionMock);

            var gifFileMock = Substitute.For<IGifFile>();
            movieMock.CreateGifFile(conditionMock, Arg.Any<IProgress<double>>()).Returns(gifFileMock);

            //
            await this.vm.ExecConvert();

            //
            await this.movieRepository.Received().GetMovieAsync(0);
            await this.convertConditionRepository.Received().GetConvertConditionAsync(0);
            await this.gifFileRepository.Received().AddGifFileAsync(gifFileMock);
        }

        [Fact]
        public void CanConvert()
        {
            //
            var movieMock = Substitute.For<IMovie>();
            this.mediaPlayer.GetCurrentMovie().Returns(movieMock);

            //

            //
            Assert.True(this.vm.ExecConvertCommand.CanExecute(""));
        }

        [Fact]
        public void CannotConvert()
        {
            //
            IMovie? movieMock = null;
            this.mediaPlayer.GetCurrentMovie().Returns(movieMock);

            //

            //
            Assert.False(this.vm.ExecConvertCommand.CanExecute(""));
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