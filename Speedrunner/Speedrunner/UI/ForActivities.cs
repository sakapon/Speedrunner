using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace Speedrunner.UI
{
    public class ForRange : ItemsControl
    {
        public static readonly DependencyProperty ItemsMarginProperty =
            DependencyProperty.Register("ItemsMargin", typeof(Thickness), typeof(ForRange), new PropertyMetadata(new Thickness(20, 0, 0, 0)));

        [Category("Layout")]
        public Thickness ItemsMargin
        {
            get { return (Thickness)GetValue(ItemsMarginProperty); }
            set { SetValue(ItemsMarginProperty, value); }
        }

        public static readonly DependencyProperty VariableNameProperty =
            DependencyProperty.Register("VariableName", typeof(string), typeof(ForRange), new PropertyMetadata("i"));

        public static readonly DependencyProperty StartProperty =
            DependencyProperty.Register("Start", typeof(int), typeof(ForRange), new PropertyMetadata(0));

        [Category("Activity")]
        public string VariableName
        {
            get { return (string)GetValue(VariableNameProperty); }
            set { SetValue(VariableNameProperty, value); }
        }

        [Category("Activity")]
        public int Start
        {
            get { return (int)GetValue(StartProperty); }
            set { SetValue(StartProperty, value); }
        }

        static ForRange()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(ForRange), new FrameworkPropertyMetadata(typeof(ForRange)));
        }
    }
}
