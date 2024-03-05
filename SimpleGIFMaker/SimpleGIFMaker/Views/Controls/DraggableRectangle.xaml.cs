using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace SimpleGIFMaker.Views.Controls
{
    /// <summary>
    /// DraggableRectangle.xaml の相互作用ロジック
    /// </summary>
    public partial class DraggableRectangle : UserControl
    {
        public ICommand DragCompletedCommand
        {
            get { return (ICommand)GetValue(DragCompletedCommandProperty); }
            set { SetValue(DragCompletedCommandProperty, value); }
        }

        public static readonly DependencyProperty DragCompletedCommandProperty =
            DependencyProperty.Register("DragCompletedCommand", typeof(ICommand), typeof(DraggableRectangle), new PropertyMetadata(null));


        public static readonly DependencyProperty StartXProperty = DependencyProperty.Register("StartX", typeof(double), typeof(DraggableRectangle), new PropertyMetadata(0.0, OnPositionChanged));

        public double StartX
        {
            get { return (double)GetValue(StartXProperty); }
            set { SetValue(StartXProperty, value); }
        }

        public static readonly DependencyProperty EndXProperty = DependencyProperty.Register("EndX", typeof(double), typeof(DraggableRectangle), new PropertyMetadata(0.0, OnPositionChanged));

        public double EndX
        {
            get { return (double)GetValue(EndXProperty); }
            set { SetValue(EndXProperty, value); }
        }

        public static readonly DependencyProperty StartYProperty = DependencyProperty.Register("StartY", typeof(double), typeof(DraggableRectangle), new PropertyMetadata(1.0, OnPositionChanged));

        public double StartY
        {
            get { return (double)GetValue(StartYProperty); }
            set { SetValue(StartYProperty, value); }
        }

        public static readonly DependencyProperty EndYProperty = DependencyProperty.Register("EndY", typeof(double), typeof(DraggableRectangle), new PropertyMetadata(1.0, OnPositionChanged));

        public double EndY
        {
            get { return (double)GetValue(EndYProperty); }
            set { SetValue(EndYProperty, value); }
        }

        public static readonly DependencyProperty RectWidthProperty = DependencyProperty.Register("RectWidth", typeof(double), typeof(DraggableRectangle), new PropertyMetadata(1.0, OnPositionChanged));

        public double RectWidth
        {
            get { return (double)GetValue(RectWidthProperty); }
            protected set { SetValue(RectWidthProperty, value); }
        }

        public static readonly DependencyProperty RectHeightProperty = DependencyProperty.Register("RectHeight", typeof(double), typeof(DraggableRectangle), new PropertyMetadata(1.0, OnPositionChanged));

        public double RectHeight
        {
            get { return (double)GetValue(RectHeightProperty); }
            protected set { SetValue(RectHeightProperty, value); }
        }

        public static readonly DependencyProperty SymbolScaleProperty = DependencyProperty.Register("SymbolScale", typeof(double), typeof(DraggableRectangle), new PropertyMetadata(1.0));

        public double SymbolScale
        {
            get { return (double)GetValue(SymbolScaleProperty); }
            set { SetValue(SymbolScaleProperty, value); }
        }

        private readonly List<DraggableSymbol> cornerSymbols;
        private bool isDragging = false;

        public DraggableRectangle()
        {
            InitializeComponent();

            this.cornerSymbols = [this.c1, this.c2, this.c3, this.c4];
            foreach (var corner in this.cornerSymbols)
            {
                corner.MouseLeftButtonDown += OnMouseLeftButtonDown;
                corner.MouseLeftButtonUp += OnMouseLeftButtonUp;
            }
            this.c1.MouseMove += C1_MouseMove;
            this.c2.MouseMove += C2_MouseMove;
            this.c3.MouseMove += C3_MouseMove;
            this.c4.MouseMove += C4_MouseMove;
        }

        private void C1_MouseMove(object sender, MouseEventArgs e)
        {
            if (isDragging == false) { return; }

            var elm = this.Parent as FrameworkElement;
            var currentPosition = e.GetPosition(elm);
            var symbol = e.OriginalSource as DraggableSymbol;
            var isValid = true;
            isValid &= currentPosition.X < this.EndX;
            isValid &= currentPosition.Y < this.EndY;
            if (symbol is not null && isValid)
            {
                this.StartX = Math.Max(0, currentPosition.X);
                this.StartY = Math.Max(0, currentPosition.Y);
            }
        }

        private void C2_MouseMove(object sender, MouseEventArgs e)
        {
            if (isDragging == false) { return; }

            var elm = this.Parent as FrameworkElement;
            var currentPosition = e.GetPosition(elm);
            var symbol = e.OriginalSource as DraggableSymbol;
            var isValid = true;
            isValid &= currentPosition.X < this.EndX;
            isValid &= currentPosition.Y > this.StartY;
            if (symbol is not null && isValid)
            {
                this.StartX = Math.Max(0, currentPosition.X);
                this.EndY = Math.Min(elm!.ActualHeight, currentPosition.Y);
            }
        }

        private void C3_MouseMove(object sender, MouseEventArgs e)
        {
            if (isDragging == false) { return; }

            var elm = this.Parent as FrameworkElement;
            var currentPosition = e.GetPosition(elm);
            var symbol = e.OriginalSource as DraggableSymbol;
            var isValid = true;
            isValid &= currentPosition.X > this.StartX;
            isValid &= currentPosition.Y > this.StartY;
            if (symbol is not null && isValid)
            {
                this.EndX = Math.Min(elm!.ActualWidth, currentPosition.X);
                this.EndY = Math.Min(elm!.ActualHeight, currentPosition.Y);
            }
        }

        private void C4_MouseMove(object sender, MouseEventArgs e)
        {
            if (isDragging == false) { return; }

            var elm = this.Parent as FrameworkElement;
            var currentPosition = e.GetPosition(elm);
            var symbol = e.OriginalSource as DraggableSymbol;
            var isValid = true;
            isValid &= currentPosition.X > this.StartX;
            isValid &= currentPosition.Y < this.EndY;
            if (symbol is not null && isValid)
            {
                this.EndX = Math.Min(elm!.ActualWidth, currentPosition.X);
                this.StartY = Math.Max(0, currentPosition.Y);
            }
        }

        private void OnMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            if (isDragging)
            {
                this.DragCompletedCommand?.Execute(this);

                isDragging = false;
                var draggableControl = sender as DraggableSymbol;
                draggableControl!.ReleaseMouseCapture();
            }
        }

        private void OnMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            isDragging = true;
            var draggableControl = sender as DraggableSymbol;
            draggableControl!.CaptureMouse();
        }

        private static void OnPositionChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var control = d as DraggableRectangle;
            if (control != null)
            {
                control.UpdateSize();
            }
        }

        private void UpdateSize()
        {
            this.RectWidth = this.EndX - this.StartX;
            this.RectHeight = this.EndY - this.StartY;
        }
    }
}
