using System;
using System.Collections.Generic;
using System.Diagnostics;
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
    public partial class Plansza : Window
    {
        public Plansza()
        {
            InitializeComponent();
        }

        private void Zasady_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
        }

        private void Statystyki_Click(object sender, RoutedEventArgs e)
        {
            Statystyki statystyki = new Statystyki();
            statystyki.Show();
        }

        private void Github_Click(object sender, RoutedEventArgs e)
        {
            string url = "https://github.com/Damian0o0";

            try
            {
                Process.Start(url);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Wystąpił błąd: " + ex.Message);
            }
        }
        private void Ustawienia_Click(object sender, RoutedEventArgs e)
        {
            Ustawienia ustawienia = new Ustawienia();
            ustawienia.Show();
        }
    }
}
