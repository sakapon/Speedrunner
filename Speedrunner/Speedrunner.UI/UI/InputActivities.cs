using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace Speedrunner.UI
{
    public class Delay : Control
    {
        public static readonly DependencyProperty TimeoutProperty =
            DependencyProperty.Register("Timeout", typeof(int), typeof(Delay), new PropertyMetadata(0));

        [Category(Constants.CategoryName)]
        public int Timeout
        {
            get { return (int)GetValue(TimeoutProperty); }
            set { SetValue(TimeoutProperty, value); }
        }

        static Delay()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(Delay), new FrameworkPropertyMetadata(typeof(Delay)));
        }
    }

    public class Click : Delay
    {
        public static readonly DependencyProperty PositionProperty =
            DependencyProperty.Register("Position", typeof(Point), typeof(Click), new PropertyMetadata(new Point()));

        public static readonly DependencyProperty UsesCurrentPointProperty =
            DependencyProperty.Register("UsesCurrentPoint", typeof(bool), typeof(Click), new PropertyMetadata(false));

        [Category(Constants.CategoryName)]
        public Point Position
        {
            get { return (Point)GetValue(PositionProperty); }
            set { SetValue(PositionProperty, value); }
        }

        [Category(Constants.CategoryName)]
        public bool UsesCurrentPoint
        {
            get { return (bool)GetValue(UsesCurrentPointProperty); }
            set { SetValue(UsesCurrentPointProperty, value); }
        }

        static Click()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(Click), new FrameworkPropertyMetadata(typeof(Click)));
        }
    }
}
