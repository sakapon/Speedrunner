using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;

namespace TasSample.Models
{
    public class ScreenWorkflowContext
    {
        // TODO:
        public static Point CurrentBasePosition { get; set; }

        public SequentialScreenWorkflow Workflow { get; set; }

        public Point BasePosition
        {
            get { return CurrentBasePosition; }
        }

        public Point GetLocationPoint(string locationName)
        {
            return this.Workflow.Locations[locationName].Point;
        }
    }
}
