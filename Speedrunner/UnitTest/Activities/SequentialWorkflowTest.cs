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
            context.Variables.Add(new Variable { Name = "p", Value = 1.0 });
            context.Variables.Add(new Variable { Name = "sum", Value = 1.0 });

            var wf = new SequentialWorkflow();
            var forRange = new ForRange { StartNumber = 3, RepeatCount = 50, Step = 2 };
            forRange.Activities.Add(new CodeActivity { Type = typeof(SequentialWorkflowTest), MethodName = "NextP" });
            forRange.Activities.Add(new CodeActivity { Type = typeof(SequentialWorkflowTest), MethodName = "AddTerm" });
            wf.Activities.Add(forRange);
            wf.Activities.Add(new CodeActivity { Type = typeof(SequentialWorkflowTest), MethodName = "Sqrt12" });

            wf.Execute(context);

            AssertNearlyEqual(Math.PI, (double)context.Variables["sum"].Value);
        }

        public static void NextP(WorkflowContext context)
        {
            var varP = context.Variables["p"];
            varP.Value = -3 * (double)varP.Value;
        }

        public static void AddTerm(WorkflowContext context)
        {
            var varSum = context.Variables["sum"];
            var i = (int)context.Variables["i"].Value;
            var p = (double)context.Variables["p"].Value;
            varSum.Value = (double)varSum.Value + 1 / (i * p);
        }

        public static void Sqrt12(WorkflowContext context)
        {
            var varSum = context.Variables["sum"];
            varSum.Value = Math.Sqrt(12) * (double)varSum.Value;
        }
    }
}
