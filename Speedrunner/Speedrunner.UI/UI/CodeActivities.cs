using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace Speedrunner.UI
{
    public class Expression : Control
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
