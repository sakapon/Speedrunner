using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Windows;

namespace Speedrunner.Activities
{
    public class WorkflowContext
    {
        public Activity Root { get; set; }
        public Point BasePosition { get; set; }
        public VariableCollection Variables { get; set; }
    }

    [DebuggerDisplay(@"\{{Name}\}")]
    public class Variable
    {
        [DefaultValue("")]
        public string Name { get; set; } = "";
        public Type Type { get; set; }
        public object Value { get; set; }
    }
}
