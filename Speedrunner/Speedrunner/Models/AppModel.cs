using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using Reactive.Bindings;
using Speedrunner.Activities;

namespace Speedrunner.Models
{
    public class AppModel
    {
        public ReactiveProperty<VariableCollection> Variables { get; } = new ReactiveProperty<VariableCollection>();

        public AppModel()
        {
            ExecuteWorkflowInDesign();
        }

        async void ExecuteWorkflowInDesign()
        {
            await Task.Delay(1000);
            if (UI.SequentialWorkflow.CurrentInDesign?.IsActiveInDesign != true) return;
            ExecuteWorkflow(UI.SequentialWorkflow.CurrentInDesign, UI.WorkflowVariables.CurrentInDesign);
        }

        public void ExecuteWorkflow(DependencyObject obj)
        {
            ExecuteWorkflow(ActivityHelper.FindSequentialWorkflow(obj), ActivityHelper.FindWorkflowVariables(obj));
        }

        public void ExecuteWorkflow(UI.SequentialWorkflow workflowUI, UI.WorkflowVariables variablesUI)
        {
            Variables.Value = null;

            var workflow = (SequentialWorkflow)workflowUI.ToModel();
            var variables = (WorkflowVariables)variablesUI.ToModel();
            var context = new WorkflowContext { Variables = VariableCollection.ToTyped(variables.Variables) };

            Task.Run(() =>
            {
                workflow.Execute(context);
                Variables.Value = context.Variables;
            });
        }
    }
}
