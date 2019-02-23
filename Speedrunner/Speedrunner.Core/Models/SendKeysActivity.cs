using System.ComponentModel;
using TasSample.Automation;

namespace TasSample.Models
{
    public class SendKeysActivity : DelayActivity
    {
        [DefaultValue("")]
        public string Keys { get; set; }

        protected override void ExecuteActivityExtended()
        {
            ScreenManager.SendKeys(this.Keys);
        }
    }
}
