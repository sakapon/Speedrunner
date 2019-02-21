using System;
using System.ComponentModel;
using System.Diagnostics;

namespace TasSample.Models
{
    public class ForTimeActivity : CompositeActivity
    {
        [DefaultValue("00:00:00")]
        public TimeSpan Duration { get; set; }

        [DefaultValue(ActivitiesUnit.OneActivity)]
        public ActivitiesUnit CheckingTimeActivitiesUnit { get; set; }

        public override void ExecuteActivity()
        {
            Stopwatch stopwatch = Stopwatch.StartNew();

            try
            {
                switch (this.CheckingTimeActivitiesUnit)
                {
                    case ActivitiesUnit.OneActivity:
                        while (stopwatch.Elapsed < this.Duration)
                        {
                            foreach (Activity activity in this.Activities)
                            {
                                if (stopwatch.Elapsed >= this.Duration)
                                {
                                    break;
                                }
                                activity.ExecuteActivity();
                            }
                        }
                        break;

                    case ActivitiesUnit.AllActivities:
                        while (stopwatch.Elapsed < this.Duration)
                        {
                            base.ExecuteActivity();
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
}
