using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Windows;

namespace TasSample.Models
{
    //[ContentProperty("Point")]
    [DebuggerDisplay(@"\{{Name}\}")]
    [Serializable]
    public struct Location
    {
        [DefaultValue("")]
        public string Name { get; set; }

        [DefaultValue("0,0")]
        public Point Point { get; set; }
    }
}
