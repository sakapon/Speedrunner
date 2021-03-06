﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace Speedrunner.UI
{
    public class ForRange : CompositeActivity
    {
        public static readonly DependencyProperty VariableNameProperty =
            DependencyProperty.Register("VariableName", typeof(string), typeof(ForRange), new PropertyMetadata("i"));

        public static readonly DependencyProperty StartProperty =
            DependencyProperty.Register("Start", typeof(int), typeof(ForRange), new PropertyMetadata(0));

        public static readonly DependencyProperty CountProperty =
            DependencyProperty.Register("Count", typeof(int), typeof(ForRange), new PropertyMetadata(0));

        public static readonly DependencyProperty StepProperty =
            DependencyProperty.Register("Step", typeof(int), typeof(ForRange), new PropertyMetadata(1));

        [Category(Constants.CategoryName)]
        public string VariableName
        {
            get { return (string)GetValue(VariableNameProperty); }
            set { SetValue(VariableNameProperty, value); }
        }

        [Category(Constants.CategoryName)]
        public int Start
        {
            get { return (int)GetValue(StartProperty); }
            set { SetValue(StartProperty, value); }
        }

        [Category(Constants.CategoryName)]
        public int Count
        {
            get { return (int)GetValue(CountProperty); }
            set { SetValue(CountProperty, value); }
        }

        [Category(Constants.CategoryName)]
        public int Step
        {
            get { return (int)GetValue(StepProperty); }
            set { SetValue(StepProperty, value); }
        }

        static ForRange()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(ForRange), new FrameworkPropertyMetadata(typeof(ForRange)));
        }
    }
}
