using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace Speedrunner.UI
{
    static class Constants
    {
        public const string CategoryName = "Activity";
    }

    public abstract class Activity : Control
    {
        public string TypeName => GetType().Name;
    }

    public abstract class CompositeActivity : ItemsControl
    {
        public string TypeName => GetType().Name;
    }

    public class Return : Activity
    {
        static Return()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(Return), new FrameworkPropertyMetadata(typeof(Return)));
        }
    }
}
