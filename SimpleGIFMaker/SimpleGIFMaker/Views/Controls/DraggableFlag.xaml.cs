using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace SimpleGIFMaker.Views.Controls
{
    /// <summary>
    /// DraggableFlag.xaml の相互作用ロジック
    /// </summary>
    public partial class DraggableFlag : UserControl
    {
        public static readonly DependencyProperty ReverseProperty = DependencyProperty.Register("Reverse", typeof(bool), typeof(DraggableFlag), new PropertyMetadata(false, OnReverseChanged));

        public bool Reverse
        {
            get { return (bool)GetValue(ReverseProperty); }
            set { SetValue(ReverseProperty, value); }
        }

        public DraggableFlag()
        {
            InitializeComponent();

            this.MouseEnter += DraggableSymbol_MouseEnter;
            this.MouseLeave += DraggableSymbol_MouseLeave;
        }

        private void DraggableSymbol_MouseEnter(object sender, MouseEventArgs e)
        {
            Cursor = Cursors.Hand;
        }

        private void DraggableSymbol_MouseLeave(object sender, MouseEventArgs e)
        {
            Cursor = Cursors.Arrow;
        }

        private static void OnReverseChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var elm = d as DraggableFlag;
            if (elm != null)
            {
                elm.UpdateDirection();
            }
        }

        private void UpdateDirection()
        {
            var scale = this.Reverse ? -1d : 1d;
            var scaleTransform = new ScaleTransform(scale, 1, this.ActualWidth / 2d, this.ActualHeight / 2d);
            this.thisControl.RenderTransform = scaleTransform;
        }

    }
}
