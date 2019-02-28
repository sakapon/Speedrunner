using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace Speedrunner.UI
{
    public class Return : Control
    {
        static Return()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(Return), new FrameworkPropertyMetadata(typeof(Return)));
        }
    }

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

    public class Code : Control
    {
        public static readonly DependencyProperty TypeProperty =
            DependencyProperty.Register("Type", typeof(Type), typeof(Code), new PropertyMetadata(null));

        public static readonly DependencyProperty MethodNameProperty =
            DependencyProperty.Register("MethodName", typeof(string), typeof(Code), new PropertyMetadata(""));

        [Category("Activity")]
        public Type Type
        {
            get { return (Type)GetValue(TypeProperty); }
            set { SetValue(TypeProperty, value); }
        }

        [Category("Activity")]
        public string MethodName
        {
            get { return (string)GetValue(MethodNameProperty); }
            set { SetValue(MethodNameProperty, value); }
        }

        static Code()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(Code), new FrameworkPropertyMetadata(typeof(Code)));
        }
    }
}
