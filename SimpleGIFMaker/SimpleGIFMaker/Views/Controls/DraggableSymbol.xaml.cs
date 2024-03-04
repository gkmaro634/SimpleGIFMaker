using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace SimpleGIFMaker.Views.Controls
{
    /// <summary>
    /// DraggableSymbol.xaml の相互作用ロジック
    /// </summary>
    public partial class DraggableSymbol : UserControl
    {
        public static readonly DependencyProperty RotationProperty = DependencyProperty.Register("Rotation", typeof(double), typeof(DraggableSymbol), new PropertyMetadata(0.0));

        public double Rotation
        {
            get { return (double)GetValue(RotationProperty); }
            set { SetValue(RotationProperty, value); }
        }

        public static readonly DependencyProperty ScaleProperty = DependencyProperty.Register("Scale", typeof(double), typeof(DraggableSymbol), new PropertyMetadata(1.0));

        public double Scale
        {
            get { return (double)GetValue(ScaleProperty); }
            set { SetValue(ScaleProperty, value); }
        }

        public DraggableSymbol()
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
    }
}
