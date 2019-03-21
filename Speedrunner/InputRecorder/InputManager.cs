using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using Win32InputLib;

namespace InputRecorder
{
    public class InputManager
    {
        public static Point ToPoint(MouseHook.StateMouse s) => new Point(s.X, s.Y);
    }

    public class MouseButton
    {
        static readonly TimeSpan ClickSpan = TimeSpan.FromMilliseconds(100);

        public event Action<(Point, DateTime)> Click;
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
