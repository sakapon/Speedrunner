using System;
using System.ComponentModel;
using System.Windows;
using KLibrary.Labs.UI.Input;

namespace Speedrunner.Activities
{
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
