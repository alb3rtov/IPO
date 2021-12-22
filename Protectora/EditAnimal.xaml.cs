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
    /// Lógica de interacción para EditAnimal.xaml
    /// </summary>
    public partial class EditAnimal : Window
    {
        private Window parent;
        private List<string> filenames = new List<string>();
        private Animal animal;
        private bool checkName = false;
        private bool checkBreed = false;
        private bool checkSize = false;
        private bool checkWeight = false;
        private bool checkAge = false;
        private bool checkChip = false;
        private bool checkaddImages1 = false;
        private bool checkaddImages2 = false;
        private bool checkaddImages3 = false;
        private bool checkaddImages4 = false;

        public EditAnimal(Window window, Animal a)
        {
            InitializeComponent();
            parent = window;
            animal = a;
            DataContext = animal;
            
            /* Set animals diferent details elements */
            setSex(animal.Sex);
            setRd(animal.SociableChildren.ToString(), rdChildrenSi, rdChildrenNo);
            setRd(animal.Sterilized.ToString(), rdSterelizedSi, rdSterelizedNo);
            setRd(animal.SociableDogs.ToString(), rdDogsSi, rdDogsNo);
            setRd(animal.Ppp.ToString(), rdPPPSi, rdPPPNo);

            /* Set current images in buttons */
            if (animal.Pictures.Count >= 1) {
                setImage(addImages1, animal.Pictures[0]);
            }
            if (animal.Pictures.Count >= 2) {
                setImage(addImages2, animal.Pictures[1]);
            }
            if (animal.Pictures.Count >= 3) {
                setImage(addImages3, animal.Pictures[2]);
            }
            if (animal.Pictures.Count >= 4) {
                setImage(addImages4, animal.Pictures[3]);
            }   
        }

        /* When close window enable parent window */
        private void Window_Closing(object sender, CancelEventArgs e)
        {
            parent.IsEnabled = true;
        }

        /* Set a given image in a given button */
        private void setImage(Button addImages, string image)
        {
            var brush = new ImageBrush();
            
            if (image.Substring(0, 1) == "I")
            {
                brush.ImageSource = new BitmapImage(new Uri("pack://application:,,,/" + image, UriKind.RelativeOrAbsolute));
            }
            else {
                brush.ImageSource = new BitmapImage(new Uri(image, UriKind.RelativeOrAbsolute));
            }

            addImages.Background = brush;
        }

        /* Get index of the button that is currently pressed */
        private int getButtonPressed() {
            int index = 0;
            
            if (checkaddImages1)
            {
                index = 0;
            }
            else if (checkaddImages2) 
            {
                index = 1;
            }
            else if (checkaddImages3)
            {
                index = 2;
            }
            else if (checkaddImages4)
            {
                index = 3;
            }
            return index;
        }   

        /* Check radio button depeding on the given string */
        private void setRd(string rd, RadioButton yes, RadioButton no) {
            if (rd == "Si")
            {
                yes.IsChecked = true;
            }
            else
            {
                no.IsChecked = true;
            }
        }

        /* Set animal sex in combobox */
        private void setSex(string sex) {
            cbSex.SelectedIndex = (sex == "Macho") ? 0 : 1;
        }

        /* Event for add images button */
        private void addImages_Click(object sender, RoutedEventArgs e)
        {
            var openDialog = new OpenFileDialog();
            openDialog.Filter = "Images|*.jpg;*.gif;*.bmp;*.png";
            int index = getButtonPressed();
            
            if (openDialog.ShowDialog() == true)
            {
                try
                {
                    if (animal.Pictures.Count > index)
                    {
                        animal.Pictures.RemoveAt(index);
                        animal.Pictures.Insert(index, openDialog.FileName);
                    }
                    else {
                        animal.Pictures.Add(openDialog.FileName);
                    }

                    var brush = new ImageBrush();
                    brush.ImageSource = new BitmapImage(new Uri(openDialog.FileName, UriKind.RelativeOrAbsolute));

                    if (addImages1.IsFocused)
                    {
                        addImages1.Background = brush;
                        checkaddImages1 = false;
                    }
                    else if (addImages2.IsFocused)
                    {
                        addImages2.Background = brush;
                        checkaddImages2 = false;
                    }
                    else if (addImages3.IsFocused)
                    {
                        addImages3.Background = brush;
                        checkaddImages3 = false;
                    }
                    else if (addImages4.IsFocused)
                    {
                        addImages4.Background = brush;
                        checkaddImages4 = false;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al cargar la imagen " + ex.Message);
                }

            }
        }

        /* Get current selection in radio button */
        private string getRadioButton(RadioButton rdYes)
        {
            return (rdYes.IsChecked == true) ? "Si" : "No";
        }

        /* Event for edit button checking that all fields are valid */
        private void btbEdit_Click(object sender, RoutedEventArgs e)
        {
            object senders = null;
            KeyEventArgs es = null;
            txtChip_LostFocus(senders, es);
            txtAge_LostFocus(senders, es);
            txtWeight_LostFocus(senders, es);
            txtSize_LostFocus(senders, es);
            txtName_LostFocus(senders, es);
            txtBreed_LostFocus(senders, es);

            if (checkName && checkBreed && checkSize && checkWeight && checkAge && checkChip)
            {
                animal.Sex = cbSex.Text;
                animal.Ppp = getRadioButton(rdPPPSi);
                animal.Sterilized = getRadioButton(rdSterelizedSi);
                animal.SociableDogs = getRadioButton(rdDogsSi);
                animal.SociableChildren = getRadioButton(rdChildrenSi);
                animal.Video = animal.Video;
                animal.Sponsor = animal.Sponsor;
                this.Close();
            }
            else
            {
                MessageBox.Show("Debe de introducir datos correctos en el formulario", "Error en el formulario", MessageBoxButton.OK, MessageBoxImage.Error);
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

        private void addImages1_GotFocus(object sender, RoutedEventArgs e)
        {
            checkaddImages1 = true;
        }

        private void addImages2_GotFocus(object sender, RoutedEventArgs e)
        {
            checkaddImages2 = true;
        }

        private void addImages3_GotFocus(object sender, RoutedEventArgs e)
        {
            checkaddImages3 = true;
        }

        private void addImages4_GotFocus(object sender, RoutedEventArgs e)
        {
            checkaddImages4 = true;
        }
    }
}
