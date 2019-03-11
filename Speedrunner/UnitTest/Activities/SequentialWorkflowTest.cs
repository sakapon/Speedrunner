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
        public void GetPi_Expression()
        {
            var context = new WorkflowContext();
            context.Variables.Add(new Variable<double> { VariableName = "sum", Value = 1.0 });
            context.Variables.Add(new Variable<double> { VariableName = "p", Value = 1.0 });

            var wf = new SequentialWorkflow();
            var forRange = new ForRange { Start = 3, Count = 50, Step = 2 };
            forRange.Activities.Add(new Expression { Text = "p *= -3" });
            forRange.Activities.Add(new Expression { Text = "sum += 1 / (i * p)" });
            wf.Activities.Add(forRange);
            wf.Activities.Add(new Expression { Text = "pi = System.Math.Sqrt(12) * sum" });

            wf.Execute(context);

            AssertNearlyEqual(Math.PI, context.Variables.Get<double>("pi"));
        }

        [TestMethod]
        public void GetPi_Methods()
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

            AssertNearlyEqual(Math.PI, context.Variables.Get<double>("pi"));
        }

        public static void NextP(WorkflowContext context)
        {
            var p = context.Variables.Get<double>("p");
            p.Value *= -3;
        }

        public static void AddTerm(WorkflowContext context)
        {
            var sum = context.Variables.Get<double>("sum");
            var p = context.Variables.Get<double>("p");
            var i = context.Variables.Get<int>("i");
            sum.Value += 1 / (i * p);
        }

        public static void Sqrt12(WorkflowContext context)
        {
            var sum = context.Variables.Get<double>("sum");
            var pi = context.Variables.Get<double>("pi");
            pi.Value = Math.Sqrt(12) * sum;
        }
    }
}
