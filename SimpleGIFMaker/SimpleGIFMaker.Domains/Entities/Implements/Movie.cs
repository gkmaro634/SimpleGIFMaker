using System.Diagnostics;
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
            var gifFilePath = ConvertToGif(this.Path, condition);

            progress.Report(1.0);

            var gif = new GifFile(gifFilePath);
            return gif;
        }

        private static string ConvertToGif(string filePath, IConvertCondition condition)
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

                var psi = new ProcessStartInfo
                {
                    FileName = ffmpegPath,
                    Arguments = $"-i \"{filePath}\" -ss {condition.StartFrame.ToString()} -t {condition.TrimLength.TotalSeconds} -vf \"crop={condition.RoiWidth}:{condition.RoiHeight}:{condition.RoiX}:{condition.RoiY},scale={condition.ScaledWidth}:{condition.ScaledHeight}\" -r {condition.GifFrameRate} -b:v 1M {gifFilePath} -progress -",
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
                // TODO: rotate info

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

                    //var lines = output.Split(new[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);
                    //if (lines.Length >= 4)
                    //{
                    //    var width = int.Parse(lines[0]);
                    //    var height = int.Parse(lines[1]);
                    //    var frameRateStr = lines[2];
                    //    var frameRate = Convert.ToDouble(frameRateStr.Split('/')[0]) / Convert.ToDouble(frameRateStr.Split('/')[1]);
                    //    var frameLength = Convert.ToDouble(lines[3]);
                    //    var frames = lines.Length > 4 ? int.Parse(lines[4]) : -1; // フレーム数がない場合は-1

                    //    this.Width = width;
                    //    this.Height = height;
                    //    this.FrameLength = TimeSpan.FromSeconds(frameLength);
                    //    this.FrameRate = frameRate;
                    //    this.FrameCount = frames;

                    //    Console.WriteLine($"解像度: {width}x{height}");
                    //    Console.WriteLine($"フレームレート: {frameRate:F2}");
                    //    Console.WriteLine($"フレーム長: {frameLength:F2}");
                    //    Console.WriteLine($"フレーム数: {frames}");
                    //}
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
