using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Speedrunner.Activities;

namespace UnitTest.Activities
{
    [TestClass]
    public class SequentialWorkflowTest
    {
        static void AssertNearlyEqual(double expected, double actual) =>
            Assert.AreEqual(0.0, Math.Round(expected - actual, 12));

        [TestMethod]
        public void GetPi()
        {
            var context = new WorkflowContext();
            context.Variables.Add(new Variable<double> { VariableName = "sum", Value = 1.0 });
            context.Variables.Add(new Variable<double> { VariableName = "p", Value = 1.0 });

            var wf = new SequentialWorkflow();
            var forRange = new ForRange { Start = 3, Count = 50, Step = 2 };
            forRange.Activities.Add(new Code { Type = typeof(SequentialWorkflowTest), MethodName = "NextP" });
            forRange.Activities.Add(new Code { Type = typeof(SequentialWorkflowTest), MethodName = "AddTerm" });
            wf.Activities.Add(forRange);
            wf.Activities.Add(new Code { Type = typeof(SequentialWorkflowTest), MethodName = "Sqrt12" });

            wf.Execute(context);

            AssertNearlyEqual(Math.PI, context.Variables.Get<double>("sum").Value);
        }

        public static void NextP(WorkflowContext context)
        {
            var varP = context.Variables.Get<double>("p");
            varP.Value *= -3;
        }

        public static void AddTerm(WorkflowContext context)
        {
            var varSum = context.Variables.Get<double>("sum");
            var p = context.Variables.Get<double>("p").Value;
            var i = context.Variables.Get<int>("i").Value;
            varSum.Value += 1 / (i * p);
        }

        public static void Sqrt12(WorkflowContext context)
        {
            var varSum = context.Variables.Get<double>("sum");
            varSum.Value *= Math.Sqrt(12);
        }
    }
}
