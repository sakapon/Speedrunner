using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using Speedrunner.Activities;
using Win32InputLib;

namespace InputRecorder
{
    public class InputManager
    {
        List<(Activity a, DateTime t)> activities = new List<(Activity, DateTime)>();

        MouseButton mouseLeft = new MouseButton();
        MouseButton mouseRight = new MouseButton();

        public InputManager()
        {
            mouseLeft.Click += o => activities.Add((new Click { Position = o.p }, o.t));
            mouseRight.Click += o => activities.Add((new Click { Position = o.p, IsRightClick = true }, o.t));
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
        static readonly TimeSpan ClickSpan = TimeSpan.FromMilliseconds(200);

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
}
