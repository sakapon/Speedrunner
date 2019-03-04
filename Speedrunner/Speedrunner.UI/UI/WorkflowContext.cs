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

        public static WorkflowVariables CurrentInDesign { get; private set; }

        public WorkflowVariables()
        {
            if (DesignerProperties.GetIsInDesignMode(this))
                CurrentInDesign = this;
        }
    }

    public class WorkflowResult : ItemsControl
    {
        public static readonly DependencyProperty IsActiveInDesignProperty =
            DependencyProperty.Register("IsActiveInDesign", typeof(bool), typeof(WorkflowResult), new PropertyMetadata(false));

        [Category(Constants.CategoryName)]
        public bool IsActiveInDesign
        {
            get { return (bool)GetValue(IsActiveInDesignProperty); }
            set { SetValue(IsActiveInDesignProperty, value); }
        }

        static WorkflowResult()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(WorkflowResult), new FrameworkPropertyMetadata(typeof(WorkflowResult)));
        }
    }

    public class Variable : Control
    {
        public static readonly DependencyProperty TypeProperty =
            DependencyProperty.Register("Type", typeof(Type), typeof(Variable), new PropertyMetadata(null));

        public static readonly DependencyProperty VariableNameProperty =
            DependencyProperty.Register("VariableName", typeof(string), typeof(Variable), new PropertyMetadata(""));

        public static readonly DependencyProperty ValueProperty =
            DependencyProperty.Register("Value", typeof(string), typeof(Variable), new PropertyMetadata(null));

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
        public string Value
        {
            get { return (string)GetValue(ValueProperty); }
            set { SetValue(ValueProperty, value); }
        }

        static Variable()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(Variable), new FrameworkPropertyMetadata(typeof(Variable)));
        }
    }
}
