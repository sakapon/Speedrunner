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
    public class ActivityTest
    {
        [TestMethod]
        public void Save_Workflow()
        {
            var path = $@"..\..\..\Activities\XAMLs\{nameof(Save_Workflow)}.xaml";
            var expected = File.ReadAllText(path);

            var wf = new SequentialWorkflow { Name = "Test" };
            wf.Activities.Add(new Delay { Timeout = 300 });
            var forRange = new ForRange { Start = 3, Count = 10 };
            forRange.Activities.Add(new Delay { Timeout = 300 });
            forRange.Activities.Add(new Delay { Timeout = 300 });
            wf.Activities.Add(forRange);

            Assert.AreEqual(expected, XamlServices.Save(wf));
        }
    }
}
