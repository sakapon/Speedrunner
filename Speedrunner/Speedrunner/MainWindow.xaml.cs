using System;
using System.Collections.Generic;
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
using Speedrunner.Activities;

namespace Speedrunner
{
    /// <summary>
    /// MainWindow.xaml の相互作用ロジック
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        public static void NextP(WorkflowContext context)
        {
            var varP = context.Variables.Get<double>("p");
            varP.Value *= -3;
        }

        public static void AddTerm(WorkflowContext context)
        {
            var varSum = context.Variables.Get<double>("sum");
            var p = context.Variables.Get<double>("p").Value;
            var i = context.Variables.Get<int>("i").Value;
            varSum.Value += 1 / (i * p);
        }

        public static void Sqrt12(WorkflowContext context)
        {
            var varSum = context.Variables.Get<double>("sum");
            varSum.Value *= Math.Sqrt(12);
        }
    }
}
