using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using SimpleGIFMaker.Domains;
using SimpleGIFMaker.Domains.Repositories;

namespace SimpleGIFMaker.ViewModels
{
    internal partial class ConvertResultWindowViewModel : ObservableObject
    {
        internal Action openExplorerAction = () => { };

        [ObservableProperty]
        internal IGifFile? gifFile;

        private readonly IGifFileRepository gifFileRepository;

        public IAsyncRelayCommand LoadedCommand { get; private set; }
        public IAsyncRelayCommand UnloadedCommand { get; private set; }

        public ConvertResultWindowViewModel(
            IGifFileRepository gifFileRepository)
        {
            this.gifFileRepository = gifFileRepository;
            this.openExplorerAction = this.OpenExplorerImpl;

            this.LoadedCommand = new AsyncRelayCommand(this.Loaded);
            this.UnloadedCommand = new AsyncRelayCommand(this.Unloaded);
        }

        //[RelayCommand]
        internal async Task Loaded()
        {
            this.GifFile = await this.gifFileRepository!.GetGifFileAsync(0);
        }

        //[RelayCommand]
        internal async Task Unloaded()
        {
            await Task.CompletedTask;
        }

        [RelayCommand]
        internal void OpenExplorer()
        {
            this.openExplorerAction?.Invoke();
        }

        internal void OpenExplorerImpl()
        {
            if (this.GifFile is null) { return; }

            var directoryPath = System.IO.Path.GetDirectoryName(this.GifFile.Path);
            if (System.IO.Directory.Exists(directoryPath))
            {
                using (var p = System.Diagnostics.Process.Start("explorer.exe", directoryPath!))
                {
                    ;
                }
            }
        }
    }
}
