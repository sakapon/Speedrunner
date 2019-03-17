using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
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
}
