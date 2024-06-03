using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace Literalnie
{
    public partial class Statystyki : Window
    {
        private DispatcherTimer timer;

        private int gamesCount = 0;
        private int totalWins = 0;
        private int consecutiveWins = 0;
        public Statystyki()
        {
            InitializeComponent();

            timer = new DispatcherTimer();
            timer.Tick += Timer_Tick;

            timer.Start();

            if (App.OpenWindows.ContainsKey(GetType().Name))
            {
                App.OpenWindows[GetType().Name].Activate();
                Close();
                return;
            }

            App.OpenWindows.Add(GetType().Name, this);
        }
        public void UpdateStats(int gamesCount, int totalWins, int consecutiveWins)
        {
            this.gamesCount = gamesCount;
            this.totalWins = totalWins;
            this.consecutiveWins = consecutiveWins;

            ZagraneGryTextBlock.Text = gamesCount.ToString();
            WygraneGryTextBlock.Text = totalWins.ToString();
            WygraneZRzeduTextBlock.Text = consecutiveWins.ToString();
        }
        public void IncrementGamesCount()
        {
            gamesCount++;
            ZagraneGryTextBlock.Text = gamesCount.ToString();
        }


        protected override void OnClosing(CancelEventArgs e)
        {
            App.OpenWindows.Remove(GetType().Name);
            base.OnClosing(e);
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            DateTime now = DateTime.Now;
            DateTime tomorrow = now.AddDays(1).Date;
            TimeSpan timeLeft = tomorrow - now;

            Czas.Text = timeLeft.ToString(@"hh\:mm\:ss");
        }

        private void ZamknijStatystyki_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

    }
}
