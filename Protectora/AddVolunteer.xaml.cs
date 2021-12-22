using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Net.Mail;
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
    /// Lógica de interacción para AddVolunteer.xaml
    /// </summary>
    public partial class AddVolunteer : Window
    {
        private Window parent;
        private ObservableCollection<Volunteer> volunteers;
        private bool checkFirstName = false;
        private bool checkLastName = false;
        private bool checkDni = false;
        private bool checkPhoneNumber = false;
        private bool checkEmail = false;
        private bool checkAge = false;
        private bool checkZone = false;
        private bool checkImages = false;
        private string filename;

        public AddVolunteer(Window window, ObservableCollection<Volunteer> volunteerList)
        {
            InitializeComponent();
            parent = window;
            volunteers = volunteerList;
            DataContext = volunteers;
        }

        /* When close window enable parent window */
        private void Window_Closing(object sender, CancelEventArgs e)
        {
            parent.IsEnabled = true;
        }

        /* Add volunteer checking that all fields are valid */
        private void btbAdd_Click(object sender, RoutedEventArgs e)
        {
            if (checkFirstName && checkLastName && checkDni && checkPhoneNumber && checkEmail && checkAge && checkZone && checkImages)
            {
                string timeDisp = cbInitial.Text + "-" + cbEnd.Text;

                Volunteer volunteer = new Volunteer(txtFirstName.Text, txtLastName.Text, int.Parse(txtDni.Text), txtEmail.Text, int.Parse(txtPhoneNumber.Text), new Uri(filename), timeDisp, txtZone.Text, cbStudies.Text, int.Parse(txtAge.Text));
                volunteers.Add(volunteer);
                this.Close();
            }
            else
            {
                MessageBox.Show("Debe de introducir datos correctos en el formulario", "Error en el formulario", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        /* Add image and show in the correct button image */
        private void addImages1_Click(object sender, RoutedEventArgs e)
        {
            var openDialog = new OpenFileDialog();
            openDialog.Filter = "Images|*.jpg;*.gif;*.bmp;*.png";

            if (openDialog.ShowDialog() == true)
            {
                try
                {
                    filename = openDialog.FileName;
                    var brush = new ImageBrush();
                    brush.ImageSource = new BitmapImage(new Uri(openDialog.FileName, UriKind.RelativeOrAbsolute));
                    checkImages = true;
                    addImages1.Background = brush;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al cargar la imagen " + ex.Message);
                }
            }
        }

        /* Constraints for all fields */
        private void txtFirstName_LostFocus(object sender, RoutedEventArgs e)
        {
            if (txtFirstName.Text.Length <= 3 || txtFirstName.Text.Length >= 15)
            {
                txtFirstName.BorderThickness = new Thickness(2);
                txtFirstName.BorderBrush = Brushes.Red;
                //txtFirstName.Background = Brushes.LightCoral;
                imgFirstNameError.Visibility = Visibility.Visible;
                checkFirstName = false;
            }
            else
            {
                txtFirstName.BorderThickness = new Thickness(1);
                txtFirstName.BorderBrush = (SolidColorBrush)new BrushConverter().ConvertFrom("#FFABADB3");
                txtFirstName.Background = Brushes.White;
                imgFirstNameError.Visibility = Visibility.Hidden;
                checkFirstName = true;
            }
        }

        private void txtFirstName_KeyUp(object sender, KeyEventArgs e)
        {
            if (txtFirstName.Text.Length > 3 && txtFirstName.Text.Length < 15)
            {
                txtFirstName.BorderThickness = new Thickness(1);
                txtFirstName.BorderBrush = (SolidColorBrush)new BrushConverter().ConvertFrom("#FFABADB3");
                txtFirstName.Background = Brushes.White;
                imgFirstNameError.Visibility = Visibility.Hidden;
                checkFirstName = true;
            }
        }

        private void txtLastName_LostFocus(object sender, RoutedEventArgs e)
        {
            if (txtLastName.Text.Length <= 3 || txtLastName.Text.Length >= 15)
            {
                txtLastName.BorderThickness = new Thickness(2);
                txtLastName.BorderBrush = Brushes.Red;
                //txtLastName.Background = Brushes.LightCoral;
                imgLastNameError.Visibility = Visibility.Visible;
                checkLastName = false;
            }
            else
            {
                txtLastName.BorderThickness = new Thickness(1);
                txtLastName.BorderBrush = (SolidColorBrush)new BrushConverter().ConvertFrom("#FFABADB3");
                txtLastName.Background = Brushes.White;
                imgLastNameError.Visibility = Visibility.Hidden;
                checkLastName = true;
            }
        }

        private void txtLastName_KeyUp(object sender, KeyEventArgs e)
        {
            if (txtLastName.Text.Length > 3 && txtLastName.Text.Length < 15)
            {
                txtLastName.BorderThickness = new Thickness(1);
                txtLastName.BorderBrush = (SolidColorBrush)new BrushConverter().ConvertFrom("#FFABADB3");
                txtLastName.Background = Brushes.White;
                imgLastNameError.Visibility = Visibility.Hidden;
                checkLastName = true;
            }
        }

        private void txtDni_LostFocus(object sender, RoutedEventArgs e)
        {
            int value;
            bool isNumber = int.TryParse(txtDni.Text, out value);

            if (isNumber)
            {
                if (txtDni.Text.Length != 8)
                {
                    txtDni.BorderThickness = new Thickness(2);
                    txtDni.BorderBrush = Brushes.Red;
                    //txtDni.Background = Brushes.LightCoral;
                    imgDniError.Visibility = Visibility.Visible;
                    checkDni = false;
                }
                else
                {
                    txtDni.BorderThickness = new Thickness(1);
                    txtDni.BorderBrush = (SolidColorBrush)new BrushConverter().ConvertFrom("#FFABADB3");
                    txtDni.Background = Brushes.White;
                    imgDniError.Visibility = Visibility.Hidden;
                    checkDni = true;
                }
            }
            else {
                txtDni.BorderThickness = new Thickness(2);
                txtDni.BorderBrush = Brushes.Red;
                //txtDni.Background = Brushes.LightCoral;
                imgDniError.Visibility = Visibility.Visible;
                checkDni = false;
            }
        }

        private void txtDni_KeyUp(object sender, KeyEventArgs e)
        {
            int value;
            bool isNumber = int.TryParse(txtDni.Text, out value);

            if (isNumber)
            {
                if (txtDni.Text.Length == 8)
                {
                    txtDni.BorderThickness = new Thickness(1);
                    txtDni.BorderBrush = (SolidColorBrush)new BrushConverter().ConvertFrom("#FFABADB3");
                    txtDni.Background = Brushes.White;
                    imgDniError.Visibility = Visibility.Hidden;
                    checkDni = true;
                }
            }
        }

        private void txtPhoneNumber_LostFocus(object sender, RoutedEventArgs e)
        {
            int value;
            bool isNumber = int.TryParse(txtPhoneNumber.Text, out value);

            if (isNumber)
            {
                if (txtPhoneNumber.Text.Length != 9)
                {
                    txtPhoneNumber.BorderThickness = new Thickness(2);
                    txtPhoneNumber.BorderBrush = Brushes.Red;
                    //txtPhoneNumber.Background = Brushes.LightCoral;
                    imgPhoneNumberError.Visibility = Visibility.Visible;
                    checkPhoneNumber = false;
                }
                else
                {
                    txtPhoneNumber.BorderThickness = new Thickness(1);
                    txtPhoneNumber.BorderBrush = (SolidColorBrush)new BrushConverter().ConvertFrom("#FFABADB3");
                    txtPhoneNumber.Background = Brushes.White;
                    imgPhoneNumberError.Visibility = Visibility.Hidden;
                    checkPhoneNumber = true;
                }
            }
            else
            {
                txtPhoneNumber.BorderThickness = new Thickness(2);
                txtPhoneNumber.BorderBrush = Brushes.Red;
                //txtPhoneNumber.Background = Brushes.LightCoral;
                imgPhoneNumberError.Visibility = Visibility.Visible;
                checkPhoneNumber = false;
            }
        }

        private void txtPhoneNumber_KeyUp(object sender, KeyEventArgs e)
        {
            int value;
            bool isNumber = int.TryParse(txtPhoneNumber.Text, out value);

            if (isNumber)
            {
                if (txtPhoneNumber.Text.Length == 9)
                {
                    txtPhoneNumber.BorderThickness = new Thickness(1);
                    txtPhoneNumber.BorderBrush = (SolidColorBrush)new BrushConverter().ConvertFrom("#FFABADB3");
                    txtPhoneNumber.Background = Brushes.White;
                    imgPhoneNumberError.Visibility = Visibility.Hidden;
                    checkPhoneNumber = true;
                }
            }
        }

        private void txtEmail_LostFocus(object sender, RoutedEventArgs e)
        {
            try
            {
                MailAddress m = new MailAddress(txtEmail.Text);
                txtEmail.BorderThickness = new Thickness(1);
                txtEmail.BorderBrush = (SolidColorBrush)new BrushConverter().ConvertFrom("#FFABADB3");
                txtEmail.Background = Brushes.White;
                imgEmailError.Visibility = Visibility.Hidden;
                checkEmail = true;
            }
            catch (Exception)
            {
                txtEmail.BorderThickness = new Thickness(2);
                txtEmail.BorderBrush = Brushes.Red;
                //txtEmail.Background = Brushes.LightCoral;
                imgEmailError.Visibility = Visibility.Visible;
                checkEmail = false;
            }
        }

        private void txtEmail_KeyUp(object sender, KeyEventArgs e)
        {
            try
            {
                MailAddress m = new MailAddress(txtEmail.Text);
                txtEmail.BorderThickness = new Thickness(1);
                txtEmail.BorderBrush = (SolidColorBrush)new BrushConverter().ConvertFrom("#FFABADB3");
                txtEmail.Background = Brushes.White;
                imgEmailError.Visibility = Visibility.Hidden;
                checkEmail = true;
            }
            catch (Exception) { };
        }

        private void txtAge_LostFocus(object sender, RoutedEventArgs e)
        {
            int value;
            bool isNumber = int.TryParse(txtAge.Text, out value);

            if (isNumber)
            {
                if (value < 18 || value > 110)
                {
                    txtAge.BorderThickness = new Thickness(2);
                    txtAge.BorderBrush = Brushes.Red;
                    //txtAge.Background = Brushes.LightCoral;
                    imgAgeError.Visibility = Visibility.Visible;
                    checkAge = false;
                }
                else
                {
                    txtAge.BorderThickness = new Thickness(1);
                    txtAge.BorderBrush = (SolidColorBrush)new BrushConverter().ConvertFrom("#FFABADB3");
                    txtAge.Background = Brushes.White;
                    imgAgeError.Visibility = Visibility.Hidden;
                    checkAge = true;
                }
            }
            else
            {
                txtAge.BorderThickness = new Thickness(2);
                txtAge.BorderBrush = Brushes.Red;
                //txtAge.Background = Brushes.LightCoral;
                imgAgeError.Visibility = Visibility.Visible;
                checkAge = false;
            }
        }

        private void txtAge_KeyUp(object sender, KeyEventArgs e)
        {
            int value;
            bool isNumber = int.TryParse(txtAge.Text, out value);

            if (isNumber)
            {
                if (value >= 18 || value <= 110)
                {
                    txtAge.BorderThickness = new Thickness(1);
                    txtAge.BorderBrush = (SolidColorBrush)new BrushConverter().ConvertFrom("#FFABADB3");
                    txtAge.Background = Brushes.White;
                    imgAgeError.Visibility = Visibility.Hidden;
                    checkAge = true;
                }
            }
        }

        private void txtZone_LostFocus(object sender, RoutedEventArgs e)
        {
            if (txtZone.Text.Length <= 5 || txtZone.Text.Length >= 20)
            {
                txtZone.BorderThickness = new Thickness(2);
                txtZone.BorderBrush = Brushes.Red;
                //txtZone.Background = Brushes.LightCoral;
                imgZoneError.Visibility = Visibility.Visible;
                checkZone = false;
            }
            else
            {
                txtZone.BorderThickness = new Thickness(1);
                txtZone.BorderBrush = (SolidColorBrush)new BrushConverter().ConvertFrom("#FFABADB3");
                txtZone.Background = Brushes.White;
                imgZoneError.Visibility = Visibility.Hidden;
                checkZone = true;
            }
        }

        private void txtZone_KeyUp(object sender, KeyEventArgs e)
        {
            if (txtZone.Text.Length >= 5 && txtZone.Text.Length <= 20)
            {
                txtZone.BorderThickness = new Thickness(1);
                txtZone.BorderBrush = (SolidColorBrush)new BrushConverter().ConvertFrom("#FFABADB3");
                txtZone.Background = Brushes.White;
                imgZoneError.Visibility = Visibility.Hidden;
                checkZone = true;
            }
        }
    }
}
