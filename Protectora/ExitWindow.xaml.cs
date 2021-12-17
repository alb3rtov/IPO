using System;
using System.Collections.Generic;
using System.IO;
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
    /// Lógica de interacción para ExitWindow.xaml
    /// </summary>
    public partial class ExitWindow : Window
    {
        public ExitWindow()
        {
            InitializeComponent();
        }

        /* Event for shutdown the application */
        private void btbYes_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        /* Close this window */
        private void btbNo_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        /* Event for enable drag move window */
        private void Window_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }
    }
}
