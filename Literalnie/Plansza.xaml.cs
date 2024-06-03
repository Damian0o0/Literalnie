using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Literalnie
{
    public partial class Plansza : Window
    {
        private string correctWord;

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

            LoadRandomWord();
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

        private void GenerujHaslo_Click(object sender, RoutedEventArgs e)
        {
            LoadRandomWord();
        }

        private void LoadRandomWord()
        {
            try
            {
                string filePath = "hasla.txt";
                if (!File.Exists(filePath))
                {
                    throw new FileNotFoundException($"Plik '{filePath}' nie został znaleziony.");
                }

                string[] words = File.ReadAllLines(filePath);
                if (words.Length == 0)
                {
                    throw new InvalidOperationException("Plik 'hasla.txt' jest pusty.");
                }

                Random rand = new Random();
                correctWord = words[rand.Next(words.Length)].ToUpper();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Wystąpił błąd podczas ładowania słowa: {ex.Message}");
                correctWord = "ERROR"; // Ustawienie domyślnego słowa w przypadku błędu
            }
        }

        private void CheckGuess_Click(object sender, RoutedEventArgs e)
        {
            TextBox[] guessTextBoxes = new TextBox[]
            {
                GuessTextBox1, GuessTextBox2, GuessTextBox3, GuessTextBox4, GuessTextBox5
            };

            string userGuess = string.Join("", guessTextBoxes.Select(tb => tb.Text.ToUpper()));
            if (userGuess.Length != 5)
            {
                MessageBox.Show("Please enter a 5-letter word.");
                return;
            }

            for (int i = 0; i < 5; i++)
            {
                if (userGuess[i] == correctWord[i])
                {
                    guessTextBoxes[i].Background = Brushes.Green;
                }
                else if (correctWord.Contains(userGuess[i]))
                {
                    guessTextBoxes[i].Background = Brushes.Yellow;
                }
                else
                {
                    guessTextBoxes[i].Background = Brushes.Gray;
                }
            }
        }
    }
}
