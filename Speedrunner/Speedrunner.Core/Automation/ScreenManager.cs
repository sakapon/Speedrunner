using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Windows;
using SendKeysClass = System.Windows.Forms.SendKeys;

namespace TasSample.Automation
{
    public static class ScreenManager
    {
        [DllImport("user32.dll")]
        private static extern bool GetCursorPos(ref POINT p);

        [DllImport("user32.dll")]
        private extern static bool SetCursorPos(int x, int y);

        [DllImport("user32.dll")]
        private extern static void mouse_event(
            MouseEvents dwFlags,         // 移動とクリックのオプション
            uint dx,              // 水平位置または移動量
            uint dy,              // 垂直位置または移動量
            uint dwData,          // ホイールの移動
            uint dwExtraInfo  // アプリケーション定義の情報
        );

        public static Point GetCursorPosition()
        {
            POINT p = new POINT();

            GetCursorPos(ref p);

            return new Point(p.x, p.y);
        }

        public static void SetCursorPosition(Point point)
        {
            SetCursorPos((int)point.X, (int)point.Y);
        }

        public static void Click()
        {
            mouse_event(MouseEvents.MOUSEEVENTF_LEFTDOWN, 0, 0, 0, 0);
            mouse_event(MouseEvents.MOUSEEVENTF_LEFTUP, 0, 0, 0, 0);
        }

        public static void Click(Point point)
        {
            SetCursorPosition(point);

            Click();
        }

        public static void SendKeys(string key)
        {
            SendKeysClass.SendWait(key);
        }

        internal enum MouseEvents : uint
        {
            MOUSEEVENTF_LEFTDOWN = 2,
            MOUSEEVENTF_LEFTUP = 4,
        }
    }

    [DebuggerDisplay(@"\{{x}, {y}\}")]
    [StructLayout(LayoutKind.Sequential)]
    public struct POINT
    {
        public int x;
        public int y;
    }
}
