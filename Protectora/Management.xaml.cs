using System;
using System.Collections.Generic;
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
        private List<Animal> animalList;
        public Management(String user)
        {
            InitializeComponent();
            lblUser.Content = user;
            lblLastDate.Content = File.ReadAllText("datetime.txt");

            // Se cargarán los datos de prueba de un fichero XML
            animalList = LoadContentXML();
            // Indicar que el origen de datos del ListBox es listadoPeliculas
            DataContext = animalList;
        }

        private List<Animal> LoadContentXML()
        {
            List<Animal> list = new List<Animal>();
            // Cargar contenido de prueba
            XmlDocument doc = new XmlDocument();
            var fichero = Application.GetResourceStream(new Uri("Data/animals.xml", UriKind.Relative));
            doc.Load(fichero.Stream);
            foreach (XmlNode node in doc.DocumentElement.ChildNodes)
            {
                var newAnimal = new Animal("", "", "", 0, 0, 0, 0, "");
                newAnimal.Name = node.Attributes["Name"].Value;
                newAnimal.Sex = node.Attributes["Sex"].Value;
                newAnimal.Breed = node.Attributes["Breed"].Value;
                newAnimal.Size = Convert.ToInt32(node.Attributes["Size"].Value);
                newAnimal.Weight = Convert.ToInt32(node.Attributes["Weight"].Value);
                newAnimal.Age = Convert.ToInt32(node.Attributes["Age"].Value);
                newAnimal.Chip = Convert.ToInt32(node.Attributes["Chip"].Value);
                newAnimal.Picture = node.Attributes["Picture"].Value;

                list.Add(newAnimal);
            }
            return list;
        }

        private void buttonLogout_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("¿Desea cerrar sesión?", "Cerrar sesión", MessageBoxButton.YesNo, MessageBoxImage.Information) == MessageBoxResult.Yes)
            {
                
                string datetime = "Último acceso: " + DateTime.Now.ToString("dd-MM-yyyy hh:mm");
                File.WriteAllText("datetime.txt", datetime);
                Application.Current.Shutdown();
            }
        }
    }
}
