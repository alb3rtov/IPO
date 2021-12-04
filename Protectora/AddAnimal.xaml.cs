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
        private string[] filenames;
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
            List<string> pictures = new List<string>();

            for (int i = 0; i < filenames.Length; i++) {
                pictures.Add(filenames[i]);
            }

            Animal animal = new Animal(txtName.Text, txtSex.Text, txtBreed.Text, 0, 0, 0, 0, pictures);
            animals.Add(animal);
            this.Close();
        }

        private void addImages_Click(object sender, RoutedEventArgs e)
        {
            var openDialog = new OpenFileDialog();
            openDialog.Multiselect = true;
            openDialog.Filter = "Images|*.jpg;*.gif;*.bmp;*.png";
            openDialog.ShowDialog();
            /*
        if (openDialog.ShowDialog() == true)
        {
            for (int i = 0; i < openDialog.FileNames.Length; i++) { 
                System.Diagnostics.Debug.WriteLine(openDialog.FileNames[i]);
            }

            try
            {
                var bitmap = new BitmapImage(new Uri(abrirDialog.FileName, UriKind.Absolute));
                imgCaratula.Source = bitmap;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar la imagen " + ex.Message);
            }
            
        }*/

            filenames = openDialog.FileNames;
        }
    }
}
