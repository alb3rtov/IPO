using Microsoft.Win32;
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
    /// Lógica de interacción para EditVolunteer.xaml
    /// </summary>
    public partial class EditVolunteer : Window
    {
        private Window parent;
        private Volunteer volunteer;
        private bool checkFirstName = false;
        private bool checkLastName = false;
        private bool checkDni = false;
        private bool checkPhoneNumber = false;
        private bool checkEmail = false;
        private bool checkAge = false;
        private bool checkZone = false;
        private string filename;
        Dictionary<int, string> times = new Dictionary<int, string>() {
            {0,"7:00"},
            {1,"8:00"},
            {2,"9:00"},
            {3,"10:00"},
            {4,"11:00"},
            {5,"12:00"},
            {6,"13:00"},
            {7,"14:00"},
            {8,"15:00"},
            {9,"16:00"},
            {10,"17:00"},
            {11,"18:00"},
            {12,"19:00"},
            {13,"20:00"},
            {14,"21:00"}
        };

        public EditVolunteer(Window window, Volunteer v)
        {
            InitializeComponent();
            parent = window;
            volunteer = v;
            DataContext = volunteer;

            /* Set volunteers diferent details elements */
            setStudies(volunteer.Studies);
            setTime(volunteer.TimeAvailability);
            setImage(addImages1, volunteer.Photo);
        }


        /* When close window enable parent window */
        private void Window_Closing(object sender, CancelEventArgs e)
        {
            parent.IsEnabled = true;
        }

        /* Set studies of volunteer */
        private void setStudies(string studies) {
            
            if (studies == "Grado en veterinaria")
            {
                cbStudies.SelectedIndex = 0;
            }
            else if (studies == "Auxiliar veterinario") 
            {
                cbStudies.SelectedIndex = 1;
            } 
            else if (studies == "Ninguno")
            {
                cbStudies.SelectedIndex = 2;
            } 
            else if (studies == "Otro")
            {
                cbStudies.SelectedIndex = 3;
            }
        }

        private void selectTime(string time, ComboBox cb) {
            for (int i = 0; i < times.Count; i++)
            {
                if (time == times[i])
                {
                    cb.SelectedIndex = i;
                    break;
                }
            }
        }

        /* Set time availability of volunteer */
        private void setTime(string timeAvailability) {
            string[] subs = timeAvailability.Split('-');

            selectTime(subs[0], cbInitial);
            selectTime(subs[1], cbEnd);
        }

        /* Set a given image in a given button */
        private void setImage(Button addImages, Uri image)
        {
            var brush = new ImageBrush();

            if (image.ToString().Substring(0, 1) == "I")
            {
                brush.ImageSource = new BitmapImage(new Uri("pack://application:,,,/" + image.ToString(), UriKind.RelativeOrAbsolute));
            }
            else
            {
                brush.ImageSource = new BitmapImage(new Uri(image.ToString(), UriKind.RelativeOrAbsolute));
            }

            addImages.Background = brush;
        }

        /* Edit volunteer checking that all fields are valid */
        private void btbEdit_Click(object sender, RoutedEventArgs e)
        {
            object senders = null;
            KeyEventArgs es = null;
            txtFirstName_LostFocus(senders, es);
            txtLastName_LostFocus(senders, es);
            txtDni_LostFocus(senders, es);
            txtPhoneNumber_LostFocus(senders, es);
            txtEmail_LostFocus(senders, es);
            txtAge_LostFocus(senders, es);
            txtZone_LostFocus(senders, es);

            if (checkFirstName && checkLastName && checkDni && checkPhoneNumber && checkEmail && checkAge && checkZone)
            {
                volunteer.Studies = cbStudies.Text;
                volunteer.TimeAvailability = cbInitial.Text + "-" + cbEnd.Text; 
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
                    addImages1.Background = brush;
                    volunteer.Photo = new Uri(filename, UriKind.RelativeOrAbsolute);
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
                txtFirstName.Background = Brushes.LightCoral;
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
                txtLastName.Background = Brushes.LightCoral;
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
                    txtDni.Background = Brushes.LightCoral;
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
            else
            {
                txtDni.BorderThickness = new Thickness(2);
                txtDni.BorderBrush = Brushes.Red;
                txtDni.Background = Brushes.LightCoral;
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
                    txtPhoneNumber.Background = Brushes.LightCoral;
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
                txtPhoneNumber.Background = Brushes.LightCoral;
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
            if (txtEmail.Text.Length <= 7 || txtEmail.Text.Length >= 30)
            {
                txtEmail.BorderThickness = new Thickness(2);
                txtEmail.BorderBrush = Brushes.Red;
                txtEmail.Background = Brushes.LightCoral;
                imgEmailError.Visibility = Visibility.Visible;
                checkEmail = false;
            }
            else
            {
                txtEmail.BorderThickness = new Thickness(1);
                txtEmail.BorderBrush = (SolidColorBrush)new BrushConverter().ConvertFrom("#FFABADB3");
                txtEmail.Background = Brushes.White;
                imgEmailError.Visibility = Visibility.Hidden;
                checkEmail = true;
            }
        }

        private void txtEmail_KeyUp(object sender, KeyEventArgs e)
        {
            if (txtEmail.Text.Length >= 7 && txtEmail.Text.Length <= 30)
            {
                txtEmail.BorderThickness = new Thickness(1);
                txtEmail.BorderBrush = (SolidColorBrush)new BrushConverter().ConvertFrom("#FFABADB3");
                txtEmail.Background = Brushes.White;
                imgEmailError.Visibility = Visibility.Hidden;
                checkEmail = true;
            }
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
                txtZone.Background = Brushes.LightCoral;
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
