using System;
using System.Collections.Generic;
using System.Linq;
using System.Timers;
using System.Windows;
using Speedrunner.Activities;
using Win32InputLib;
using Keys = System.Windows.Forms.Keys;

namespace InputRecorder
{
    public class InputManager
    {
        List<(Activity a, DateTime t)> activities = new List<(Activity, DateTime)>();

        MouseButton mouseLeft = new MouseButton();
        MouseButton mouseRight = new MouseButton();
        Keyboard keyboard = new Keyboard();

        public InputManager()
        {
            mouseLeft.Click += o => activities.Add((new Click { Position = o.p }, o.t));
            mouseRight.Click += o => activities.Add((new Click { Position = o.p, IsRightClick = true }, o.t));
            keyboard.KeyStroke += o => activities.Add((new KeyStroke { Key = o.k }, o.t));
            keyboard.TextInput += o => activities.Add((new InputText { Text = o.s }, o.t));
        }

        public void NotifyMouse(ref MouseHook.StateMouse s)
        {
            switch (s.Stroke)
            {
                case MouseHook.Stroke.MOVE:
                    break;
                case MouseHook.Stroke.LEFT_DOWN:
                    mouseLeft.Down(ToPoint(s));
                    break;
                case MouseHook.Stroke.LEFT_UP:
                    mouseLeft.Up(ToPoint(s));
                    break;
                case MouseHook.Stroke.RIGHT_DOWN:
                    mouseRight.Down(ToPoint(s));
                    break;
                case MouseHook.Stroke.RIGHT_UP:
                    mouseRight.Up(ToPoint(s));
                    break;
                default:
                    break;
            }
        }

        public void NotifyKeyboard(ref KeyboardHook.StateKeyboard s)
        {
            switch (s.Stroke)
            {
                case KeyboardHook.Stroke.KEY_DOWN:
                case KeyboardHook.Stroke.SYSKEY_DOWN:
                    keyboard.Down(s.Key);
                    break;
                case KeyboardHook.Stroke.KEY_UP:
                case KeyboardHook.Stroke.SYSKEY_UP:
                    keyboard.Up(s.Key);
                    break;
                default:
                    break;
            }
        }

        static readonly TimeSpan InvocationSpan = TimeSpan.FromMilliseconds(100);

        public SequentialWorkflow GetWorkflow()
        {
            var now = DateTime.Now;
            for (var i = activities.Count - 1; i >= 0 && now - activities[i].t < InvocationSpan; i--)
                activities.RemoveAt(i);

            var workflow = new SequentialWorkflow { Title = "By Input Recorder" };
            foreach (var item in activities.Select(_ => _.a))
                workflow.Activities.Add(item);

            activities.Clear();
            return workflow;
        }

        public static Point ToPoint(MouseHook.StateMouse s) => new Point(s.X, s.Y);
    }

    public class MouseButton
    {
        static readonly TimeSpan ClickSpan = TimeSpan.FromMilliseconds(300);

        public event Action<(Point p, DateTime t)> Click;
        public event Action<(Point, Point, DateTime)> Drag;

        bool isDown;
        Point pDown;
        DateTime dtDown;

        public void Down(Point p)
        {
            isDown = true;
            pDown = p;
            dtDown = DateTime.Now;
        }

        public void Up(Point p)
        {
            if (!isDown) return;

            var now = DateTime.Now;
            if (p == pDown || now - dtDown < ClickSpan)
                Click?.Invoke((p, now));
            else
                Drag?.Invoke((pDown, p, now));

            isDown = false;
        }
    }

    public class Keyboard
    {
        public event Action<(Keys k, DateTime t)> KeyStroke;
        public event Action<(string s, DateTime t)> TextInput;

        Keys modifier;
        string text = "";
        string textForTimer = "";
        Timer timer = new Timer(1000);

        public Keyboard()
        {
            timer.Elapsed += (o, e) =>
            {
                if (text != "" && textForTimer == text)
                {
                    TextInput?.Invoke((text, DateTime.Now));
                    text = "";
                }
                textForTimer = text;
            };
            timer.Start();
        }

        public void Down(Keys key)
        {
            var m = ToModifier(key);
            modifier |= m;
            if (m != Keys.None) return;

            var s = ToLetter(key);
            if (s != null && modifier == Keys.None)
                text += s.ToLowerInvariant();
            else if (s != null && modifier == Keys.Shift)
                text += s;
            else
            {
                if (text != "")
                {
                    TextInput?.Invoke((text, DateTime.Now));
                    text = "";
                }
                KeyStroke?.Invoke((modifier | key, DateTime.Now));
            }
        }

        public void Up(Keys key)
        {
            modifier ^= ToModifier(key);
        }

        static Keys ToModifier(Keys key)
        {
            switch (key)
            {
                case Keys.ShiftKey:
                case Keys.LShiftKey:
                case Keys.RShiftKey:
                case Keys.Shift:
                    return Keys.Shift;
                case Keys.ControlKey:
                case Keys.LControlKey:
                case Keys.RControlKey:
                case Keys.Control:
                    return Keys.Control;
                case Keys.Menu:
                case Keys.LMenu:
                case Keys.RMenu:
                case Keys.Alt:
                    return Keys.Alt;
                case Keys.LWin:
                case Keys.RWin:
                default:
                    return Keys.None;
            }
        }

        static string ToLetter(Keys key)
        {
            if (Keys.D0 <= key && key <= Keys.D9)
                return key.ToString().TrimStart('D');
            else if (Keys.A <= key && key <= Keys.Z)
                return key.ToString();
            else
                return null;
        }
    }
}
