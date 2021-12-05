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
            Animal animal = new Animal(txtName.Text, txtSex.Text, txtBreed.Text, 0, 0, 0, 0, filenames);
            animals.Add(animal);
            this.Close();
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

                    if (addImages1.IsFocused)
                    {
                        addImages1.Background = brush;
                    } 
                    else if (addImages2.IsFocused) {
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
    }
}
