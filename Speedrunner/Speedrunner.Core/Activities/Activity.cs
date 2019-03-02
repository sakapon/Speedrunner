using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Windows.Markup;

namespace Speedrunner.Activities
{
    [DebuggerDisplay(@"\{{GetType().Name}\}")]
    public abstract class Activity
    {
        public abstract void Execute(WorkflowContext context);
    }

    [ContentProperty("Activities")]
    public abstract class CompositeActivity : Activity
    {
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public Collection<Activity> Activities { get; } = new Collection<Activity>();

        public override void Execute(WorkflowContext context)
        {
            foreach (var activity in Activities)
            {
                activity.Execute(context);
                if (context.IsReturned) return;
            }
        }
    }

    public class Return : Activity
    {
        public override void Execute(WorkflowContext context)
        {
            context.IsReturned = true;
        }
    }

    [DebuggerDisplay(@"\{{GetType().Name}: {Timeout} ms\}")]
    public class Delay : Activity
    {
        [DefaultValue(0)]
        public int Timeout { get; set; }

        public override void Execute(WorkflowContext context)
        {
            Sleep(Timeout);

            ExecuteAfterDelay();
        }

        protected virtual void ExecuteAfterDelay() { }

        static void Sleep(int timeoutInMilliseconds)
        {
            if (timeoutInMilliseconds <= 0) return;

            Thread.Sleep(timeoutInMilliseconds);
        }
    }

    [DebuggerDisplay(@"\{{GetType().Name}: {Type.Name}.{MethodName}\}")]
    public class Code : Activity
    {
        public Type Type { get; set; }
        [DefaultValue("")]
        public string MethodName { get; set; } = "";

        public override void Execute(WorkflowContext context)
        {
            if (Type == null) throw new InvalidOperationException();
            if (string.IsNullOrWhiteSpace(MethodName)) throw new InvalidOperationException();

            var methods = Type.GetMethods(BindingFlags.Public | BindingFlags.Static | BindingFlags.InvokeMethod)
                .Where(m => m.Name == MethodName)
                .ToArray();

            if (methods.Length != 1) throw new InvalidOperationException();
            var method = methods[0];
            var parameters = method.GetParameters();

            if (parameters.Length == 0)
            {
                method.Invoke(null, null);
            }
            else if (parameters.Length == 1 && parameters[0].ParameterType == typeof(WorkflowContext))
            {
                method.Invoke(null, new[] { context });
            }
            else
            {
                throw new InvalidOperationException();
            }
        }
    }
}
