using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Windows.Markup;

namespace TasSample.Models
{
    [ContentProperty("Activities")]
    [DebuggerDisplay(@"\{{Name}\}")]
    public class SequentialScreenWorkflow
    {
        public Guid WorkflowId { get; private set; }

        [DefaultValue("")]
        public string Name { get; set; }

        [DefaultValue("")]
        public Uri Uri { get; set; }

        [DefaultValue("")]
        public string BasePositionDescription { get; set; }

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

        private LocationCollection locations;

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public LocationCollection Locations
        {
            get
            {
                if (this.locations == null)
                {
                    this.locations = new LocationCollection();
                }
                return this.locations;
            }
        }

        public SequentialScreenWorkflow()
        {
            this.WorkflowId = Guid.NewGuid();
        }

        public void Start()
        {
            try
            {
                ScreenWorkflowManager.Contexts[this.WorkflowId] = new ScreenWorkflowContext { Workflow = this };

                foreach (Activity activity in this.Activities)
                {
                    activity.SetWorkflowId(this.WorkflowId);
                }

                foreach (Activity activity in this.Activities)
                {
                    activity.ExecuteActivity();
                }
            }
            catch (ReturnException)
            {
            }
        }
    }
}
