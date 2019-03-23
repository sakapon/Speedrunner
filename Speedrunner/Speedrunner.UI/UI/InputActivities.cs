using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using Keys = System.Windows.Forms.Keys;

namespace Speedrunner.UI
{
    public class Delay : Activity
    {
        public static readonly DependencyProperty TimeoutProperty =
            DependencyProperty.Register("Timeout", typeof(int), typeof(Delay), new PropertyMetadata(500));

        [Category(Constants.CategoryName)]
        public int Timeout
        {
            get { return (int)GetValue(TimeoutProperty); }
            set { SetValue(TimeoutProperty, value); }
        }

        static Delay()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(Delay), new FrameworkPropertyMetadata(typeof(Delay)));
        }
    }

    public class MouseMove : Delay
    {
        public static readonly DependencyProperty PositionProperty =
            DependencyProperty.Register("Position", typeof(Point), typeof(MouseMove), new PropertyMetadata(new Point()));

        [Category(Constants.CategoryName)]
        public Point Position
        {
            get { return (Point)GetValue(PositionProperty); }
            set { SetValue(PositionProperty, value); }
        }

        static MouseMove()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(MouseMove), new FrameworkPropertyMetadata(typeof(MouseMove)));
        }
    }

    public class Click : Delay
    {
        public static readonly DependencyProperty PositionProperty =
            DependencyProperty.Register("Position", typeof(Point), typeof(Click), new PropertyMetadata(new Point()));

        public static readonly DependencyProperty UsesCurrentPointProperty =
            DependencyProperty.Register("UsesCurrentPoint", typeof(bool), typeof(Click), new PropertyMetadata(false));

        public static readonly DependencyProperty IsRightClickProperty =
            DependencyProperty.Register("IsRightClick", typeof(bool), typeof(Click), new PropertyMetadata(false));

        [Category(Constants.CategoryName)]
        public Point Position
        {
            get { return (Point)GetValue(PositionProperty); }
            set { SetValue(PositionProperty, value); }
        }

        [Category(Constants.CategoryName)]
        public bool UsesCurrentPoint
        {
            get { return (bool)GetValue(UsesCurrentPointProperty); }
            set { SetValue(UsesCurrentPointProperty, value); }
        }

        [Category(Constants.CategoryName)]
        public bool IsRightClick
        {
            get { return (bool)GetValue(IsRightClickProperty); }
            set { SetValue(IsRightClickProperty, value); }
        }

        static Click()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(Click), new FrameworkPropertyMetadata(typeof(Click)));
        }
    }

    public class KeyStroke : Delay
    {
        public static readonly DependencyProperty KeyProperty =
            DependencyProperty.Register("Key", typeof(Keys), typeof(KeyStroke), new PropertyMetadata(Keys.None));

        [Category(Constants.CategoryName)]
        public Keys Key
        {
            get { return (Keys)GetValue(KeyProperty); }
            set { SetValue(KeyProperty, value); }
        }

        static KeyStroke()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(KeyStroke), new FrameworkPropertyMetadata(typeof(KeyStroke)));
        }
    }

    public class InputText : Delay
    {
        public static readonly DependencyProperty TextProperty =
            DependencyProperty.Register("Text", typeof(string), typeof(InputText), new PropertyMetadata(""));

        [Category(Constants.CategoryName)]
        public string Text
        {
            get { return (string)GetValue(TextProperty); }
            set { SetValue(TextProperty, value); }
        }

        static InputText()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(InputText), new FrameworkPropertyMetadata(typeof(InputText)));
        }
    }
}
