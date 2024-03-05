using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace SimpleGIFMaker.Views.Controls
{
    /// <summary>
    /// MediaTimeline.xaml の相互作用ロジック
    /// </summary>
    public partial class MediaTimeline : UserControl
    {
        #region props
        public static readonly DependencyProperty DragCompletedCommandProperty = DependencyProperty.Register("DragCompletedCommand", typeof(ICommand), typeof(MediaTimeline), new PropertyMetadata(null));

        public ICommand DragCompletedCommand
        {
            get { return (ICommand)GetValue(DragCompletedCommandProperty); }
            set { SetValue(DragCompletedCommandProperty, value); }
        }

        public static readonly DependencyProperty CurrentPositionProperty = DependencyProperty.Register("CurrentPosition", typeof(double), typeof(MediaTimeline), new PropertyMetadata(0.0));

        public double CurrentPosition
        {
            get { return (double)GetValue(CurrentPositionProperty); }
            set { SetValue(CurrentPositionProperty, value); }
        }

        public static readonly DependencyProperty MaskStartPositionProperty = DependencyProperty.Register("MaskStartPosition", typeof(double), typeof(MediaTimeline), new PropertyMetadata(0.0, OnMaskPositionChanged));

        public double MaskStartPosition
        {
            get { return (double)GetValue(MaskStartPositionProperty); }
            set { SetValue(MaskStartPositionProperty, value); }
        }

        public static readonly DependencyProperty MaskEndPositionProperty = DependencyProperty.Register("MaskEndPosition", typeof(double), typeof(MediaTimeline), new PropertyMetadata(1.0, OnMaskPositionChanged));

        public double MaskEndPosition
        {
            get { return (double)GetValue(MaskEndPositionProperty); }
            set { SetValue(MaskEndPositionProperty, value); }
        }

        public static readonly DependencyProperty IsEditableProperty = DependencyProperty.Register("IsEditable", typeof(bool), typeof(MediaTimeline), new PropertyMetadata(false, OnEditableChanged));

        public bool IsEditable
        {
            get { return (bool)GetValue(IsEditableProperty); }
            set { SetValue(IsEditableProperty, value); }
        }
        #endregion

        private bool isDragging = false;

        public MediaTimeline()
        {
            InitializeComponent();

            this.maskStartFlag.MouseLeftButtonDown += this.OnMouseLeftButtonDown;
            this.maskStartFlag.MouseLeftButtonUp += this.OnMouseLeftButtonUp;
            this.maskStartFlag.MouseMove += this.StartFlag_MouseMove;

            this.maskEndFlag.MouseLeftButtonDown += this.OnMouseLeftButtonDown;
            this.maskEndFlag.MouseLeftButtonUp += this.OnMouseLeftButtonUp;
            this.maskEndFlag.MouseMove += this.EndFlag_MouseMove;

            this.SizeChanged += MediaTimeline_SizeChanged;

            this.UpdateEditableState();
            this.UpdateMaskPosition();
        }

        private void MediaTimeline_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            this.UpdateMaskPosition();
        }

        private void StartFlag_MouseMove(object sender, MouseEventArgs e)
        {
            if (this.isDragging == false) { return; }

            var elm = this as FrameworkElement;
            var currentPosition = e.GetPosition(elm);
            var xRatio = currentPosition.X / elm!.ActualWidth;
            var symbol = e.OriginalSource as DraggableFlag;
            var isValid = true;
            isValid &= xRatio < this.MaskEndPosition;
            isValid &= 0 <= xRatio;
            if (symbol is not null && isValid)
            {
                this.MaskStartPosition = xRatio;
            }
        }

        private void EndFlag_MouseMove(object sender, MouseEventArgs e)
        {
            if (this.isDragging == false) { return; }

            var elm = this as FrameworkElement;
            var currentPosition = e.GetPosition(elm);
            var xRatio = currentPosition.X / elm!.ActualWidth;
            var symbol = e.OriginalSource as DraggableFlag;
            var isValid = true;
            isValid &= this.MaskStartPosition < xRatio;
            isValid &= xRatio <= 1;
            if (symbol is not null && isValid)
            {
                this.MaskEndPosition = xRatio;
            }
        }

        private void OnMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            if (this.isDragging)
            {
                this.DragCompletedCommand?.Execute(this);

                this.isDragging = false;
                var draggableControl = sender as DraggableFlag;
                draggableControl!.ReleaseMouseCapture();
            }
        }

        private void OnMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.isDragging = true;
            var draggableControl = sender as DraggableFlag;
            draggableControl!.CaptureMouse();
        }

        private static void OnMaskPositionChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var elm = d as MediaTimeline;
            if (elm is not null)
            {
                elm.UpdateMaskPosition();
            }
        }

        private void UpdateMaskPosition()
        {
            var startX = this.MaskStartPosition * this.ActualWidth;
            var endX = this.MaskEndPosition * this.ActualWidth;

            // start flag
            Canvas.SetLeft(this.maskStartFlag, startX);

            // end flag
            Canvas.SetLeft(this.maskEndFlag, endX);

            // start rect
            this.maskStart.Width = startX;

            // end rect
            Canvas.SetLeft(this.maskEnd, endX);
            this.maskEnd.Width = this.ActualWidth - endX;
        }

        private static void OnEditableChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var elm = d as MediaTimeline;
            if (elm is not null)
            {
                elm.UpdateEditableState();
            }
        }

        private void UpdateEditableState()
        {
            var vis = this.IsEditable ? Visibility.Visible : Visibility.Hidden;
            this.maskStartFlag.Visibility = vis;
            this.maskEndFlag.Visibility = vis;
        }
    }
}
