using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Threading;
using System.Windows;
using KLibrary.Labs.UI.Input;
using FormsKeys = System.Windows.Forms.Keys;

namespace Speedrunner.Activities
{
    [DebuggerDisplay(@"\{{GetType().Name}: {Timeout} ms\}")]
    public class Delay : Activity
    {
        [DefaultValue(100)]
        public int Timeout { get; set; } = 100;

        public override void Execute(WorkflowContext context)
        {
            Sleep(Timeout);

            ExecuteAfterDelay();
        }

        protected virtual void ExecuteAfterDelay() { }

        static void Sleep(int timeoutInMilliseconds)
        {
            if (timeoutInMilliseconds <= 0) return;

            Thread.Sleep(timeoutInMilliseconds);
        }
    }

    public class MouseMove : Delay
    {
        [DefaultValue("0,0")]
        public Point Position { get; set; }

        protected override void ExecuteAfterDelay()
        {
            MouseInjection.Move(Position);
        }
    }

    public class Click : Delay
    {
        [DefaultValue("0,0")]
        public Point Position { get; set; }

        [DefaultValue(false)]
        public bool UsesCurrentPoint { get; set; }

        [DefaultValue(false)]
        public bool IsRightClick { get; set; }

        protected override void ExecuteAfterDelay()
        {
            var button = IsRightClick ? MouseButtonType.Right : MouseButtonType.Left;

            if (UsesCurrentPoint)
                MouseInjection.Click(button);
            else
                MouseInjection.Click(Position, button);
        }
    }

    public class KeyStroke : Delay
    {
        [DefaultValue("None")]
        public FormsKeys Key { get; set; }

        protected override void ExecuteAfterDelay()
        {
            if (Key == FormsKeys.None) return;

            KeyboardInjection.StrokeKey(Key);
        }
    }

    public class InputText : Delay
    {
        [DefaultValue("")]
        public string Text { get; set; } = "";

        protected override void ExecuteAfterDelay()
        {
            if (string.IsNullOrEmpty(Text)) return;

            KeyboardInjection.EnterCharacters(Text);
        }
    }
}
