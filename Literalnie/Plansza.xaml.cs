using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace Literalnie
{
    public partial class Plansza : Window
    {
        private string correctWord;

        public Plansza()
        {
            InitializeComponent();

            LoadRandomWord();

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

        private void GenerujHaslo_Click(object sender, RoutedEventArgs e)
        {
            LoadRandomWord();
        }

        private void LoadRandomWord()
        {
            string[] words;
            try
            {
                words = File.ReadAllLines("hasla.txt");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error reading the file: " + ex.Message);
                return;
            }

            if (words.Length == 0)
            {
                MessageBox.Show("No words loaded from the file.");
                return;
            }

            Random rand = new Random();
            correctWord = words[rand.Next(words.Length)].ToUpper();
            Console.WriteLine("Randomly selected word: " + correctWord); 

            using (StreamWriter writer = new StreamWriter("odpowiedz.txt", true))
            {
                writer.WriteLine(correctWord);
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
            if (correctWord == null)
            {
                MessageBox.Show("Please generate a word first.");
                return;
            }

            TextBox[] guessTextBoxes = new TextBox[]
            {
        GuessTextBox1, GuessTextBox2, GuessTextBox3, GuessTextBox4, GuessTextBox5,
        GuessTextBox6, GuessTextBox7, GuessTextBox8, GuessTextBox9, GuessTextBox10,
        GuessTextBox11, GuessTextBox12, GuessTextBox13, GuessTextBox14, GuessTextBox15,
        GuessTextBox16, GuessTextBox17, GuessTextBox18, GuessTextBox19, GuessTextBox20,
        GuessTextBox21, GuessTextBox22, GuessTextBox23, GuessTextBox24, GuessTextBox25,
        GuessTextBox26, GuessTextBox27, GuessTextBox28, GuessTextBox29, GuessTextBox30
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