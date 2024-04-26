using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace Literalnie
{
    public partial class App : Application
    {
        public static Dictionary<string, Window> OpenWindows { get; } = new Dictionary<string, Window>();
    }
}
