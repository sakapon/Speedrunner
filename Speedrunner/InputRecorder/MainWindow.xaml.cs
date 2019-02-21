using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Win32InputLib;

namespace InputRecorder
{
    /// <summary>
    /// MainWindow.xaml の相互作用ロジック
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            Closing += (o, e) =>
            {
                MouseHook.Stop();
                KeyboardHook.Stop();
            };

            RecordButton.Checked += RecordButton_Checked;
            RecordButton.Unchecked += RecordButton_Unchecked;

            actions.CollectionChanged += (o, e) =>
            {
                if (e.Action == NotifyCollectionChangedAction.Add)
                {
                    Debug.WriteLine(e.NewItems[0]);
                }
            };
        }

        void RecordButton_Checked(object sender, RoutedEventArgs e)
        {
            RecordButton.Content = "Recording...";
            MouseHook.AddEvent(MouseNotified);
            KeyboardHook.AddEvent(KeyboardNotified);
            MouseHook.Start();
            KeyboardHook.Start();
        }

        void RecordButton_Unchecked(object sender, RoutedEventArgs e)
        {
            RecordButton.Content = "Record";
            MouseHook.Stop();
            KeyboardHook.Stop();
        }

        ObservableCollection<string> actions = new ObservableCollection<string>();
        MouseHook.StateMouse mouseAction;

        void MouseNotified(ref MouseHook.StateMouse s)
        {
            switch (s.Stroke)
            {
                case MouseHook.Stroke.MOVE:
                    switch (mouseAction.Stroke)
                    {
                        case MouseHook.Stroke.LEFT_DOWN:
                        case MouseHook.Stroke.RIGHT_DOWN:
                            actions.Add($"{mouseAction.Stroke}: {mouseAction.X}, {mouseAction.Y}");
                            break;
                        default:
                            break;
                    }
                    break;
                case MouseHook.Stroke.LEFT_DOWN:
                    switch (mouseAction.Stroke)
                    {
                        case MouseHook.Stroke.RIGHT_DOWN:
                            actions.Add($"{mouseAction.Stroke}: {mouseAction.X}, {mouseAction.Y}");
                            break;
                        default:
                            break;
                    }
                    break;
                case MouseHook.Stroke.LEFT_UP:
                    switch (mouseAction.Stroke)
                    {
                        case MouseHook.Stroke.MOVE:
                        case MouseHook.Stroke.RIGHT_UP:
                            actions.Add($"{s.Stroke}: {s.X}, {s.Y}");
                            break;
                        case MouseHook.Stroke.LEFT_DOWN:
                            actions.Add($"Left_Click: {s.X}, {s.Y}");
                            break;
                        case MouseHook.Stroke.RIGHT_DOWN:
                            actions.Add($"{mouseAction.Stroke}: {mouseAction.X}, {mouseAction.Y}");
                            actions.Add($"{s.Stroke}: {s.X}, {s.Y}");
                            break;
                        default:
                            break;
                    }
                    break;
                case MouseHook.Stroke.RIGHT_DOWN:
                    switch (mouseAction.Stroke)
                    {
                        case MouseHook.Stroke.LEFT_DOWN:
                            actions.Add($"{mouseAction.Stroke}: {mouseAction.X}, {mouseAction.Y}");
                            break;
                        default:
                            break;
                    }
                    break;
                case MouseHook.Stroke.RIGHT_UP:
                    switch (mouseAction.Stroke)
                    {
                        case MouseHook.Stroke.MOVE:
                        case MouseHook.Stroke.LEFT_UP:
                            actions.Add($"{s.Stroke}: {s.X}, {s.Y}");
                            break;
                        case MouseHook.Stroke.RIGHT_DOWN:
                            actions.Add($"Right_Click: {s.X}, {s.Y}");
                            break;
                        case MouseHook.Stroke.LEFT_DOWN:
                            actions.Add($"{mouseAction.Stroke}: {mouseAction.X}, {mouseAction.Y}");
                            actions.Add($"{s.Stroke}: {s.X}, {s.Y}");
                            break;
                        default:
                            break;
                    }
                    break;
                default:
                    break;
            }

            if (MouseHook.Stroke.MOVE <= s.Stroke && s.Stroke <= MouseHook.Stroke.RIGHT_UP)
            {
                mouseAction = s;
            }
        }

        void KeyboardNotified(ref KeyboardHook.StateKeyboard s)
        {
        }
    }
}
