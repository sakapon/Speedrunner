using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
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
            var uiType = control.GetType();
            if (uiType.Namespace != typeof(UI.Constants).Namespace) return null;

            var type = ActivityTypes[uiType.Name];
            if (!type.IsSubclassOf(typeof(Activities.Activity))) return null;

            var activity = (Activities.Activity)Activator.CreateInstance(type);

            // Copies property values.
            var properties = uiType.GetProperties()
                .Where(p => p.GetCustomAttribute<CategoryAttribute>()?.Category == UI.Constants.CategoryName)
                .ToDictionary(p => p.Name, p => p.GetValue(control));
            foreach (var p in properties)
                type.GetProperty(p.Key).SetValue(activity, p.Value);

            // Adds children.
            if ((control is ItemsControl ic) && !ic.Items.IsEmpty)
            {
                var activities = (ICollection<Activities.Activity>)type.GetProperty("Activities").GetValue(activity);
                var children = ic.Items.OfType<Control>()
                    .Select(ToActivity)
                    .Where(a => a != null);
                foreach (var child in children)
                    activities.Add(child);
            }

            return activity;
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
