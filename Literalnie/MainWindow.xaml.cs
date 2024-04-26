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

namespace Literalnie
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }
        private void X_Click(object sender, RoutedEventArgs e)
        {
            if (App.OpenWindows.ContainsKey("Plansza"))
            {
                this.Hide();
                App.OpenWindows["Plansza"].Activate();
            }
            else
            {
                Plansza plansza = new Plansza();
                plansza.Show();

                this.Hide();

                if (!App.OpenWindows.ContainsKey("Plansza"))
                {
                    App.OpenWindows.Add("Plansza", plansza);
                }
            }
        }
    }
}