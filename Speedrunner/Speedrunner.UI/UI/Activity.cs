using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace Speedrunner.UI
{
    static class Constants
    {
        public const string CategoryName = "Activity";
    }

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

    public class InvokeMethod : Control
    {
        public static readonly DependencyProperty TypeProperty =
            DependencyProperty.Register("Type", typeof(Type), typeof(InvokeMethod), new PropertyMetadata(null));

        public static readonly DependencyProperty MethodNameProperty =
            DependencyProperty.Register("MethodName", typeof(string), typeof(InvokeMethod), new PropertyMetadata(""));

        [Category(Constants.CategoryName)]
        public Type Type
        {
            get { return (Type)GetValue(TypeProperty); }
            set { SetValue(TypeProperty, value); }
        }

        [Category(Constants.CategoryName)]
        public string MethodName
        {
            get { return (string)GetValue(MethodNameProperty); }
            set { SetValue(MethodNameProperty, value); }
        }

        static InvokeMethod()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(InvokeMethod), new FrameworkPropertyMetadata(typeof(InvokeMethod)));
        }
    }
}
