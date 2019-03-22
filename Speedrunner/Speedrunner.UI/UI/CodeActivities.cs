using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace Speedrunner.UI
{
    public class InvokeMethod : Activity
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

    public class Expression : Activity
    {
        public static readonly DependencyProperty TextProperty =
            DependencyProperty.Register("Text", typeof(string), typeof(Expression), new PropertyMetadata(""));

        [Category(Constants.CategoryName)]
        public string Text
        {
            get { return (string)GetValue(TextProperty); }
            set { SetValue(TextProperty, value); }
        }

        static Expression()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(Expression), new FrameworkPropertyMetadata(typeof(Expression)));
        }
    }
}
