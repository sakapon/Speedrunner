using System.ComponentModel;
using System.Threading;
using System.Windows.Markup;

namespace TasSample.Models
{
    [ContentProperty("Timeout")]
    public class DelayActivity : Activity
    {
        [DefaultValue(0)]
        public int Timeout { get; set; }

        public override void ExecuteActivity()
        {
            Sleep(this.Timeout);

            this.ExecuteActivityExtended();
        }

        protected virtual void ExecuteActivityExtended()
        {
        }

        private static void Sleep(int millisecondsTimeout)
        {
            if (millisecondsTimeout <= 0)
            {
                return;
            }
            Thread.Sleep(millisecondsTimeout);
        }
    }
}
