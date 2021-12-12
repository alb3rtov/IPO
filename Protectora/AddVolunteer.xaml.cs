using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    /// Lógica de interacción para AddVolunteer.xaml
    /// </summary>
    public partial class AddVolunteer : Window
    {
        private Window parent;
        private ObservableCollection<Volunteer> volunteers;
        private bool checkName = false;
        private bool checkBreed = false;
        private bool checkSize = false;
        private bool checkWeight = false;
        private bool checkAge = false;
        private bool checkChip = false;
        private bool checkImages = false;

        public AddVolunteer(Window window, ObservableCollection<Volunteer> volunteerList)
        {
            InitializeComponent();
            parent = window;
            volunteers = volunteerList;
            DataContext = volunteers;
            

        }

        private void Window_Closing(object sender, CancelEventArgs e)
        {
            parent.IsEnabled = true;
        }

        private void btbAdd_Click(object sender, RoutedEventArgs e)
        {
            if (checkName && checkBreed && checkSize && checkWeight && checkAge && checkChip && checkImages)
            {   
                /*
                string ppp = getRadioButton(rdPPPSi);
                string sterelized = getRadioButton(rdSterelizedSi);
                string dogs = getRadioButton(rdDogsSi);
                string childen = getRadioButton(rdChildrenSi);

                Animal animal = new Animal(txtName.Text, cbSex.Text, txtBreed.Text, int.Parse(txtSize.Text), int.Parse(txtWeight.Text), int.Parse(txtAge.Text), int.Parse(txtChip.Text), filenames, null, null, sterelized, childen, dogs, ppp);
                animals.Add(animal);
                this.Close();*/
            }
            else
            {
                MessageBox.Show("Debe de introducir datos correctos en el formulario", "Error en el formulario", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }


        private void txtName_LostFocus(object sender, RoutedEventArgs e)
        {
            if (txtName.Text.Length <= 2 || txtName.Text.Length >= 12)
            {
                txtName.BorderThickness = new Thickness(2);
                txtName.BorderBrush = Brushes.Red;
                txtName.Background = Brushes.LightCoral;
                imgNameError.Visibility = Visibility.Visible;
                checkName = false;
            }
            else
            {
                txtName.BorderThickness = new Thickness(1);
                txtName.BorderBrush = (SolidColorBrush)new BrushConverter().ConvertFrom("#FFABADB3");
                txtName.Background = Brushes.White;
                imgNameError.Visibility = Visibility.Hidden;
                checkName = true;
            }
        }

        private void txtName_KeyUp(object sender, KeyEventArgs e)
        {
            if (txtName.Text.Length > 2 && txtName.Text.Length < 12)
            {
                txtName.BorderThickness = new Thickness(1);
                txtName.BorderBrush = (SolidColorBrush)new BrushConverter().ConvertFrom("#FFABADB3");
                txtName.Background = Brushes.White;
                imgNameError.Visibility = Visibility.Hidden;
                checkName = true;
            }
        }

        private void txtBreed_LostFocus(object sender, RoutedEventArgs e)
        {
            if (txtDni.Text.Length <= 3 || txtDni.Text.Length >= 20)
            {
                txtDni.BorderThickness = new Thickness(2);
                txtDni.BorderBrush = Brushes.Red;
                txtDni.Background = Brushes.LightCoral;
                imgDniError.Visibility = Visibility.Visible;
                checkBreed = false;
            }
            else
            {
                txtDni.BorderThickness = new Thickness(1);
                txtDni.BorderBrush = (SolidColorBrush)new BrushConverter().ConvertFrom("#FFABADB3");
                txtDni.Background = Brushes.White;
                imgDniError.Visibility = Visibility.Hidden;
                checkBreed = true;
            }
        }

        private void txtBreed_KeyUp(object sender, KeyEventArgs e)
        {
            if (txtDni.Text.Length > 3 && txtDni.Text.Length < 20)
            {
                txtDni.BorderThickness = new Thickness(1);
                txtDni.BorderBrush = (SolidColorBrush)new BrushConverter().ConvertFrom("#FFABADB3");
                txtDni.Background = Brushes.White;
                imgDniError.Visibility = Visibility.Hidden;
                checkBreed = true;
            }
        }

        private void txtPhoneNumber_LostFocus(object sender, RoutedEventArgs e)
        {
            int value;
            bool isNumber = int.TryParse(txtPhoneNumber.Text, out value);

            if (isNumber)
            {
                if (value < 15 || value > 110)
                {
                    txtPhoneNumber.BorderThickness = new Thickness(2);
                    txtPhoneNumber.BorderBrush = Brushes.Red;
                    txtPhoneNumber.Background = Brushes.LightCoral;
                    imgPhoneNumberError.Visibility = Visibility.Visible;
                    checkSize = false;
                }
                else
                {
                    txtPhoneNumber.BorderThickness = new Thickness(1);
                    txtPhoneNumber.BorderBrush = (SolidColorBrush)new BrushConverter().ConvertFrom("#FFABADB3");
                    txtPhoneNumber.Background = Brushes.White;
                    imgPhoneNumberError.Visibility = Visibility.Hidden;
                    checkSize = true;
                }
            }
            else
            {
                txtPhoneNumber.BorderThickness = new Thickness(2);
                txtPhoneNumber.BorderBrush = Brushes.Red;
                txtPhoneNumber.Background = Brushes.LightCoral;
                imgPhoneNumberError.Visibility = Visibility.Visible;
                checkSize = false;
            }
        }

        private void txtPhoneNumber_KeyUp(object sender, KeyEventArgs e)
        {
            int value;
            bool isNumber = int.TryParse(txtPhoneNumber.Text, out value);

            if (isNumber)
            {
                if (value >= 15 || value <= 110)
                {
                    txtPhoneNumber.BorderThickness = new Thickness(1);
                    txtPhoneNumber.BorderBrush = (SolidColorBrush)new BrushConverter().ConvertFrom("#FFABADB3");
                    txtPhoneNumber.Background = Brushes.White;
                    imgPhoneNumberError.Visibility = Visibility.Hidden;
                    checkSize = true;
                }
            }
        }

        private void txtEmail_LostFocus(object sender, RoutedEventArgs e)
        {
            int value;
            bool isNumber = int.TryParse(txtEmail.Text, out value);

            if (isNumber)
            {
                if (value < 5 || value > 99)
                {
                    txtEmail.BorderThickness = new Thickness(2);
                    txtEmail.BorderBrush = Brushes.Red;
                    txtEmail.Background = Brushes.LightCoral;
                    imgEmailError.Visibility = Visibility.Visible;
                    checkWeight = false;
                }
                else
                {
                    txtEmail.BorderThickness = new Thickness(1);
                    txtEmail.BorderBrush = (SolidColorBrush)new BrushConverter().ConvertFrom("#FFABADB3");
                    txtEmail.Background = Brushes.White;
                    imgEmailError.Visibility = Visibility.Hidden;
                    checkWeight = true;
                }
            }
            else
            {
                txtEmail.BorderThickness = new Thickness(2);
                txtEmail.BorderBrush = Brushes.Red;
                txtEmail.Background = Brushes.LightCoral;
                imgEmailError.Visibility = Visibility.Visible;
                checkWeight = false;
            }
        }

        private void txtEmail_KeyUp(object sender, KeyEventArgs e)
        {
            int value;
            bool isNumber = int.TryParse(txtEmail.Text, out value);

            if (isNumber)
            {
                if (value >= 5 || value <= 99)
                {
                    txtEmail.BorderThickness = new Thickness(1);
                    txtEmail.BorderBrush = (SolidColorBrush)new BrushConverter().ConvertFrom("#FFABADB3");
                    txtEmail.Background = Brushes.White;
                    imgEmailError.Visibility = Visibility.Hidden;
                    checkWeight = true;
                }
            }
        }

        private void txtAge_LostFocus(object sender, RoutedEventArgs e)
        {
            int value;
            bool isNumber = int.TryParse(txtAge.Text, out value);

            if (isNumber)
            {
                if (value < 0 || value > 25)
                {
                    txtAge.BorderThickness = new Thickness(2);
                    txtAge.BorderBrush = Brushes.Red;
                    txtAge.Background = Brushes.LightCoral;
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
                txtAge.Background = Brushes.LightCoral;
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
                if (value >= 0 || value <= 25)
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
            int value;
            bool isNumber = int.TryParse(txtZone.Text, out value);

            if (isNumber)
            {
                if (txtZone.Text.Length != 6)
                {
                    txtZone.BorderThickness = new Thickness(2);
                    txtZone.BorderBrush = Brushes.Red;
                    txtZone.Background = Brushes.LightCoral;
                    imgZoneError.Visibility = Visibility.Visible;
                    checkChip = false;
                }
                else
                {
                    txtZone.BorderThickness = new Thickness(1);
                    txtZone.BorderBrush = (SolidColorBrush)new BrushConverter().ConvertFrom("#FFABADB3");
                    txtZone.Background = Brushes.White;
                    imgZoneError.Visibility = Visibility.Hidden;
                    checkChip = true;
                }
            }
            else
            {
                txtZone.BorderThickness = new Thickness(2);
                txtZone.BorderBrush = Brushes.Red;
                txtZone.Background = Brushes.LightCoral;
                imgZoneError.Visibility = Visibility.Visible;
                checkChip = false;
            }
        }

        private void txtZone_KeyUp(object sender, KeyEventArgs e)
        {
            int value;
            bool isNumber = int.TryParse(txtZone.Text, out value);

            if (isNumber)
            {
                if (txtZone.Text.Length == 6)
                {
                    txtZone.BorderThickness = new Thickness(1);
                    txtZone.BorderBrush = (SolidColorBrush)new BrushConverter().ConvertFrom("#FFABADB3");
                    txtZone.Background = Brushes.White;
                    imgZoneError.Visibility = Visibility.Hidden;
                    checkChip = true;
                }
            }
        }
    }
}
