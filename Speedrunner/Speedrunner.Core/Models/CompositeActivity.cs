using System;
using System.ComponentModel;
using System.Windows.Markup;

namespace TasSample.Models
{
    [ContentProperty("Activities")]
    public abstract class CompositeActivity : Activity
    {
        private ActivityCollection activities;

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public ActivityCollection Activities
        {
            get
            {
                if (this.activities == null)
                {
                    this.activities = new ActivityCollection();
                }
                return this.activities;
            }
        }

        public override void ExecuteActivity()
        {
            foreach (Activity activity in this.Activities)
            {
                activity.ExecuteActivity();
            }
        }

        protected internal override void SetWorkflowId(Guid workflowId)
        {
            foreach (Activity activity in this.Activities)
            {
                activity.SetWorkflowId(workflowId);
            }
        }
    }
}
