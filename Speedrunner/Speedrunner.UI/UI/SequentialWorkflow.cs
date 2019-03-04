using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace Speedrunner.UI
{
    public class SequentialWorkflow : ItemsControl
    {
        public static readonly DependencyProperty TitleProperty =
            DependencyProperty.Register("Title", typeof(string), typeof(SequentialWorkflow), new PropertyMetadata(""));

        [Category(Constants.CategoryName)]
        public string Title
        {
            get { return (string)GetValue(TitleProperty); }
            set { SetValue(TitleProperty, value); }
        }

        static SequentialWorkflow()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(SequentialWorkflow), new FrameworkPropertyMetadata(typeof(SequentialWorkflow)));
        }

        public static SequentialWorkflow CurrentInDesign { get; private set; }

        public SequentialWorkflow()
        {
            if (DesignerProperties.GetIsInDesignMode(this))
                CurrentInDesign = this;
        }
    }
}
