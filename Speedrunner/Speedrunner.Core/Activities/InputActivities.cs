using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Threading;
using System.Windows;
using KLibrary.Labs.UI.Input;

namespace Speedrunner.Activities
{
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

    public class Click : Delay
    {
        [DefaultValue("0,0")]
        public Point Position { get; set; }

        [DefaultValue(false)]
        public bool UsesCurrentPoint { get; set; }

        protected override void ExecuteAfterDelay()
        {
            if (UsesCurrentPoint)
                MouseInjection.Click();
            else
                MouseInjection.Click(Position);
        }
    }
}
