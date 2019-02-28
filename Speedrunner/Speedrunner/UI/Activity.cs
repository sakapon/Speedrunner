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

        [Category("Activity")]
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
}
