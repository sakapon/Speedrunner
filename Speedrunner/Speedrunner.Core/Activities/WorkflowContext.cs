using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Windows;
using System.Windows.Markup;

namespace Speedrunner.Activities
{
    public class WorkflowContext
    {
        public bool IsReturned { get; set; }
        public Exception Error { get; set; }
        public Activity Root { get; set; }
        public Point BasePosition { get; set; }
        public VariableCollection Variables { get; } = new VariableCollection();
    }

    [ContentProperty("Variables")]
    public class WorkflowVariables
    {
        public VariableCollection Variables { get; } = new VariableCollection();
    }

    public abstract class Variable
    {
        [DefaultValue("")]
        public string Name { get; set; } = "";
    }

    [DebuggerDisplay(@"\{{Name}: {Value}\}")]
    public class Variable<T> : Variable
    {
        public T Value { get; set; }
        public Type Type => typeof(T);
    }
}
