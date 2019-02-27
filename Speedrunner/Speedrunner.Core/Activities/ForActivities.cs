using System;
using System.ComponentModel;
using System.Diagnostics;

namespace Speedrunner.Activities
{
    [DebuggerDisplay(@"\{{GetType().Name}: {Count} times for {VariableName} = {Start} by {Step}\}")]
    public class ForRange : CompositeActivity
    {
        [DefaultValue("i")]
        public string VariableName { get; set; } = "i";
        [DefaultValue(0)]
        public int Start { get; set; }
        [DefaultValue(0)]
        public int Count { get; set; }
        [DefaultValue(1)]
        public int Step { get; set; } = 1;

        public override void Execute(WorkflowContext context)
        {
            var varN = context.Variables[VariableName];
            if (varN == null)
            {
                varN = new Variable { Name = VariableName, Type = typeof(int), Value = 0 };
                context.Variables.Add(varN);
            }

            var n = Start;
            for (var i = 0; i < Count; i++)
            {
                varN.Value = n;
                base.Execute(context);
                if (context.IsReturned) return;
                n += Step;
            }
        }
    }

    public class ForDuration : CompositeActivity
    {
        [DefaultValue("00:00:00")]
        public TimeSpan Duration { get; set; }

        [DefaultValue(ActivitiesUnit.One)]
        public ActivitiesUnit CheckingTimeUnit { get; set; }

        public override void Execute(WorkflowContext context)
        {
            var stopwatch = Stopwatch.StartNew();

            try
            {
                switch (CheckingTimeUnit)
                {
                    case ActivitiesUnit.One:
                        while (stopwatch.Elapsed < Duration)
                        {
                            foreach (var activity in Activities)
                            {
                                if (stopwatch.Elapsed >= Duration) return;
                                activity.Execute(context);
                                if (context.IsReturned) return;
                            }
                        }
                        break;

                    case ActivitiesUnit.All:
                        while (stopwatch.Elapsed < Duration)
                        {
                            base.Execute(context);
                        }
                        break;

                    default:
                        throw new InvalidOperationException();
                }
            }
            finally
            {
                stopwatch.Stop();
            }
        }
    }

    public enum ActivitiesUnit
    {
        One,
        All,
    }
}
