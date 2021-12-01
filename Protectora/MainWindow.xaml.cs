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
        private string user = "admin";
        private string password = "password";
        private Window management;

        public MainWindow()
        {
            InitializeComponent();
            txtUser.Focus();
        }

        private void checkCredentials() {
            if (txtUser.Text == user && txtPassword.Text == password)
            {
                management = new Management(user);
                this.Close();
                management.Show();

            }
            else if (txtUser.Text == "" || txtPassword.Text == "")
            {
                MessageBox.Show("Debe introducir alguna cadena en los campos de usuario y constraseña", "Credenciales incorrectas", MessageBoxButton.OK, MessageBoxImage.Error);   
                txtUser.Text = "";
                txtPassword.Text = "";
            }
            else
            {
                MessageBox.Show("El usuario o la contraseña no es correcto", "Credenciales incorrectas", MessageBoxButton.OK, MessageBoxImage.Error);
                txtUser.Text = "";
                txtPassword.Text = "";
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            checkCredentials();
        }
    }
}
