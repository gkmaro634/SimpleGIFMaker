using System.Diagnostics;

namespace SimpleGIFMaker.Domains
{
    public class GifFile
    {
        public string Path { get; set; }

        public int Width { get; set; }

        public int Height { get; set; }

        public double FrameRate { get; set; }

        public GifFile(string path)
        {
            this.Path = path;

            this.GetGifFileInfo(path);
        }

        private void GetGifFileInfo(string filePath)
        {
            var ffprobePath = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "ffprobe.exe");

            try
            {
                var psi = new ProcessStartInfo
                {
                    FileName = ffprobePath,
                    Arguments = $"-v error -select_streams v:0 -show_entries stream=width,height,r_frame_rate,nb_frames -of default=noprint_wrappers=1:nokey=1 \"{filePath}\"",
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
                    var lines = output.Split(new[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);
                    if (lines.Length >= 3)
                    {
                        var width = int.Parse(lines[0]);
                        var height = int.Parse(lines[1]);
                        var frameRateStr = lines[2];
                        var frameRate = Convert.ToDouble(frameRateStr.Split('/')[0]) / Convert.ToDouble(frameRateStr.Split('/')[1]);
                        var frames = lines.Length > 3 ? int.Parse(lines[3]) : -1; // フレーム数がない場合は-1

                        this.Width = width;
                        this.Height = height;
                        this.FrameRate = frameRate;
                        //this.FrameCount = frames;

                        Console.WriteLine($"解像度: {width}x{height}");
                        Console.WriteLine($"フレームレート: {frameRate:F2}");
                        //Console.WriteLine($"フレーム数: {frames}");
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
