using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
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

            if (App.OpenWindows.ContainsKey(GetType().Name) && App.OpenWindows["Zasady"] == null)
            {
                App.OpenWindows[GetType().Name].Activate();
                Close();
                return;
            }

            App.OpenWindows.Add(GetType().Name, this);

            if (App.OpenWindows.ContainsKey("MainWindow"))
            {
                App.OpenWindows["MainWindow"].Activate();
            }
            else
            {
                MainWindow mainWindow = new MainWindow();
                mainWindow.Show();
            }
        }

        protected override void OnClosing(CancelEventArgs e)
        {
            App.OpenWindows.Remove(GetType().Name);
            base.OnClosing(e);
        }

        private void Zasady_Click(object sender, RoutedEventArgs e)
        {
            if (App.OpenWindows.ContainsKey("MainWindow"))
            {
                App.OpenWindows["MainWindow"].Activate();
            }
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
                ProcessStartInfo startInfo = new ProcessStartInfo(url);
                Process.Start(startInfo);
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
