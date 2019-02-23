using System.ComponentModel;

namespace TasSample.Models
{
    public class ForCountActivity : CompositeActivity
    {
        [DefaultValue(0)]
        public int RepeatCount { get; set; }

        public override void ExecuteActivity()
        {
            for (int i = 0; i < this.RepeatCount; i++)
            {
                base.ExecuteActivity();
            }
        }
    }
}
