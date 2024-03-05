//using Microsoft.Xaml.Behaviors;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using System.Windows;
//using System.Windows.Controls;

//namespace SimpleGIFMaker.Views.Behaviros
//{
//    public class SizeNotifyBehavior : Behavior<FrameworkElement>
//    {
//        protected override void OnAttached()
//        {
//            base.OnAttached();

//            AssociatedObject.SizeChanged += AssociatedObject_SizeChanged;
//        }

//        protected override void OnDetaching()
//        {
//            base.OnDetaching();

//            AssociatedObject.SizeChanged -= AssociatedObject_SizeChanged;
//        }

//        private void AssociatedObject_SizeChanged(object sender, SizeChangedEventArgs e)
//        {
//        }
//    }
//}
