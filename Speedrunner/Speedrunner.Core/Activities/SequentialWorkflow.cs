using System;
using System.ComponentModel;
using System.Diagnostics;

namespace Speedrunner.Activities
{
    [DebuggerDisplay(@"\{{Title}\}")]
    public class SequentialWorkflow : CompositeActivity
    {
        [DefaultValue("")]
        public string Title { get; set; } = "";

        public override void Execute(WorkflowContext context)
        {
            try
            {
                base.Execute(context);
            }
            catch (Exception ex)
            {
                context.IsReturned = true;
                context.Error = ex;
                var varError = context.Variables.Get<string>("$error");
                varError.Value = ex.Message;
            }
        }
    }
}
