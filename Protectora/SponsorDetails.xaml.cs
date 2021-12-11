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
    /// Lógica de interacción para SponsorDetails.xaml
    /// </summary>
    public partial class SponsorDetails : Window
    {
        public Window parent;
        public SponsorDetails(Window window, Sponsor sponsor)
        {
            InitializeComponent();
            parent = window;
            DataContext = sponsor;

            txtName.Text = " " + sponsor.Firstname + " " + sponsor.Lastname + " ";
        }

        private void Window_Closing(object sender, CancelEventArgs e)
        {
            parent.IsEnabled = true;
        }
    }
}
