using System.ComponentModel;

namespace TasSample.Models
{
    public class ReadClipboardActivity : DelayActivity
    {
        [DefaultValue("")]
        public string Format { get; set; }

        [DefaultValue("")]
        public string Variable { get; set; }
    }
}
