using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Reflection;
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

    [DebuggerDisplay(@"\{{Type.Name} {VariableName} = {Value}\}")]
    public class Variable
    {
        public Type Type { get; set; }
        [DefaultValue("")]
        public string VariableName { get; set; } = "";
        public string Value { get; set; }

        public object ActualValue
        {
            get
            {
                var c = TypeDescriptor.GetConverter(Type);
                return c.ConvertFrom(Value);
            }
        }

        public Variable ToTyped()
        {
            var toTyped = typeof(Variable).GetMethod(nameof(ToTyped), BindingFlags.NonPublic | BindingFlags.Instance)
                .MakeGenericMethod(Type);
            return (Variable)toTyped.Invoke(this, null);
        }

        Variable ToTyped<T>() =>
            new Variable<T>
            {
                VariableName = VariableName,
                Value = (T)ActualValue,
            };
    }

    [DebuggerDisplay(@"\{{Type.Name} {VariableName} = {Value}\}")]
    public class Variable<T> : Variable
    {
        public new Type Type => typeof(T);
        public new T Value { get; set; }
        public new T ActualValue => Value;
    }
}
