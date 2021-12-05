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
        }

        private void Window_Closing(object sender, CancelEventArgs e)
        {
            parent.IsEnabled = true;
        }

        private void btbAdd_Click(object sender, RoutedEventArgs e)
        {
            if (checkName && checkBreed && checkSize && checkWeight && checkAge && checkChip && checkImages)
            {
                Animal animal = new Animal(txtName.Text, cbSex.Text, txtBreed.Text, int.Parse(txtSize.Text), int.Parse(txtWeight.Text), int.Parse(txtAge.Text), int.Parse(txtChip.Text), filenames);
                animals.Add(animal);
                this.Close();
            }
            else {
                MessageBox.Show("Debe de introducir datos correctos en el formulario", "Error en el formulario", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            
        }

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

        private void txtName_LostFocus(object sender, RoutedEventArgs e)
        {
            if (txtName.Text.Length <= 2 || txtName.Text.Length >= 12)
            {
                txtName.BorderThickness = new Thickness(2);
                txtName.BorderBrush = Brushes.Red;
                txtName.Background = Brushes.LightCoral;
                checkName = false;
            }
            else
            {
                txtName.BorderThickness = new Thickness(1);
                txtName.BorderBrush = (SolidColorBrush)new BrushConverter().ConvertFrom("#FFABADB3");
                txtName.Background = Brushes.White;
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
                checkName = true;
            }
        }

        private void txtBreed_LostFocus(object sender, RoutedEventArgs e)
        {
            if (txtBreed.Text.Length <= 3 || txtBreed.Text.Length >= 20)
            {
                txtBreed.BorderThickness = new Thickness(2);
                txtBreed.BorderBrush = Brushes.Red;
                txtBreed.Background = Brushes.LightCoral;
                checkBreed = false;
            }
            else
            {
                txtBreed.BorderThickness = new Thickness(1);
                txtBreed.BorderBrush = (SolidColorBrush)new BrushConverter().ConvertFrom("#FFABADB3");
                txtBreed.Background = Brushes.White;
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
                checkBreed = true;
            }
        }

        private void txtSize_LostFocus(object sender, RoutedEventArgs e)
        {
            int value;
            bool isNumber = int.TryParse(txtSize.Text, out value);

            if (isNumber)
            {
                if (value < 0 || value > 99)
                {
                    txtSize.BorderThickness = new Thickness(2);
                    txtSize.BorderBrush = Brushes.Red;
                    txtSize.Background = Brushes.LightCoral;
                    checkSize = false;
                }
                else
                {
                    txtSize.BorderThickness = new Thickness(1);
                    txtSize.BorderBrush = (SolidColorBrush)new BrushConverter().ConvertFrom("#FFABADB3");
                    txtSize.Background = Brushes.White;
                    checkSize = true;
                }
            }
            else
            {
                txtSize.BorderThickness = new Thickness(2);
                txtSize.BorderBrush = Brushes.Red;
                txtSize.Background = Brushes.LightCoral;
                checkSize = false;
            }
        }

        private void txtSize_KeyUp(object sender, KeyEventArgs e)
        {
            int value;
            bool isNumber = int.TryParse(txtSize.Text, out value);

            if (isNumber)
            {
                if (value >= 0 || value <= 99)
                {
                    txtSize.BorderThickness = new Thickness(1);
                    txtSize.BorderBrush = (SolidColorBrush)new BrushConverter().ConvertFrom("#FFABADB3");
                    txtSize.Background = Brushes.White;
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
                if (value < 0 || value > 99)
                {
                    txtWeight.BorderThickness = new Thickness(2);
                    txtWeight.BorderBrush = Brushes.Red;
                    txtWeight.Background = Brushes.LightCoral;
                    checkWeight = false;
                }
                else
                {
                    txtWeight.BorderThickness = new Thickness(1);
                    txtWeight.BorderBrush = (SolidColorBrush)new BrushConverter().ConvertFrom("#FFABADB3");
                    txtWeight.Background = Brushes.White;
                    checkWeight = true;
                }
            }
            else
            {
                txtWeight.BorderThickness = new Thickness(2);
                txtWeight.BorderBrush = Brushes.Red;
                txtWeight.Background = Brushes.LightCoral;
                checkWeight = false;
            }
        }

        private void txtWeight_KeyUp(object sender, KeyEventArgs e)
        {
            int value;
            bool isNumber = int.TryParse(txtWeight.Text, out value);

            if (isNumber)
            {
                if (value >= 0 || value <= 99)
                {
                    txtWeight.BorderThickness = new Thickness(1);
                    txtWeight.BorderBrush = (SolidColorBrush)new BrushConverter().ConvertFrom("#FFABADB3");
                    txtWeight.Background = Brushes.White;
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
                    checkAge = false;
                }
                else
                {
                    txtAge.BorderThickness = new Thickness(1);
                    txtAge.BorderBrush = (SolidColorBrush)new BrushConverter().ConvertFrom("#FFABADB3");
                    txtAge.Background = Brushes.White;
                    checkAge = true;
                }
            }
            else
            {
                txtAge.BorderThickness = new Thickness(2);
                txtAge.BorderBrush = Brushes.Red;
                txtAge.Background = Brushes.LightCoral;
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
                    txtChip.Background = Brushes.LightCoral;
                    checkChip = false;
                }
                else
                {
                    txtChip.BorderThickness = new Thickness(1);
                    txtChip.BorderBrush = (SolidColorBrush)new BrushConverter().ConvertFrom("#FFABADB3");
                    txtChip.Background = Brushes.White;
                    checkChip = true;
                }
            }
            else
            {
                txtChip.BorderThickness = new Thickness(2);
                txtChip.BorderBrush = Brushes.Red;
                txtChip.Background = Brushes.LightCoral;
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
                    checkChip = true;
                }
            }
        }
    }
}
