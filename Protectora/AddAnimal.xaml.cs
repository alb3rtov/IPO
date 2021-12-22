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
    /// Lógica de interacción para AddAnimal.xaml
    /// </summary>
    public partial class AddAnimal : Window
    {
        private Window parent;
        private ObservableCollection<Animal> animals;
        private List<string> filenames = new List<string>();
        private bool checkName = false;
        private bool checkBreed = false;
        private bool checkSize = false;
        private bool checkWeight = false;
        private bool checkAge = false;
        private bool checkChip = false;
        private bool checkImages = false;

        public AddAnimal(Window window, ObservableCollection<Animal> animalList)
        {
            InitializeComponent();
            parent = window;
            animals = animalList;
            DataContext = animals;
            rdSterelizedSi.IsChecked = true;
            rdPPPSi.IsChecked = true;
            rdChildrenSi.IsChecked = true;
            rdDogsSi.IsChecked = true;
        }

        /* When close window enable parent window */
        private void Window_Closing(object sender, CancelEventArgs e)
        {
            parent.IsEnabled = true;
        }

        /* Returns if a given button is checked */
        private string getRadioButton(RadioButton rdYes) {
            return (rdYes.IsChecked == true) ? "Si" : "No";
        }

        /* Event for add button checking that all fields are valid */
        private void btbAdd_Click(object sender, RoutedEventArgs e)
        {
            if (checkName && checkBreed && checkSize && checkWeight && checkAge && checkChip && checkImages)
            {
                string ppp = getRadioButton(rdPPPSi);
                string sterelized = getRadioButton(rdSterelizedSi);
                string dogs = getRadioButton(rdDogsSi);
                string childen = getRadioButton(rdChildrenSi);

                Animal animal = new Animal(txtName.Text, cbSex.Text, txtBreed.Text, int.Parse(txtSize.Text), int.Parse(txtWeight.Text), int.Parse(txtAge.Text), int.Parse(txtChip.Text), filenames, null, null, sterelized, childen, dogs, ppp);
                animals.Add(animal);
                this.Close();
            }
            else {
                MessageBox.Show("Debe de introducir datos correctos en el formulario", "Error en el formulario", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        /* Event for add images button */
        private void addImages_Click(object sender, RoutedEventArgs e)
        {
            var openDialog = new OpenFileDialog();
            openDialog.Filter = "Images|*.jpg;*.gif;*.bmp;*.png";

            if (openDialog.ShowDialog() == true)
            {
                try
                {
                    filenames.Add(openDialog.FileName);
                    var brush = new ImageBrush();
                    brush.ImageSource = new BitmapImage(new Uri(openDialog.FileName, UriKind.RelativeOrAbsolute));
                    checkImages = true;

                    if (addImages1.IsFocused)
                    {
                        addImages1.Background = brush;
                    }
                    else if (addImages2.IsFocused)
                    {
                        addImages2.Background = brush;
                    }
                    else if (addImages3.IsFocused)
                    {
                        addImages3.Background = brush;
                    }
                    else if (addImages4.IsFocused)
                    {
                        addImages4.Background = brush;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al cargar la imagen " + ex.Message);
                }

            }
        }

        /* Constraints for all fields */
        private void txtName_LostFocus(object sender, RoutedEventArgs e)
        {
            if (txtName.Text.Length <= 2 || txtName.Text.Length >= 12)
            {
                txtName.BorderThickness = new Thickness(2);
                txtName.BorderBrush = Brushes.Red;
                //txtName.Background = Brushes.LightCoral;
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
            if (txtBreed.Text.Length <= 3 || txtBreed.Text.Length >= 20)
            {
                txtBreed.BorderThickness = new Thickness(2);
                txtBreed.BorderBrush = Brushes.Red;
                //txtBreed.Background = Brushes.LightCoral;
                imgBreedError.Visibility = Visibility.Visible;
                checkBreed = false;
            }
            else
            {
                txtBreed.BorderThickness = new Thickness(1);
                txtBreed.BorderBrush = (SolidColorBrush)new BrushConverter().ConvertFrom("#FFABADB3");
                txtBreed.Background = Brushes.White;
                imgBreedError.Visibility = Visibility.Hidden;
                checkBreed = true;
            }
        }

        private void txtBreed_KeyUp(object sender, KeyEventArgs e)
        {
            if (txtBreed.Text.Length > 3 && txtBreed.Text.Length < 20)
            {
                txtBreed.BorderThickness = new Thickness(1);
                txtBreed.BorderBrush = (SolidColorBrush)new BrushConverter().ConvertFrom("#FFABADB3");
                txtBreed.Background = Brushes.White;
                imgBreedError.Visibility = Visibility.Hidden;
                checkBreed = true;
            }
        }

        private void txtSize_LostFocus(object sender, RoutedEventArgs e)
        {
            int value;
            bool isNumber = int.TryParse(txtSize.Text, out value);

            if (isNumber)
            {
                if (value < 15 || value > 110)
                {
                    txtSize.BorderThickness = new Thickness(2);
                    txtSize.BorderBrush = Brushes.Red;
                    //txtSize.Background = Brushes.LightCoral;
                    imgSizeError.Visibility = Visibility.Visible;
                    checkSize = false;
                }
                else
                {
                    txtSize.BorderThickness = new Thickness(1);
                    txtSize.BorderBrush = (SolidColorBrush)new BrushConverter().ConvertFrom("#FFABADB3");
                    txtSize.Background = Brushes.White;
                    imgSizeError.Visibility = Visibility.Hidden;
                    checkSize = true;
                }
            }
            else
            {
                txtSize.BorderThickness = new Thickness(2);
                txtSize.BorderBrush = Brushes.Red;
                //txtSize.Background = Brushes.LightCoral;
                imgSizeError.Visibility = Visibility.Visible;
                checkSize = false;
            }
        }

        private void txtSize_KeyUp(object sender, KeyEventArgs e)
        {
            int value;
            bool isNumber = int.TryParse(txtSize.Text, out value);

            if (isNumber)
            {
                if (value >= 15 || value <= 110)
                {
                    txtSize.BorderThickness = new Thickness(1);
                    txtSize.BorderBrush = (SolidColorBrush)new BrushConverter().ConvertFrom("#FFABADB3");
                    txtSize.Background = Brushes.White;
                    imgSizeError.Visibility = Visibility.Hidden;
                    checkSize = true;
                }
            }
        }

        private void txtWeight_LostFocus(object sender, RoutedEventArgs e)
        {
            int value;
            bool isNumber = int.TryParse(txtWeight.Text, out value);

            if (isNumber)
            {
                if (value < 5 || value > 99)
                {
                    txtWeight.BorderThickness = new Thickness(2);
                    txtWeight.BorderBrush = Brushes.Red;
                    //txtWeight.Background = Brushes.LightCoral;
                    imgWeightError.Visibility = Visibility.Visible;
                    checkWeight = false;
                }
                else
                {
                    txtWeight.BorderThickness = new Thickness(1);
                    txtWeight.BorderBrush = (SolidColorBrush)new BrushConverter().ConvertFrom("#FFABADB3");
                    txtWeight.Background = Brushes.White;
                    imgWeightError.Visibility = Visibility.Hidden;
                    checkWeight = true;
                }
            }
            else
            {
                txtWeight.BorderThickness = new Thickness(2);
                txtWeight.BorderBrush = Brushes.Red;
                //txtWeight.Background = Brushes.LightCoral;
                imgWeightError.Visibility = Visibility.Visible;
                checkWeight = false;
            }
        }

        private void txtWeight_KeyUp(object sender, KeyEventArgs e)
        {
            int value;
            bool isNumber = int.TryParse(txtWeight.Text, out value);

            if (isNumber)
            {
                if (value >= 5 || value <= 99)
                {
                    txtWeight.BorderThickness = new Thickness(1);
                    txtWeight.BorderBrush = (SolidColorBrush)new BrushConverter().ConvertFrom("#FFABADB3");
                    txtWeight.Background = Brushes.White;
                    imgWeightError.Visibility = Visibility.Hidden;
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

        private void txtChip_LostFocus(object sender, RoutedEventArgs e)
        {
            int value;
            bool isNumber = int.TryParse(txtChip.Text, out value);

            if (isNumber)
            {
                if (txtChip.Text.Length != 6)
                {
                    txtChip.BorderThickness = new Thickness(2);
                    txtChip.BorderBrush = Brushes.Red;
                    //txtChip.Background = Brushes.LightCoral;
                    imgChipError.Visibility = Visibility.Visible;
                    checkChip = false;
                }
                else
                {
                    txtChip.BorderThickness = new Thickness(1);
                    txtChip.BorderBrush = (SolidColorBrush)new BrushConverter().ConvertFrom("#FFABADB3");
                    txtChip.Background = Brushes.White;
                    imgChipError.Visibility = Visibility.Hidden;
                    checkChip = true;
                }
            }
            else
            {
                txtChip.BorderThickness = new Thickness(2);
                txtChip.BorderBrush = Brushes.Red;
                //txtChip.Background = Brushes.LightCoral;
                imgChipError.Visibility = Visibility.Visible;
                checkChip = false;
            }
        }

        private void txtChip_KeyUp(object sender, KeyEventArgs e)
        {
            int value;
            bool isNumber = int.TryParse(txtChip.Text, out value);

            if (isNumber)
            {
                if (txtChip.Text.Length == 6)
                {
                    txtChip.BorderThickness = new Thickness(1);
                    txtChip.BorderBrush = (SolidColorBrush)new BrushConverter().ConvertFrom("#FFABADB3");
                    txtChip.Background = Brushes.White;
                    imgChipError.Visibility = Visibility.Hidden;
                    checkChip = true;
                }
            }
        }
    }
}
