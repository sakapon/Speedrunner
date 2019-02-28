using System;
using System.Collections.Generic;
using System.ComponentModel;
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

namespace Speedrunner.UILab
{
    /// <summary>
    /// このカスタム コントロールを XAML ファイルで使用するには、手順 1a または 1b の後、手順 2 に従います。
    ///
    /// 手順 1a) 現在のプロジェクトに存在する XAML ファイルでこのカスタム コントロールを使用する場合
    /// この XmlNamespace 属性を使用場所であるマークアップ ファイルのルート要素に
    /// 追加します:
    ///
    ///     xmlns:MyNamespace="clr-namespace:Speedrunner.UILab"
    ///
    ///
    /// 手順 1b) 異なるプロジェクトに存在する XAML ファイルでこのカスタム コントロールを使用する場合
    /// この XmlNamespace 属性を使用場所であるマークアップ ファイルのルート要素に
    /// 追加します:
    ///
    ///     xmlns:MyNamespace="clr-namespace:Speedrunner.UILab;assembly=Speedrunner.UILab"
    ///
    /// また、XAML ファイルのあるプロジェクトからこのプロジェクトへのプロジェクト参照を追加し、
    /// リビルドして、コンパイル エラーを防ぐ必要があります:
    ///
    ///     ソリューション エクスプローラーで対象のプロジェクトを右クリックし、
    ///     [参照の追加] の [プロジェクト] を選択してから、このプロジェクトを参照し、選択します。
    ///
    ///
    /// 手順 2)
    /// コントロールを XAML ファイルで使用します。
    ///
    ///     <MyNamespace:ForRange/>
    ///
    /// </summary>
    public class ForRange : ItemsControl
    {
        public static readonly DependencyProperty VariableNameProperty =
            DependencyProperty.Register("VariableName", typeof(string), typeof(ForRange), new PropertyMetadata("i"));

        public static readonly DependencyProperty StartProperty =
            DependencyProperty.Register("Start", typeof(int), typeof(ForRange), new PropertyMetadata(0));

        [Category("Activity")]
        public string VariableName
        {
            get { return (string)GetValue(VariableNameProperty); }
            set { SetValue(VariableNameProperty, value); }
        }

        [Category("Activity")]
        public int Start
        {
            get { return (int)GetValue(StartProperty); }
            set { SetValue(StartProperty, value); }
        }

        static ForRange()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(ForRange), new FrameworkPropertyMetadata(typeof(ForRange)));
        }
    }
}
