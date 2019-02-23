using System.ComponentModel;
using System.Windows;
using System.Windows.Markup;

namespace TasSample.Models
{
    public class ScreenWorkflowStartInfo
    {
        [DefaultValue("")]
        public string Name { get; set; }

        [DefaultValue("")]
        public string FilePath { get; set; }

        [DefaultValue("0,0")]
        public Point BasePosition { get; set; }
    }
}
