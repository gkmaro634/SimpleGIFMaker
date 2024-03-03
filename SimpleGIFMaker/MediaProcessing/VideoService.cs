using OpenCvSharp;
using System.Timers;

namespace MediaProcessing
{
    public class VideoService
    {
        public Action<Stream> OnFrameChanged = (s) => { };

        private VideoCapture capture;
        private System.Timers.Timer timer;

        public VideoService(string videoPath)
        {
            this.capture = new VideoCapture(videoPath);
            if (!this.capture.IsOpened())
            {
                throw new Exception("Video not found.");
            }

            // Set the timer interval based on the video FPS
            this.timer = new System.Timers.Timer(1000 / this.capture.Fps); 
            this.timer.Elapsed += this.Timer_Elapsed;
        }

        private void Timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            using (var mat = new Mat())
            {
                if (this.capture.Read(mat))
                {
                    var imageStream = MatToStream(mat);
                    this.OnFrameChanged?.Invoke(imageStream);
                }
                else
                {
                    // Stop the timer if the video ends
                    this.timer.Stop(); 
                }
            }
        }

        public void Start() => this.timer.Start();
        public void Stop() => this.timer.Stop();

        private static Stream MatToStream(Mat mat)
        {
            var imageBytes = mat.ToBytes(".png");
            return new MemoryStream(imageBytes);
        }
    }
}
