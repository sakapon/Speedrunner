using System.ComponentModel;
using System.Windows;
using TasSample.Automation;

namespace TasSample.Models
{
    public class ClickActivity : DelayActivity
    {
        [DefaultValue("")]
        public string LocationName { get; set; }

        [DefaultValue("0,0")]
        public Point Point { get; set; }

        [DefaultValue(false)]
        public bool CurrentPoint { get; set; }

        protected override void ExecuteActivityExtended()
        {
            if (string.IsNullOrEmpty(this.LocationName))
            {
                ScreenManager.Click();
            }
            else
            {
                ScreenManager.Click(this.Context.BasePosition + (Vector)this.Context.GetLocationPoint(this.LocationName));
            }
        }
    }
}
