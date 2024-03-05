namespace SimpleGIFMaker.Domains
{
    public class CutRange
    {
        public TimeSpan Start { get; set; }

        public TimeSpan End { get; set; }

        public CutRange(TimeSpan start, TimeSpan end)
        {
            this.Start = start;
            this.End = end;
        }
    }
}
