using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Text.Json;

namespace SimpleGIFMaker.Domains
{
    public class Movie : IMovie
    {
        public int Id { get; set; }

        public string Path { get; set; }

        public int Width { get; set; }

        public int Height { get; set; }

        public int FrameCount { get; set; }

        public TimeSpan FrameLength { get; set; }

        public double FrameRate { get; set; }

        public int Rotation { get; set; }

        public Movie(string path)
        {
            this.Path = path;
            this.CacheMovieInfo(path);
        }

        public IGifFile CreateGifFile(IConvertCondition condition, IProgress<double> progress)
        {
            progress.Report(0);

            // TODO: progress
            // mp4->gif
            var toRotate = this.Rotation != 0;
            var gifFilePath = ConvertToGif(this.Path, condition, toRotate);

            progress.Report(1.0);

            var gif = new GifFile(gifFilePath);
            return gif;
        }

        private static string ConvertToGif(string filePath, IConvertCondition condition, bool toRotate)
        {
            var ffmpegPath = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "ffmpeg.exe");
            var orgFileName = System.IO.Path.GetFileName(filePath);
            var gifFileName = orgFileName.Replace(".mp4", ".gif");
            var gifFilePath = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, gifFileName);

            try
            {
                if (File.Exists(gifFilePath))
                {
                    File.Delete(gifFilePath);
                }

                var transposeStr = string.Empty;
                var x = toRotate ? condition.RoiY : condition.RoiX;
                var y = toRotate ? condition.RoiX : condition.RoiY;
                var width = toRotate ? condition.RoiHeight : condition.RoiWidth;
                var height = toRotate ? condition.RoiWidth : condition.RoiHeight;

                var psi = new ProcessStartInfo
                {
                    FileName = ffmpegPath,
                    Arguments = $"-i \"{filePath}\" -ss {condition.StartFrame.ToString()} -t {condition.TrimLength.TotalSeconds} -vf \"{transposeStr}crop={width}:{height}:{x}:{y},scale={width*condition.GifScale}:{height*condition.GifScale}\" -r {condition.GifFrameRate} -b:v 1M {gifFilePath} -progress -",
                    UseShellExecute = false,
                    RedirectStandardOutput = true,
                    CreateNoWindow = false//true
                };

                using (var process = Process.Start(psi))
                {
                    if (process is null)
                    {
                        throw new Exception("process is null");
                    }

                    var output = process.StandardOutput.ReadToEnd();
                    process.WaitForExit();

                    Console.WriteLine($"save {gifFilePath}");
                    return gifFilePath;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);

                return string.Empty;
            }
        }

        private void CacheMovieInfo(string filePath)
        {
            var ffprobePath = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "ffprobe.exe");

            try
            {
                var psi = new ProcessStartInfo
                {
                    FileName = ffprobePath,
                    Arguments = $"-v error -select_streams v:0 -show_streams -print_format json \"{filePath}\"",
                    UseShellExecute = false,
                    RedirectStandardOutput = true,
                    CreateNoWindow = true
                };

                using (var process = Process.Start(psi))
                {
                    if (process is null)
                    {
                        throw new Exception("process is null");
                    }

                    var output = process.StandardOutput.ReadToEnd();
                    process.WaitForExit();

                    // 出力から解像度、フレームレート、フレーム数を抽出
                    using (var doc = JsonDocument.Parse(output))
                    {
                        var root = doc.RootElement;
                        var streams = root.GetProperty("streams");
                        var stream = streams.EnumerateArray().First();

                        this.Width = stream.GetProperty("width").GetInt32();
                        this.Height = stream.GetProperty("height").GetInt32();
                        this.FrameLength = TimeSpan.FromSeconds(Convert.ToDouble(stream.GetProperty("duration").GetString()));
                        this.FrameRate = Convert.ToDouble(stream.GetProperty("r_frame_rate").GetString()?.Split('/')?.First());
                        this.FrameCount = Convert.ToInt32(stream.GetProperty("nb_frames").GetString());

                        var sideDataList = stream.GetProperty("side_data_list");
                        var sideData = sideDataList.EnumerateArray().First();
                        this.Rotation = sideData.GetProperty("rotation").GetInt32();
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
