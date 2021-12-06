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
        public VideoWindow(Window window, Animal animal)
        {
            InitializeComponent();
            parent = window;
            DataContext = animal;
            lblName.Content = "Video sobre " + animal.Name;
        }

        private void Window_Closing(object sender, CancelEventArgs e)
        {
            meVideo.LoadedBehavior = MediaState.Manual;
            meVideo.UnloadedBehavior = MediaState.Manual;
            meVideo.Stop();
            parent.IsEnabled = true;
        }

        private void btbPlay_Click(object sender, RoutedEventArgs e)
        {
            meVideo.LoadedBehavior = MediaState.Manual;
            meVideo.UnloadedBehavior = MediaState.Manual;
            meVideo.Play();
        }

        private void btbStop_Click(object sender, RoutedEventArgs e)
        {
            meVideo.LoadedBehavior = MediaState.Manual;
            meVideo.UnloadedBehavior = MediaState.Manual;
            meVideo.Pause();
        }
    }
}