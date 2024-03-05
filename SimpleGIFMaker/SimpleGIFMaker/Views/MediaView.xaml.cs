using CommunityToolkit.Mvvm.DependencyInjection;
using SimpleGIFMaker.ViewModels;
using System.Windows.Controls;

namespace SimpleGIFMaker.Views
{
    /// <summary>
    /// MediaView.xaml の相互作用ロジック
    /// </summary>
    public partial class MediaView : UserControl
    {
        private MediaViewModel vm;

        public MediaView()
        {
            InitializeComponent();

            this.vm = Ioc.Default.GetService<MediaViewModel>()!;
            this.DataContext = this.vm;

            this.startButton.Visibility = System.Windows.Visibility.Visible;
            this.pauseButton.Visibility = System.Windows.Visibility.Hidden;

            this.vm.PropertyChanged += Vm_PropertyChanged;
        }

        private void Vm_PropertyChanged(object? sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(this.vm.CurrentMovie))
            {
                if (this.vm.CurrentMovie is null)
                {
                    return;
                }

                // ファイル読み込み直後にサムネイルを表示する
                this.media.ScrubbingEnabled = true;
                this.media.Play();
                this.media.Stop();
                this.media.Position = TimeSpan.Zero;
            }
            else if(e.PropertyName == nameof(this.vm.MediaState))
            {
                if (this.vm.MediaState == Models.Definitions.MediaStateType.Playing)
                {
                    this.media.Play();

                    this.startButton.Visibility = System.Windows.Visibility.Hidden;
                    this.pauseButton.Visibility = System.Windows.Visibility.Visible;
                }
                else if (this.vm.MediaState == Models.Definitions.MediaStateType.Pause)
                {
                    this.media.Pause();

                    this.startButton.Visibility = System.Windows.Visibility.Visible;
                    this.pauseButton.Visibility = System.Windows.Visibility.Hidden;
                }
            }
        }
    }
}
