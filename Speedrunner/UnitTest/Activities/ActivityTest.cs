using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Forms;
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

            var wf = new SequentialWorkflow { Title = "Test" };
            wf.Activities.Add(new Delay { Timeout = 300 });
            var forRange = new ForRange { Start = 3, Count = 10 };
            forRange.Activities.Add(new Click { Position = new Point(0, 123) });
            forRange.Activities.Add(new KeyStroke { Timeout = 100, Key = Keys.Control | Keys.A });
            wf.Activities.Add(forRange);

            Assert.AreEqual(expected, XamlServices.Save(wf));
            var o = (SequentialWorkflow)XamlServices.Parse(expected);
        }
    }
}
