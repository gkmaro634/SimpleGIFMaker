﻿using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.WindowsAPICodePack.Dialogs;
using SimpleGIFMaker.Domains;
using SimpleGIFMaker.Domains.Repositories;
using SimpleGIFMaker.Models;
using static SimpleGIFMaker.Models.Definitions;

namespace SimpleGIFMaker.ViewModels
{
    internal partial class ConvertControlViewModel : ObservableObject
    {
        private readonly IMediaPlayer mediaPlayer;
        private readonly IMovieRepository movieRepository;
        private readonly IConvertConditionRepository convertConditionRepository;
        private readonly IGifFileRepository gifFileRepository;

        [ObservableProperty]
        private List<ScaleSelectItem> scaleSelectItems = new();

        [ObservableProperty]
        private IConvertCondition? condition;

        [ObservableProperty]
        [NotifyCanExecuteChangedFor(nameof(ExecConvertCommand))]
        private IMovie? movie;

        [ObservableProperty]
        private MediaStateType mediaState = MediaStateType.Empty;

        internal Func<IMovie?> selectMovieFileFunc;

        public ConvertControlViewModel(
            IMediaPlayer mediaPlayer,
            IMovieRepository movieRepository,
            IConvertConditionRepository convertConditionRepository,
            IGifFileRepository gifFileRepository)
        {
            this.mediaPlayer = mediaPlayer;
            this.movieRepository = movieRepository;
            this.convertConditionRepository = convertConditionRepository;
            this.gifFileRepository = gifFileRepository;

            this.selectMovieFileFunc = this.SelectMovieFile;

            var items = Enumerable.Range(1, 10).Select(v =>
            {
                var label = v == 1 ? "1" : $"1/{v}";
                var item = new ScaleSelectItem(label, 1d / (double)v);
                return item;
            });
            this.ScaleSelectItems.AddRange(items);
        }

        [RelayCommand]
        internal async Task Loaded()
        {
            var condition = await this.convertConditionRepository.GetConvertConditionAsync(0);
            this.Condition = condition;
        }

        [RelayCommand]
        internal async Task Unloaded()
        {
            if (this.Condition is not null)
            {
                await this.convertConditionRepository.UpdateConvertConditionAsync(0, this.Condition);
            }
        }

        internal IMovie? SelectMovieFile()
        {
            using (var dialog = new CommonOpenFileDialog())
            {
                var result = dialog.ShowDialog();
                if (result != CommonFileDialogResult.Ok)
                {
                    return null;
                }

                var filePath = dialog.FileName;
                var movie = new Movie(filePath);
                return movie;
            }
        }

        [RelayCommand]
        internal async Task SelectFile()
        {
            var movie = this.selectMovieFileFunc.Invoke();
            if (movie is null)
            {
                return;
            }

            var condition = new ConvertCondition(movie);
            this.Condition = condition;
            this.Movie = movie;

            await this.convertConditionRepository.AddConvertConditionAsync(condition);
            await this.movieRepository.AddMovieAsync(movie);

            this.mediaPlayer.SetMovie(movie);

            this.MediaState = MediaStateType.SourceLoaded;
        }

        [RelayCommand(CanExecute = nameof(CanConvert))]
        internal async Task ExecConvert()
        {
            var movie = await this.movieRepository.GetMovieAsync(0);
            if (movie is null)
            {
                return;
            }

            var condition = await this.convertConditionRepository.GetConvertConditionAsync(0);
            if (condition is null)
            {
                return;
            }

            var progress = new Progress<double>();
            var gif = movie.CreateGifFile(condition, progress);

            await this.gifFileRepository.AddGifFileAsync(gif);
        }

        private bool CanConvert()
        {
            return this.Movie is object;
        }

        [RelayCommand]
        internal async Task UpdateGifScale(double scale)
        {
            if (this.Condition is not null)
            {
                this.Condition.GifScale = scale;
                await this.convertConditionRepository.UpdateConvertConditionAsync(0, this.Condition);
            }
        }

        [RelayCommand]
        internal async Task UpdateGifFrameRate(int frameRate)
        {
            if (this.Condition is not null)
            {
                this.Condition.GifFrameRate = frameRate;
                await this.convertConditionRepository.UpdateConvertConditionAsync(0, this.Condition);
            }
        }

    }
}
