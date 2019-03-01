using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace Speedrunner
{
    public static class ActivityHelper
    {
        static readonly Dictionary<string, Type> ActivityTypes =
            typeof(Activities.Activity).Assembly.ExportedTypes
                .Where(t => t.Namespace == typeof(Activities.Activity).Namespace)
                .ToDictionary(t => t.Name);

        public static Activities.Activity ToActivity(Control control)
        {
            if (control.GetType().Namespace != typeof(UI.Constants).Namespace) throw new ArgumentException();

            throw new NotImplementedException();
        }

        public static UI.SequentialWorkflow FindSequentialWorkflow(DependencyObject obj)
        {
            return GetLogicalDescendants(obj)
                .OfType<UI.SequentialWorkflow>()
                .FirstOrDefault();
        }

        public static IEnumerable<DependencyObject> GetLogicalDescendants(DependencyObject obj)
        {
            foreach (DependencyObject child in LogicalTreeHelper.GetChildren(obj))
            {
                yield return child;

                foreach (var descendant in GetLogicalDescendants(child))
                {
                    yield return descendant;
                }
            }
        }
    }
}
