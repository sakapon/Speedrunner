using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace Speedrunner.UI
{
    public class WorkflowVariables : ItemsControl
    {
        static WorkflowVariables()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(WorkflowVariables), new FrameworkPropertyMetadata(typeof(WorkflowVariables)));
        }
    }

    public class Variable : Control
    {
        public static readonly DependencyProperty TypeProperty =
            DependencyProperty.Register("Type", typeof(Type), typeof(Variable), new PropertyMetadata(null));

        public static readonly DependencyProperty VariableNameProperty =
            DependencyProperty.Register("VariableName", typeof(string), typeof(Variable), new PropertyMetadata(""));

        public static readonly DependencyProperty ValueProperty =
            DependencyProperty.Register("Value", typeof(object), typeof(Variable), new PropertyMetadata(null));

        [Category(Constants.CategoryName)]
        public Type Type
        {
            get { return (Type)GetValue(TypeProperty); }
            set { SetValue(TypeProperty, value); }
        }

        [Category(Constants.CategoryName)]
        public string VariableName
        {
            get { return (string)GetValue(VariableNameProperty); }
            set { SetValue(VariableNameProperty, value); }
        }

        [Category(Constants.CategoryName)]
        public object Value
        {
            get { return (object)GetValue(ValueProperty); }
            set { SetValue(ValueProperty, value); }
        }

        static Variable()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(Variable), new FrameworkPropertyMetadata(typeof(Variable)));
        }
    }
}
