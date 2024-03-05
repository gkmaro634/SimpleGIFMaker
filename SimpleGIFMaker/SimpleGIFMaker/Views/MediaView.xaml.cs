using CommunityToolkit.Mvvm.DependencyInjection;
using SimpleGIFMaker.ViewModels;
using System.Windows.Controls;
using System.Windows.Threading;

namespace SimpleGIFMaker.Views
{
    /// <summary>
    /// MediaView.xaml の相互作用ロジック
    /// </summary>
    public partial class MediaView : UserControl
    {
        private MediaViewModel vm;
        private DispatcherTimer timer;

        private double prevSeekbarValue = 0d;

        public MediaView()
        {
            InitializeComponent();

            this.vm = Ioc.Default.GetService<MediaViewModel>()!;
            this.DataContext = this.vm;

            // button
            this.startButton.Visibility = System.Windows.Visibility.Visible;
            this.pauseButton.Visibility = System.Windows.Visibility.Hidden;

            // crop
            this.cropRect.IsEditable = false;

            // cut
            this.timeline.IsEditable = false;

            // events
            this.media.MediaEnded += Media_MediaEnded;

            this.timer = new DispatcherTimer();
            this.timer.Interval = TimeSpan.FromMilliseconds(100);
            this.timer.Tick += Timer_Tick;
            this.timer.Start();

            this.vm.PropertyChanged += Vm_PropertyChanged;
        }

        private void Media_MediaEnded(object sender, System.Windows.RoutedEventArgs e)
        {
            if (this.vm.MediaState == Models.Definitions.MediaStateType.Playing)
            {
                this.vm.StopPlayingMovieCommand.Execute("");

                this.media.Position = TimeSpan.Zero;

                this.timeline.CurrentPosition = 0;
            }
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

                this.prevSeekbarValue = 0;
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
            else if (e.PropertyName == nameof(this.vm.EditMode))
            {
                this.cropRect.IsEditable = false;
                this.timeline.IsEditable = false;
                if (this.vm.EditMode == Models.Definitions.EditModeType.CropSetting)
                {
                    this.cropRect.IsEditable = true;
                }
                else if (this.vm.EditMode == Models.Definitions.EditModeType.CutSetting)
                {
                    this.timeline.IsEditable = true;
                }
            }
        }

        private void Timer_Tick(object? sender, EventArgs e)
        {
            var toSkip = false;
            toSkip |= this.vm.MediaState == Models.Definitions.MediaStateType.Empty;
            toSkip |= this.media.NaturalDuration.HasTimeSpan == false;

            if (toSkip)
            {
                return;
            }


            if (this.prevSeekbarValue == this.timeline.CurrentPosition)
            {
                // 時間経過
                if (this.vm.MediaState == Models.Definitions.MediaStateType.Playing)
                {
                    // 現在の再生位置取得
                    var currentTime = this.media.Position;
                    var duration = this.media.NaturalDuration.TimeSpan;
                    var progressRatio = currentTime.TotalMilliseconds / duration.TotalMilliseconds;

                    // シークバーのValueを更新
                    this.timeline.CurrentPosition = progressRatio;
                    this.prevSeekbarValue = progressRatio;
                }
            }
            else
            {
                // シークバーを操作
                var toRestart = false;
                if (this.vm.MediaState == Models.Definitions.MediaStateType.Playing)
                {
                    this.media.Pause();
                    toRestart = true;
                }

                var progressRatio = this.timeline.CurrentPosition;
                var duration = this.media.NaturalDuration.TimeSpan.TotalMilliseconds;
                var position = duration * progressRatio;
                this.media.Position = TimeSpan.FromMilliseconds(position);

                this.prevSeekbarValue = progressRatio;

                if (toRestart)
                {
                    this.media.Play();
                }
            }

            // 現在時刻表示を更新
            this.currentPositionText.Text = this.media.Position.ToString(@"hh\:mm\:ss");
        }
    }
}
