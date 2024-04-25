using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Literalnie
{
    public partial class Statystyki : Window
    {
        public Statystyki()
        {
            InitializeComponent();
        }

        private void ZamknijStatystyki_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

    }
}
