using System;
using System.ComponentModel;
using System.Diagnostics;
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
        public ActivityCollection Activities { get; } = new ActivityCollection();

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

    [ContentProperty("Timeout")]
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
}
