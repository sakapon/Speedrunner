using System;
using System.Collections.ObjectModel;
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
        public VariableCollection Variables { get; set; } = new VariableCollection();
    }

    [ContentProperty("Variables")]
    public class WorkflowVariables
    {
        public Collection<Variable> Variables { get; } = new Collection<Variable>();
    }

    [DebuggerDisplay(@"\{{Type.Name} {VariableName} = {ActualValue}\}")]
    public abstract class VariableBase
    {
        public abstract Type Type { get; set; }
        [DefaultValue("")]
        public string VariableName { get; set; } = "";
        public abstract object ActualValue { get; set; }

        public static VariableBase CreateTyped(string name, Type type)
        {
            var varType = typeof(Variable<>).MakeGenericType(type);
            var v = (VariableBase)Activator.CreateInstance(varType);
            v.VariableName = name;
            return v;
        }

        public static VariableBase CreateTyped(string name, Type type, object value)
        {
            var v = CreateTyped(name, type);
            v.ActualValue = value;
            return v;
        }
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

        public VariableBase ToTyped()
        {
            var toTyped = typeof(Variable).GetMethod(nameof(ToTyped), BindingFlags.NonPublic | BindingFlags.Instance)
                .MakeGenericMethod(Type);
            return (VariableBase)toTyped.Invoke(this, null);
        }

        Variable<T> ToTyped<T>() =>
            new Variable<T>
            {
                VariableName = VariableName,
                ActualValue = ActualValue,
            };
    }

    public class Variable<T> : VariableBase
    {
        public override Type Type
        {
            get => typeof(T);
            set => throw new NotSupportedException();
        }

        public T Value { get; set; }

        public override object ActualValue
        {
            get => Value;
            set => Value = (T)value;
        }

        public static implicit operator T(Variable<T> v) => v.Value;
    }
}
