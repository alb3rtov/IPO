using System;
using System.Collections.Generic;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Protectora
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private string usuario = "admin";
        private string password = "ipo1";
        private Window management;

        public MainWindow()
        {
            InitializeComponent();
            txtUsuario.Focus();
        }

        private void checkCredentials() {
            if (txtUsuario.Text == usuario && txtPassword.Text == password)
            {
                management = new Management();
                this.Close();
                management.Show();

            }
            else if (txtUsuario.Text == "" || txtPassword.Text == "")
            {
                MessageBox.Show("Debe introducir alguna cadena en los campos de usuario y constraseña", "Credenciales incorrectas", MessageBoxButton.OK, MessageBoxImage.Error);
                txtUsuario.Text = "";
                txtPassword.Text = "";
            }
            else
            {
                MessageBox.Show("El usuario o la contraseña no es correcto", "Credenciales incorrectas", MessageBoxButton.OK, MessageBoxImage.Error);
                txtUsuario.Text = "";
                txtPassword.Text = "";
            }
        }


        private void Button_Click(object sender, RoutedEventArgs e)
        {
            checkCredentials();
        }

        private void Button_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Return) {
                checkCredentials();
            }
        }

        private void txtUsuario_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Return)
            {
                checkCredentials();
            }
        }

        private void txtPassword_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Return)
            {
                checkCredentials();
            }
        }
    }
}
