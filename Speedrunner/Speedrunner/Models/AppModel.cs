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

        public void ExecuteWorkflow(DependencyObject obj)
        {
            Variables.Value = null;

            var workflow = (SequentialWorkflow)ActivityHelper.FindSequentialWorkflow(obj).ToModel();
            var variables = (WorkflowVariables)ActivityHelper.FindWorkflowVariables(obj).ToModel();
            var context = new WorkflowContext { Variables = variables.Variables.ToTyped() };

            Task.Run(() =>
            {
                workflow.Execute(context);
                Variables.Value = context.Variables;
            });
        }
    }
}
