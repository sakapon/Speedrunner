using System;
using System.ComponentModel;
using System.Diagnostics;
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
                activity.Execute(context);
        }
    }
}
