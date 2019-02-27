using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xaml;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Speedrunner.Activities;

namespace UnitTest.Activities
{
    [TestClass]
    public class WorkflowContextTest
    {
        [TestMethod]
        public void Save_Variables()
        {
            var path = $@"..\..\..\Activities\XAMLs\{nameof(Save_Variables)}.xaml";
            var expected = File.ReadAllText(path);

            var vs = new WorkflowVariables();
            vs.Variables.Add(new Variable<int> { Name = "i", Value = 123 });
            vs.Variables.Add(new Variable<double> { Name = "sum", Value = 1.0 });
            vs.Variables.Add(new Variable<string> { Name = "Message", Value = "Hello" });

            Assert.AreEqual(expected, XamlServices.Save(vs));
            var o = (WorkflowVariables)XamlServices.Parse(expected);
        }
    }
}
