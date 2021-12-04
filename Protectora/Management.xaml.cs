using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
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
using System.Xml;

namespace Protectora
{
    /// <summary>
    /// Lógica de interacción para Management.xaml
    /// </summary>
    public partial class Management : Window
    {
        //public List<Animal> animalList;
        public ObservableCollection<Animal> animalList;
        private int imgIndex = 0;
        bool first = true;
        private Window exitWindow;
        private Window newWindow;

        public Management(String user)
        {
            InitializeComponent();
            lblUser.Content = user;
            lblLastDate.Content = File.ReadAllText("datetime.txt");

            animalList = LoadContentXML();

            DataContext = animalList;

            btnAdd.ToolTip = "Añadir animal";
        }

        private ObservableCollection<Animal> LoadContentXML()
        {
            ObservableCollection<Animal> list = new ObservableCollection<Animal>();
            // Cargar contenido de prueba
            XmlDocument doc = new XmlDocument();
            var fichero = Application.GetResourceStream(new Uri("Data/animals.xml", UriKind.Relative));
            doc.Load(fichero.Stream);
            foreach (XmlNode node in doc.DocumentElement.ChildNodes)
            {
                List<string> pictures = new List<string>();
                var newAnimal = new Animal("", "", "", 0, 0, 0, 0, pictures);
                newAnimal.Name = node.Attributes["Name"].Value;
                newAnimal.Sex = node.Attributes["Sex"].Value;
                newAnimal.Breed = node.Attributes["Breed"].Value;
                newAnimal.Size = Convert.ToInt32(node.Attributes["Size"].Value);
                newAnimal.Weight = Convert.ToInt32(node.Attributes["Weight"].Value);
                newAnimal.Age = Convert.ToInt32(node.Attributes["Age"].Value);
                newAnimal.Chip = Convert.ToInt32(node.Attributes["Chip"].Value);
                newAnimal.Pictures.Add(node.Attributes["Picture1"].Value);
                newAnimal.Pictures.Add(node.Attributes["Picture2"].Value);
                newAnimal.Pictures.Add(node.Attributes["Picture3"].Value);
                newAnimal.Pictures.Add(node.Attributes["Picture4"].Value);
                
                list.Add(newAnimal);
            }
            
            return list;
        }

        private void buttonLogout_Click(object sender, RoutedEventArgs e)
        {
            exitWindow = new ExitWindow();
            exitWindow.Show();
        }
        private int getCurrentIndex() {
            int index = 0;
            for (int i = 0; i < animalList.Count; i++)
            {
                if (lblName.Content.ToString() == animalList[i].Name)
                {
                    index = i;
                    break;
                }
            }
            return index;
        }
        private void btbPreviousImage_Click(object sender, RoutedEventArgs e)
        {
            int index = getCurrentIndex();
            int size = animalList[index].Pictures.Count;

            imgIndex--;
            if (imgIndex < 0) {
                imgIndex = imgIndex + size;
            }
            var bitmap = new BitmapImage(new Uri(animalList[index].Pictures[imgIndex % size], UriKind.Relative));
            imgPicture.Source = bitmap;
        }

        private void btbNextImage_Click(object sender, RoutedEventArgs e)
        {
            int index = getCurrentIndex();
            int size = animalList[index].Pictures.Count;

            imgIndex++;
            if (imgIndex < 0)
            {
                imgIndex = imgIndex + size;
            }
            var bitmap = new BitmapImage(new Uri(animalList[index].Pictures[imgIndex % size], UriKind.Relative));
            imgPicture.Source = bitmap;
        }

        private void lstListaAnimales_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            int index = getCurrentIndex();

            var bitmap = new BitmapImage(new Uri(animalList[index].Pictures[0], UriKind.Relative));
            imgPicture.Source = bitmap;
        }

        private void btnEliminar_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnAniadir_Click(object sender, RoutedEventArgs e)
        {
            switch (((TabItem)tcPestanas.SelectedItem).Header.ToString())
            {
                case "Animales":
                    this.IsEnabled = false;
                    newWindow = new AddAnimal(this, animalList);
                    newWindow.Show();
                    break;
                case "Socios":
                    break;
                case "Voluntarios":
                    break;
                case "Ayuda":
                    /*DISABLE BUTTONS*/
                    break;
            }
        }


        private void tcPestanas_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (!first)
            {
                switch (((TabItem)tcPestanas.SelectedItem).Header.ToString())
                {
                    case "Animales":
                        btnAdd.ToolTip = "Añadir animal";
                        btnEdit.ToolTip = "Editar animal";
                        btnDelete.ToolTip = "Eliminar animal";
                        break;
                    case "Socios":
                        btnAdd.ToolTip = "Añadir socio";
                        btnEdit.ToolTip = "Editar animal";
                        btnDelete.ToolTip = "Eliminar socio";
                        break;
                    case "Voluntarios":
                        btnAdd.ToolTip = "Añadir voluntario";
                        btnEdit.ToolTip = "Editar animal";
                        btnDelete.ToolTip = "Eliminar voluntario";
                        break;
                    case "Ayuda":
                        /*DISABLE BUTTONS*/
                        break;
                }
            }
            else {
                first = false;
            }
        }

        private void Window_Closing(object sender, CancelEventArgs e)
        {
            string datetime = "Último acceso: " + DateTime.Now.ToString("dd-MM-yyyy HH:mm");
            File.WriteAllText("datetime.txt", datetime);
            Application.Current.Shutdown();
        }

        private void Window_GotFocus(object sender, RoutedEventArgs e)
        {
            DataContext = animalList;
        }
    }
}
