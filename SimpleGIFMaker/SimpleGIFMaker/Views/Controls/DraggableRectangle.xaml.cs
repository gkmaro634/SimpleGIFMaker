using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Shapes;

namespace SimpleGIFMaker.Views.Controls
{
    /// <summary>
    /// DraggableRectangle.xaml の相互作用ロジック
    /// </summary>
    public partial class DraggableRectangle : UserControl
    {
        // X Coordinate
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

        // Y Coordinate
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

        // Width
        public static readonly DependencyProperty RectWidthProperty = DependencyProperty.Register("RectWidth", typeof(double), typeof(DraggableRectangle), new PropertyMetadata(1.0, OnPositionChanged));

        public double RectWidth
        {
            get { return (double)GetValue(RectWidthProperty); }
            protected set { SetValue(RectWidthProperty, value); }
        }

        //internal static readonly DependencyPropertyKey RectWidthPropertyKey = DependencyProperty.RegisterReadOnly("RectWidth", typeof(double), typeof(DraggableRectangle), new FrameworkPropertyMetadata(1));

        //public double RectWidth
        //{
        //    get { return (double)GetValue(RectWidthPropertyKey.DependencyProperty); }
        //    protected set { SetValue(RectWidthPropertyKey, value); }
        //}

        // Height
        public static readonly DependencyProperty RectHeightProperty = DependencyProperty.Register("RectHeight", typeof(double), typeof(DraggableRectangle), new PropertyMetadata(1.0, OnPositionChanged));

        public double RectHeight
        {
            get { return (double)GetValue(RectHeightProperty); }
            protected set { SetValue(RectHeightProperty, value); }
        }

        //internal static readonly DependencyPropertyKey RectHeightPropertyKey = DependencyProperty.RegisterReadOnly("RectHeight", typeof(double), typeof(DraggableRectangle), new FrameworkPropertyMetadata(1));

        //public double RectHeight
        //{
        //    get { return (double)GetValue(RectHeightPropertyKey.DependencyProperty); }
        //    protected set { SetValue(RectHeightPropertyKey, value); }
        //}

        private List<Ellipse> cornerPoints;

        public DraggableRectangle()
        {
            InitializeComponent();

            this.cornerPoints = [this.c1, this.c2, this.c3, this.c4];
            foreach (var corner in this.cornerPoints)
            {
                corner.MouseLeftButtonDown += OnMouseLeftButtonDown;
                corner.MouseLeftButtonUp += OnMouseLeftButtonUp;
                corner.MouseMove += OnMouseMove;
            }

            //rect.MouseLeftButtonDown += Rect_MouseLeftButtonDown;
            //rect.MouseLeftButtonUp += Rect_MouseLeftButtonUp;
            //rect.MouseMove += Rect_MouseMove;
        }

        private void OnMouseMove(object sender, MouseEventArgs e)
        {
            if (isDragging == false) { return; }

            var currentPosition = e.GetPosition(this.Parent as UIElement);
            Console.WriteLine(e.OriginalSource.ToString());

            //if (isDragging && sender is Shape draggableControl)
            //{
            //    var currentPosition = e.GetPosition(this.Parent as UIElement);
            //    //var transform = draggableControl.RenderTransform as TranslateTransform;
            //    //if (transform == null)
            //    //{
            //    //    transform = new TranslateTransform();
            //    //    draggableControl.RenderTransform = transform;
            //    //}
            //    //transform.X = currentPosition.X - clickPosition.X;
            //    //transform.Y = currentPosition.Y - clickPosition.Y;
            //}
        }

        private void OnMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            isDragging = false;
            var draggableControl = sender as Shape;
            draggableControl!.ReleaseMouseCapture();
        }

        private void OnMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            isDragging = true;
            var draggableControl = sender as Shape;
            clickPosition = e.GetPosition(this);
            draggableControl!.CaptureMouse();
        }

        // Position changed callback
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

        private bool isDragging = false;
        private Point clickPosition;


        //private void UpdatePosition()
        //{
        //    Canvas.SetLeft(rect, this.StartX);
        //    Canvas.SetTop(rect, this.StartY);
        //    Canvas.SetRight(rect, this.EndX);
        //    Canvas.SetBottom(rect, this.EndY);
        //}


        private void Rect_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            isDragging = true;
            var draggableControl = sender as Shape;
            clickPosition = e.GetPosition(this);
            draggableControl!.CaptureMouse();
        }

        private void Rect_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            isDragging = false;
            var draggableControl = sender as Shape;
            draggableControl!.ReleaseMouseCapture();
        }

        private void Rect_MouseMove(object sender, MouseEventArgs e)
        {
            if (isDragging && sender is Shape draggableControl)
            {
                var currentPosition = e.GetPosition(this.Parent as UIElement);
                //var transform = draggableControl.RenderTransform as TranslateTransform;
                //if (transform == null)
                //{
                //    transform = new TranslateTransform();
                //    draggableControl.RenderTransform = transform;
                //}
                //transform.X = currentPosition.X - clickPosition.X;
                //transform.Y = currentPosition.Y - clickPosition.Y;
            }
        }
    }
}
