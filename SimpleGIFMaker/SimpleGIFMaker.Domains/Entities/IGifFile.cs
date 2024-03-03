using System.Diagnostics;

namespace SimpleGIFMaker.Domains
{
    public interface IGifFile
    {
        int Id { get; set; }

        string Path { get; set; }

        int Width { get; set; }

        int Height { get; set; }

        double FrameRate { get; set; }
    }
}
