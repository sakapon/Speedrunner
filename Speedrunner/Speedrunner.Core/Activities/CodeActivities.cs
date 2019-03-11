using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text.RegularExpressions;

namespace Speedrunner.Activities
{
    [DebuggerDisplay(@"\{{GetType().Name}: {Text}\}")]
    public class Expression : Activity
    {
        static readonly Regex ExpressionPattern = new Regex(@"^\s*(.+?)\s*\S?=.+$");

        [DefaultValue("")]
        public string Text { get; set; } = "";

        public override void Execute(WorkflowContext context)
        {
            throw new NotImplementedException();
        }
    }
}
