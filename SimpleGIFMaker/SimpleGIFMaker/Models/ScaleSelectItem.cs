namespace SimpleGIFMaker.Models
{
    internal class ScaleSelectItem
    {
        public string Label { get; set; } = string.Empty;

        public double Value { get; set; } = 0d;

        public ScaleSelectItem(string name, double value)
        {
            this.Label = name;
            this.Value = value;
        }
    }
}
