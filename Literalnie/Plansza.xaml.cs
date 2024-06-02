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
            Ustawienia ustawienia = new UstawSettings();
            ustawienia.Show();
        }

        private void GenerujHaslo_Click(object sender, RoutedEventArgs e)
        {
            // Generuj nowe hasło
            string[] slowa = File.ReadAllLines("hasla.txt");
            string haslo = slowa[new Random().Next(slowa.Length)];

            // Ustaw wartości textboxów
            GuessTextBox1.Text = haslo[0].ToString();
            GuessTextBox2.Text = haslo[1].ToString();
            GuessTextBox3.Text = haslo[2].ToString();
            GuessTextBox4.Text = haslo[3].ToString();
            GuessTextBox5.Text = haslo[4].ToString();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            GenerujHaslo_Click(sender, e);
        }
    }
}