using NSubstitute;
using SimpleGIFMaker.Domains;
using SimpleGIFMaker.Domains.Repositories;
using SimpleGIFMaker.ViewModels;
using static SimpleGIFMaker.Models.Definitions;

namespace SimpleGIFMaker.Tests.UseCases
{
    public class uc301_StartPlayingMovieTests : IDisposable
    {
        private bool disposedValue;

        private MediaViewModel vm;

        private IMediaPlayer mediaPlayer;
        private IConvertConditionRepository convertConditionRepository;
        private IMovieRepository movieRepository;

        public uc301_StartPlayingMovieTests()
        {
            this.mediaPlayer = Substitute.For<IMediaPlayer>();
            this.convertConditionRepository = Substitute.For<IConvertConditionRepository>();
            this.movieRepository = Substitute.For<IMovieRepository>();

            this.vm = new MediaViewModel(this.mediaPlayer, this.movieRepository, this.convertConditionRepository);
        }

        [Fact]
        public void StartPlayingMovie()
        {
            //
            var movieMock = Substitute.For<IMovie>();
            this.mediaPlayer.GetCurrentMovie().Returns(movieMock);
            this.mediaPlayer.MovieChanged.Invoke(movieMock);

            //
            this.vm.StartPlayingMovie();

            //
            Assert.Equal(MediaStateType.Playing, this.vm.MediaState);
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