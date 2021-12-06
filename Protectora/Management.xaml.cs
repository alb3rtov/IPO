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
        public ObservableCollection<Animal> animalList;
        public List<Sponsor> sponsorList;
        private int imgIndex = 0;
        bool first = true;
        bool addAction = false;
        int sizeList = 0;
        private Window exitWindow;
        private Window newWindow;
        private Window sponsorWindow;
        private Window videoWindow;

        public Management(String user)
        {
            InitializeComponent();
            lblUser.Content = user;
            lblLastDate.Content = File.ReadAllText("datetime.txt");

            animalList = LoadContentAnimalsXML();
            sponsorList = LoadContentSponsorXML();
            addSponsor();



            DataContext = animalList;
        }

        private void addSponsor() {
            for (int i = 0; i < animalList.Count; i++) {
                animalList[i].Sponsor = sponsorList[i];
            }
        }

        private List<Sponsor> LoadContentSponsorXML() 
        {
            List<Sponsor> list = new List<Sponsor>();

            XmlDocument doc = new XmlDocument();
            var fichero = Application.GetResourceStream(new Uri("Data/sponsors.xml", UriKind.Relative));
            doc.Load(fichero.Stream);

            foreach (XmlNode node in doc.DocumentElement.ChildNodes)
            {
                DateTime time = DateTime.Now;
                var sponsor = new Sponsor(0, "", "", 0, "", "", 0, time, "");
                sponsor.Dni = Convert.ToInt32(node.Attributes["Dni"].Value);
                sponsor.Firstname = node.Attributes["Firstname"].Value;
                sponsor.Lastname = node.Attributes["Lastname"].Value;
                sponsor.PhoneNumber = Convert.ToInt32(node.Attributes["PhoneNumber"].Value);
                sponsor.PaymentMethod = node.Attributes["PaymentMethod"].Value;
                sponsor.BankAccountNumber = node.Attributes["BankAccountNumber"].Value;
                sponsor.MonthlyContribution = Convert.ToInt32(node.Attributes["MonthlyContribution"].Value);
                sponsor.StartSponsorship = Convert.ToDateTime(node.Attributes["StartSponsorship"].Value);
                sponsor.Picture = node.Attributes["Picture"].Value;
                list.Add(sponsor);
            }

            return list;
        }

        private ObservableCollection<Animal> LoadContentAnimalsXML()
        {
            ObservableCollection<Animal> list = new ObservableCollection<Animal>();
            // Cargar contenido de prueba
            XmlDocument doc = new XmlDocument();
            var fichero = Application.GetResourceStream(new Uri("Data/animals.xml", UriKind.Relative));
            doc.Load(fichero.Stream);
            foreach (XmlNode node in doc.DocumentElement.ChildNodes)
            {
                List<string> pictures = new List<string>();
                var newAnimal = new Animal("", "", "", 0, 0, 0, 0, pictures, null, null, "", "", "", "");
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
                newAnimal.Video = new Uri(node.Attributes["Video"].Value, UriKind.RelativeOrAbsolute);
                newAnimal.SociableChildren = node.Attributes["SociableChildren"].Value;
                newAnimal.SociableDogs = node.Attributes["SociableDogs"].Value;
                newAnimal.Sterilized = node.Attributes["Sterilized"].Value;
                newAnimal.Ppp = node.Attributes["PPP"].Value;

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

            var bitmap = new BitmapImage(new Uri(animalList[index].Pictures[imgIndex % size], UriKind.RelativeOrAbsolute));

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
            var bitmap = new BitmapImage(new Uri(animalList[index].Pictures[imgIndex % size], UriKind.RelativeOrAbsolute));

            imgPicture.Source = bitmap;
        }

        private void lstListaAnimales_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            fixImageDisplay();
        }

        private void btnEliminar_Click(object sender, RoutedEventArgs e)
        {

            if (animalList.Count == 0)
            {
                MessageBox.Show("No existen animales en la lista", "Error al eliminar animal", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else {
                int index = getCurrentIndex();

                if (MessageBox.Show("¿Desea eliminar el animal " + animalList[index].Name + "?", "Eliminar animal", MessageBoxButton.YesNo, MessageBoxImage.Information) == MessageBoxResult.Yes)
                {
                    animalList.RemoveAt(index);
                    int size = lstListaAnimales.Items.Count;

                    if (size != 0)
                    {
                        imgPicture.Visibility = Visibility.Visible;
                        lstListaAnimales.SelectedItem = lstListaAnimales.Items[size - 1];
                        lstListaAnimales.ScrollIntoView(lstListaAnimales.Items[size - 1]);
                        fixImageDisplay();
                    }
                    else
                    {
                        imgPicture.Visibility = Visibility.Hidden;
                    }
                }
            }            
        }

        private void btnAniadir_Click(object sender, RoutedEventArgs e)
        {
            switch (((TabItem)tcPestanas.SelectedItem).Header.ToString())
            {
                case "Animales":
                    this.IsEnabled = false;
                    addAction = true;
                    sizeList = animalList.Count;
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
                        btnEdit.ToolTip = "Editar socio";
                        btnDelete.ToolTip = "Eliminar socio";
                        break;
                    case "Voluntarios":
                        btnAdd.ToolTip = "Añadir voluntario";
                        btnEdit.ToolTip = "Editar voluntario";
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

        private void fixImageDisplay()
        {
            int index = getCurrentIndex();
            var bitmap = new BitmapImage(new Uri(animalList[index].Pictures[0], UriKind.RelativeOrAbsolute));
            imgPicture.Source = bitmap;
            imgPicture.Visibility = Visibility.Visible;

            if (animalList[index].Sponsor == null)
            {
                btbSponsor.IsEnabled = false;
                btbVideo.IsEnabled = false;
            }
            else {
                btbSponsor.IsEnabled = true;
                btbVideo.IsEnabled = true;
            }
        }

        private void lstListaAnimales_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Down || e.Key == Key.Up) {
                fixImageDisplay();
            }
        }

        private void Window_IsEnabledChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (this.IsEnabled && addAction) {
                if (sizeList != animalList.Count)
                {
                    sizeList = animalList.Count;
                    addAction = false;
                    int size = lstListaAnimales.Items.Count;
                    lstListaAnimales.SelectedItem = lstListaAnimales.Items[size - 1];
                    lstListaAnimales.ScrollIntoView(lstListaAnimales.Items[size - 1]);
                    fixImageDisplay();
                }
            }
        }

        private void btnEdit_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btbSponsor_Click(object sender, RoutedEventArgs e)
        {
            int index = getCurrentIndex();
            Sponsor aux = animalList[index].Sponsor;

            this.IsEnabled = false;
            sponsorWindow = new SponsorDetails(this, aux);
            sponsorWindow.Show();

        }

        private void btbVideo_Click(object sender, RoutedEventArgs e)
        {
            int index = getCurrentIndex();
            Animal selectedAnimal = animalList[index];

            this.IsEnabled = false;
            videoWindow = new VideoWindow(this, selectedAnimal);
            videoWindow.Show();
        }
    }
}
