using System;
using System.Diagnostics;

namespace TasSample.Models
{
    [DebuggerDisplay(@"\{{this.GetType().Name}\}")]
    public abstract class Activity
    {
        public Guid WorkflowId { get; private set; }

        protected ScreenWorkflowContext Context
        {
            get { return ScreenWorkflowManager.Contexts[this.WorkflowId]; }
        }

        public abstract void ExecuteActivity();

        protected internal virtual void SetWorkflowId(Guid workflowId)
        {
            this.WorkflowId = workflowId;
        }
    }
}
