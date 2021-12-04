﻿using System;
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
            if (txtUser.Text == user && txtPassword.Password == password)
            {
                management = new Management(user);
                this.Close();
                management.Show();

            }
            else if (txtUser.Text == "" || txtPassword.Password == "")
            {
                this.IsEnabled = false;
                MessageBox.Show("Debe introducir alguna cadena en los campos de usuario y constraseña", "Credenciales incorrectas", MessageBoxButton.OK, MessageBoxImage.Error);
                this.IsEnabled = true;
                txtUser.Text = "";
                txtPassword.Password = "";
                lblPassword.Visibility = Visibility.Visible;
            }
            else
            {
                this.IsEnabled = false;
                MessageBox.Show("El usuario o la contraseña no es correcto", "Credenciales incorrectas", MessageBoxButton.OK, MessageBoxImage.Error);
                this.IsEnabled = true;
                txtUser.Text = "";
                txtPassword.Password = "";
                lblPassword.Visibility = Visibility.Visible;
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            checkCredentials();
        }

        private void WrapPanel_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            lblPassword.Visibility = Visibility.Hidden;
            txtPassword.Focus();
        }

        private void txtPassword_GotFocus(object sender, RoutedEventArgs e)
        {
            lblPassword.Visibility = Visibility.Hidden;
        }

        private void txtPassword_LostFocus(object sender, RoutedEventArgs e)
        {
            if (txtPassword.Password.Length == 0)
            {
                lblPassword.Visibility = Visibility.Visible;
            }
        }

        private void txtUser_KeyUp(object sender, KeyEventArgs e)
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
