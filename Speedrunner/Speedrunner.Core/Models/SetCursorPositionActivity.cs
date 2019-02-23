using System.ComponentModel;
using System.Windows;
using TasSample.Automation;

namespace TasSample.Models
{
    public class SetCursorPositionActivity : DelayActivity
    {
        [DefaultValue("")]
        public string LocationName { get; set; }

        [DefaultValue("0,0")]
        public Point Point { get; set; }

        protected override void ExecuteActivityExtended()
        {
            Point target = string.IsNullOrEmpty(this.LocationName) ? this.Point : this.Context.GetLocationPoint(this.LocationName);

            ScreenManager.SetCursorPosition(this.Context.BasePosition + (Vector)target);
        }
    }
}
