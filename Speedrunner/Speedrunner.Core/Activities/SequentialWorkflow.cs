using System;
using System.ComponentModel;
using System.Diagnostics;

namespace Speedrunner.Activities
{
    [DebuggerDisplay(@"\{{Name}\}")]
    public class SequentialWorkflow : CompositeActivity
    {
        [DefaultValue("")]
        public string Name { get; set; } = "";
    }
}
