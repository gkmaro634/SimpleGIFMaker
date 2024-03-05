namespace SimpleGIFMaker.Models
{
    internal class SelectableItem
    {
        public string Label { get; set; } = string.Empty;

        public double Value { get; set; } = 0d;

        public SelectableItem(string name, double value)
        {
            this.Label = name;
            this.Value = value;
        }
    }
}
