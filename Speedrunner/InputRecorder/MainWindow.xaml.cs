using System;
using System.Collections.Generic;
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

namespace InputRecorder
{
    /// <summary>
    /// MainWindow.xaml の相互作用ロジック
    /// </summary>
    public partial class MainWindow : Window
    {
        const string OutputDirName = "Workflows";

        // TODO: 現在の実装ではマウスとキーボードが独立しています。
        InputManager input = new InputManager();
        SequentialWorkflow workflow = new SequentialWorkflow { Title = "By Input Recorder" };

        public MainWindow()
        {
            InitializeComponent();

            Directory.CreateDirectory(OutputDirName);

            RecordButton.Checked += RecordButton_Checked;
            RecordButton.Unchecked += RecordButton_Unchecked;
            ReplayButton.Click += ReplayButton_Click;
            Closing += (o, e) => RecordButton_Unchecked(o, null);
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
            MouseHook.AddEvent(input.NotifyMouse);
            KeyboardHook.AddEvent(input.NotifyKeyboard);
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
            }
        }
    }
}
