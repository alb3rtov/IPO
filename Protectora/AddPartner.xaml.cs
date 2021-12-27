using Microsoft.Win32;
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
    /// Lógica de interacción para AddPartner.xaml
    /// </summary>
    public partial class AddPartner : Window
    {
        private Window parent;
        private ObservableCollection<Partner> partners;
        private bool checkFirstName = false;
        private bool checkLastName = false;
        private bool checkDni = false;
        private bool checkBankNumber = false;
        private bool checkMonthlyContribution = false;
        private bool checkImages = false;
        private string filename;

        public AddPartner(Window window, ObservableCollection<Partner> partnerList)
        {
            InitializeComponent();
            parent = window;
            partners = partnerList;
            DataContext = partners;
        }

        /* When close window enable parent window */
        private void Window_Closing(object sender, CancelEventArgs e)
        {
            parent.IsEnabled = true;
        }

        /* Select image */
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
            else
            {
                txtDni.BorderThickness = new Thickness(2);
                txtDni.BorderBrush = Brushes.Red;
                //txtDni.Background = Brushes.LightCoral;
                imgDniError.Visibility = Visibility.Visible;
                checkDni = false;
            }
        }

        private void txtBankNumber_KeyUp(object sender, KeyEventArgs e)
        {
            if (txtBankNumber.Text.Length >= 24 && txtBankNumber.Text.Length <= 30)
            {
                txtBankNumber.BorderThickness = new Thickness(1);
                txtBankNumber.BorderBrush = (SolidColorBrush)new BrushConverter().ConvertFrom("#FFABADB3");
                txtBankNumber.Background = Brushes.White;
                imgBankNumber.Visibility = Visibility.Hidden;
                checkBankNumber = true;
            }
        }

        private void txtBankNumber_LostFocus(object sender, RoutedEventArgs e)
        {
            if (txtBankNumber.Text.Length >= 24 && txtBankNumber.Text.Length <= 30)
            {
                txtBankNumber.BorderThickness = new Thickness(1);
                txtBankNumber.BorderBrush = (SolidColorBrush)new BrushConverter().ConvertFrom("#FFABADB3");
                txtBankNumber.Background = Brushes.White;
                imgBankNumber.Visibility = Visibility.Hidden;
                checkBankNumber = true;
            }
            else
            {
                txtBankNumber.BorderThickness = new Thickness(2);
                txtBankNumber.BorderBrush = Brushes.Red;
                //txtLastName.Background = Brushes.LightCoral;
                imgBankNumber.Visibility = Visibility.Visible;
                checkBankNumber = false;
            }
        }

        private void txtMonthlyContribution_KeyUp(object sender, KeyEventArgs e)
        {
            int value;
            bool isNumber = int.TryParse(txtMonthlyContribution.Text, out value);

            if (isNumber)
            {
                if (txtMonthlyContribution.Text.Length >= 1)
                {
                    txtMonthlyContribution.BorderThickness = new Thickness(1);
                    txtMonthlyContribution.BorderBrush = (SolidColorBrush)new BrushConverter().ConvertFrom("#FFABADB3");
                    txtMonthlyContribution.Background = Brushes.White;
                    imgMonthlyContribution.Visibility = Visibility.Hidden;
                    checkMonthlyContribution = true;
                }
            }
        }

        private void txtMonthlyContribution_LostFocus(object sender, RoutedEventArgs e)
        {
            int value;
            bool isNumber = int.TryParse(txtMonthlyContribution.Text, out value);

            if (isNumber)
            {
                if (txtMonthlyContribution.Text.Length < 1)
                {
                    txtMonthlyContribution.BorderThickness = new Thickness(2);
                    txtMonthlyContribution.BorderBrush = Brushes.Red;
                    //txtPhoneNumber.Background = Brushes.LightCoral;
                    imgMonthlyContribution.Visibility = Visibility.Visible;
                    checkMonthlyContribution = false;
                }
                else
                {
                    txtMonthlyContribution.BorderThickness = new Thickness(1);
                    txtMonthlyContribution.BorderBrush = (SolidColorBrush)new BrushConverter().ConvertFrom("#FFABADB3");
                    txtMonthlyContribution.Background = Brushes.White;
                    imgMonthlyContribution.Visibility = Visibility.Hidden;
                    checkMonthlyContribution = true;
                }
            }
            else
            {
                txtMonthlyContribution.BorderThickness = new Thickness(2);
                txtMonthlyContribution.BorderBrush = Brushes.Red;
                //txtPhoneNumber.Background = Brushes.LightCoral;
                imgMonthlyContribution.Visibility = Visibility.Visible;
                checkMonthlyContribution = false;
            }
        }

        private void btbAdd_Click(object sender, RoutedEventArgs e)
        {
            if (checkFirstName && checkLastName && checkDni && checkBankNumber && checkMonthlyContribution && checkImages)
            {
                Partner partner = new Partner(int.Parse(txtDni.Text), txtFirstName.Text, txtLastName.Text, txtBankNumber.Text, int.Parse(txtMonthlyContribution.Text), cbPaymentMethod.Text, filename);
                partners.Add(partner);
                this.Close();
            }
            else
            {
                MessageBox.Show("Debe de introducir datos correctos en el formulario", "Error en el formulario", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
