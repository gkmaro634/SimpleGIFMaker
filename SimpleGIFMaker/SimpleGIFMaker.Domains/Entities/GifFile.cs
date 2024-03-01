using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleGIFMaker.Domains
{
    public class GifFile
    {
        public string Path { get; set; }

        public int Width { get; set; }

        public int Height { get; set; }

        public int FrameCount { get; set; }

        public int FrameRate { get; set; }

        public GifFile(string path)
        {
            this.Path = path;
        }
    }
}
