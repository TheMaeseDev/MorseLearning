using System;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Threading;

namespace MorseApp
{
    public partial class MainWindow : Window
    {
        private DateTime keyPressStart;
        private const int threshold = 100; // Milisegundos para distinguir punto de raya

        public MainWindow()
        {
            InitializeComponent();
        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Space)
            {
                keyPressStart = DateTime.Now;
                entradaNodo.Fill = Brushes.Yellow; // Enciende la flecha de entrada
            }
        }

        private void Window_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Space)
            {
                TimeSpan duration = DateTime.Now - keyPressStart;
                bool esPunto = duration.TotalMilliseconds < threshold;

                entradaNodo.Fill = Brushes.Gray; // Apagar la flecha de entrada
                if (esPunto)
                {
                    puntoNodo.Fill = Brushes.Yellow;
                }
                else
                {
                    rayaNodo.Fill = Brushes.Yellow;
                }

                // Apagar después de un segundo
                DispatcherTimer timer = new DispatcherTimer();
                timer.Interval = TimeSpan.FromSeconds(0.5);
                timer.Tick += (s, ev) =>
                {
                    puntoNodo.Fill = Brushes.Gray;
                    rayaNodo.Fill = Brushes.Gray;
                    timer.Stop();
                };
                timer.Start();
            }
        }
    }
}