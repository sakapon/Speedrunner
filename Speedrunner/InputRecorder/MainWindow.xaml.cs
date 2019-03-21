using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Diagnostics;
using System.IO;
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
using System.Xaml;
using Speedrunner.Activities;
using Win32InputLib;
using Keys = System.Windows.Forms.Keys;

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

            Directory.CreateDirectory(OutputDirName);

            RecordButton.Checked += RecordButton_Checked;
            RecordButton.Unchecked += RecordButton_Unchecked;
            ReplayButton.Click += ReplayButton_Click;
            Closing += (o, e) => RecordButton_Unchecked(o, null);

            actions.CollectionChanged += (o, e) =>
            {
                if (e.Action == NotifyCollectionChangedAction.Add)
                {
                    Debug.WriteLine(e.NewItems[0]);
                }
            };
        }

        void ReplayButton_Click(object sender, RoutedEventArgs e)
        {
            ReplayButton.IsEnabled = false;

            Task.Run(() =>
            {
                workflow.Execute(new WorkflowContext());

                Dispatcher.InvokeAsync(() => ReplayButton.IsEnabled = true);
            });
        }

        void RecordButton_Checked(object sender, RoutedEventArgs e)
        {
            RecordButton.Content = "Recording...";
            workflow.Activities.Clear();
            actions.Clear();
            MouseHook.AddEvent(input.NotifyMouse);
            KeyboardHook.AddEvent(KeyboardNotified);
            MouseHook.Start();
            KeyboardHook.Start();
        }

        void RecordButton_Unchecked(object sender, RoutedEventArgs e)
        {
            RecordButton.Content = "Record";
            MouseHook.Stop();
            KeyboardHook.Stop();

            workflow = input.GetWorkflow();

            if (workflow.Activities.Any())
            {
                XamlServices.Save($@"{OutputDirName}\{DateTime.Now:yyyyMMdd-HHmmss}.xaml", workflow);
                File.WriteAllLines($@"{OutputDirName}\{DateTime.Now:yyyyMMdd-HHmmss}.txt", actions, Encoding.UTF8);
            }
        }

        const string OutputDirName = "Workflows";

        // TODO: 現在の実装ではマウスとキーボードが独立しています。
        InputManager input = new InputManager();
        SequentialWorkflow workflow = new SequentialWorkflow { Title = "By Input Recorder" };
        ObservableCollection<string> actions = new ObservableCollection<string>();
        KeyboardHook.StateKeyboard keyAction;

        void KeyboardNotified(ref KeyboardHook.StateKeyboard s)
        {
            switch (s.Stroke)
            {
                case KeyboardHook.Stroke.KEY_DOWN:
                case KeyboardHook.Stroke.SYSKEY_DOWN:
                    if (Keys.D0 <= s.Key && s.Key <= Keys.Z)
                    {
                        workflow.Activities.Add(new KeyStroke { Timeout = 500, Key = s.Key });
                        actions.Add($"Type: {s.Key}");
                    }
                    else
                    {
                        actions.Add($"{s.Stroke}: {s.Key}");
                    }
                    break;
                case KeyboardHook.Stroke.KEY_UP:
                case KeyboardHook.Stroke.SYSKEY_UP:
                    if (IsModifier(s.Key))
                    {
                        actions.Add($"{s.Stroke}: {s.Key}");
                    }
                    break;
                default:
                    break;
            }

            keyAction = s;
        }

        static bool IsModifier(Keys key)
        {
            switch (key)
            {
                case Keys.ShiftKey:
                case Keys.ControlKey:
                case Keys.Menu:
                case Keys.LWin:
                case Keys.RWin:
                case Keys.LShiftKey:
                case Keys.RShiftKey:
                case Keys.LControlKey:
                case Keys.RControlKey:
                case Keys.LMenu:
                case Keys.RMenu:
                case Keys.Shift:
                case Keys.Control:
                case Keys.Alt:
                    return true;
                default:
                    return false;
            }
        }

        static string ToLetter(Keys key)
        {
            if (Keys.D0 <= key && key <= Keys.D9)
                return key.ToString().TrimStart('D');
            else if (Keys.A <= key && key <= Keys.Z)
                return key.ToString();

            throw new InvalidOperationException();
        }
    }
}
