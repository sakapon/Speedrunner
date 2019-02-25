using System;
using System.ComponentModel;

namespace Speedrunner.Activities
{
    public class ForRangeActivity : CompositeActivity
    {
        [DefaultValue("i")]
        public string VariableName { get; set; }
        [DefaultValue(0)]
        public int StartNumber { get; set; }
        [DefaultValue(0)]
        public int RepeatCount { get; set; }
        [DefaultValue(1)]
        public int Step { get; set; }

        public override void Execute(WorkflowContext context)
        {
            var varN = context.Variables[VariableName];
            if (varN == null)
            {
                varN = new Variable { Name = VariableName, Type = typeof(int), Value = 0 };
                context.Variables.Add(varN);
            }

            var n = StartNumber;
            for (var i = 0; i < RepeatCount; i++)
            {
                varN.Value = n;
                base.Execute(context);
                n += Step;
            }
        }
    }
}
