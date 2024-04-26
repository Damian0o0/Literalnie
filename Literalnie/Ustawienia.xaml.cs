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
    public partial class Ustawienia : Window
    {
        public Ustawienia()
        {
            InitializeComponent();
        }

        private void Zamknij_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
