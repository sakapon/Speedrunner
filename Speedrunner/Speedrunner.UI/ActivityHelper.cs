using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Markup;

namespace Speedrunner
{
    public static class ActivityHelper
    {
        static readonly Dictionary<string, Type> ActivityTypes =
            typeof(Activities.Activity).Assembly.ExportedTypes
                .Where(t => t.Namespace == typeof(Activities.Activity).Namespace)
                .ToDictionary(t => t.Name);

        public static object ToModel(this Control control)
        {
            var uiType = control.GetType();
            if (uiType.Namespace != typeof(UI.Constants).Namespace) return null;

            var type = ActivityTypes[uiType.Name];
            var model = Activator.CreateInstance(type);

            // Copies property values.
            var properties = uiType.GetProperties()
                .Where(p => p.GetCustomAttribute<CategoryAttribute>()?.Category == UI.Constants.CategoryName)
                .ToDictionary(p => p.Name, p => p.GetValue(control));
            foreach (var p in properties)
                type.GetProperty(p.Key).SetValue(model, p.Value);

            // Adds children.
            if ((control is ItemsControl ic) && !ic.Items.IsEmpty)
            {
                var collectionName = type.GetCustomAttribute<ContentPropertyAttribute>().Name;
                var collection = (IList)type.GetProperty(collectionName).GetValue(model);
                var children = ic.Items.OfType<Control>()
                    .Select(ToModel)
                    .Where(a => a != null);
                foreach (var child in children)
                    collection.Add(child);
            }

            return model;
        }

        public static UI.SequentialWorkflow FindSequentialWorkflow(DependencyObject obj)
        {
            return GetLogicalDescendants(obj)
                .OfType<UI.SequentialWorkflow>()
                .FirstOrDefault();
        }

        public static UI.WorkflowVariables FindWorkflowVariables(DependencyObject obj)
        {
            return GetLogicalDescendants(obj)
                .OfType<UI.WorkflowVariables>()
                .FirstOrDefault();
        }

        public static IEnumerable<DependencyObject> GetLogicalDescendants(DependencyObject obj)
        {
            foreach (var child in LogicalTreeHelper.GetChildren(obj).OfType<DependencyObject>())
            {
                yield return child;

                foreach (var descendant in GetLogicalDescendants(child))
                {
                    yield return descendant;
                }
            }
        }

        public static IEnumerable<DependencyObject> GetLogicalAncestors(DependencyObject obj)
        {
            var current = obj;
            while ((current = LogicalTreeHelper.GetParent(current)) != null)
                yield return current;
        }
    }
}
