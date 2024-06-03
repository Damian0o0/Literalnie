using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace Literalnie
{
    public partial class Plansza : Window
    {
        private string[] words = {
            "konto", "fotka", "cytat", "pokaż", "tylko", "marek", "temat",
            "kości", "głowa", "wyraz", "sklep", "tosty", "cudna", "cudny", "nudny", "agata", "kubek",
            "nożyk", "łóżko", "czapa", "drzwi", "zegar", "kreda", "oczko", "taśma", "wafel", "ramka",
            "karta", "kwiat", "baton", "trawa", "sufit", "lampa", "ogień", "balon", "okrąg", "żabka"
        };
        private string secretWord;
        private int currentRow = 0;

        public Plansza()
        {
            InitializeComponent();
            LoadSecretWord();

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

        private void LoadSecretWord()
        {
            var random = new Random();
            secretWord = words[random.Next(words.Length)].ToUpper();
        }
        private void SaveSecretWord()
        {
            try
            {
                string filePath = "odpowiedz.txt";
                File.WriteAllText(filePath, secretWord);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Wystąpił błąd podczas zapisywania odpowiedzi: " + ex.Message);
            }
        }

        private void IncrementGamesCount()
        {
            if (App.OpenWindows.ContainsKey("Statystyki"))
            {
                if (App.OpenWindows["Statystyki"] is Statystyki statystykiWindow)
                {
                    statystykiWindow.IncrementGamesCount();
                }
            }
        }

        private void TextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            TextBox textBox = sender as TextBox;
            if (!string.IsNullOrEmpty(e.Text) && char.IsLetter(e.Text[0]))
            {
                textBox.Text = e.Text.ToUpper();
                textBox.CaretIndex = 1;
                e.Handled = true;
            }
        }

        private void CheckGuess_Click(object sender, RoutedEventArgs e)
        {
            if (currentRow >= 6) return;

            TextBox[] textBoxes = new TextBox[5];
            for (int i = 0; i < 5; i++)
            {
                textBoxes[i] = (TextBox)GameGrid.Children.Cast<UIElement>()
                    .First(el => Grid.GetRow(el) == currentRow && Grid.GetColumn(el) == i);
            }

            string guess = string.Concat(textBoxes.Select(tb => tb.Text.ToUpper()));

            if (guess.Length < 5)
            {
                MessageBox.Show("Wprowadź 5 liter.");
                return;
            }

            if (guess == secretWord)
            {
                MessageBox.Show("Gratulacje! Zgadłeś słowo!");
                Close(); // Zamknij okno gry po wygranej
                return;
            }

            for (int i = 0; i < 5; i++)
            {
                char guessChar = guess[i];
                if (guessChar == secretWord[i])
                {
                    textBoxes[i].Background = new SolidColorBrush(Colors.Green);
                }
                else if (secretWord[i] == guess[i] || secretWord.Contains(guessChar))
                {
                    textBoxes[i].Background = new SolidColorBrush(Colors.Yellow);
                }
                else
                {
                    textBoxes[i].Background = new SolidColorBrush(Colors.Gray);
                }
            }

            currentRow++;
        }

    }
}

