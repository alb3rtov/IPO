using System;
using System.Collections.Generic;
using System.ComponentModel;
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
using System.Windows.Shapes;

namespace Protectora
{
    /// <summary>
    /// Lógica de interacción para VideoWindow.xaml
    /// </summary>
    public partial class VideoWindow : Window
    {
        private Window parent;
        private bool paused = false;

        public VideoWindow(Window window, Animal animal)
        {
            InitializeComponent();
            this.Title = "Video de " + animal.Name;
            parent = window;
            DataContext = animal;
            lblName.Content = "Video sobre " + animal.Name;
        }
        
        /* When close window enable parent window */
        private void Window_Closing(object sender, CancelEventArgs e)
        {
            meVideo.LoadedBehavior = MediaState.Manual;
            meVideo.UnloadedBehavior = MediaState.Manual;
            meVideo.Stop();
            parent.IsEnabled = true;
        }

        /* Change button funcionality if video is paused or is not */
        private void btbPlayStop_Click(object sender, RoutedEventArgs e)
        {
            if (paused)
            {
                meVideo.Play();
                imgPlayStop.Source = new BitmapImage(new Uri("pack://application:,,,/Images/pause.png"));
                paused = false;
            }
            else
            {
                meVideo.Pause();
                imgPlayStop.Source = new BitmapImage(new Uri("pack://application:,,,/Images/play.png"));
                paused = true;
            }
        }

        /* If video is loaded play automatically */
        private void meVideo_Loaded(object sender, RoutedEventArgs e)
        {
            meVideo.LoadedBehavior = MediaState.Manual;
            meVideo.UnloadedBehavior = MediaState.Manual;
            meVideo.Play();
        }

        /* Forwards video 5 seconds */
        private void btbForward_Click(object sender, RoutedEventArgs e)
        {
            meVideo.Position += TimeSpan.FromMilliseconds(5000);

            if (paused)
            {
                meVideo.Play();
                meVideo.Pause();
            }
        }

        /* Backwards video 5 seconds */
        private void btbBack_Click(object sender, RoutedEventArgs e)
        {
            meVideo.Position -= TimeSpan.FromMilliseconds(5000);

            if (paused)
            {
                meVideo.Play();
                meVideo.Pause();
            }
        }

        /* If video ended play again */
        private void meVideo_MediaEnded(object sender, RoutedEventArgs e)
        {
            meVideo.Stop();
            meVideo.Play();
        }
    }
}