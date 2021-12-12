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
        public ObservableCollection<Volunteer> volunteerList;
        public List<Sponsor> sponsorList;
        private int imgIndex = 0;
        bool first = true;
        bool addAction = false;
        bool deleteAction = false;
        int sizeList = 0;
        private Window exitWindow;
        private Window addWindow;
        private Window editWindow;
        private Window sponsorWindow;
        private Window videoWindow;
        bool animalTab = false;
        bool volunteerTab = false;
        bool partnerTab = false;

        public Management(String user)
        {
            InitializeComponent();
            lblUser.Content = user;
            lblLastDate.Content = File.ReadAllText("datetime.txt");

            animalList = LoadContentAnimalsXML();
            sponsorList = LoadContentSponsorXML();
            volunteerList = LoadContentVolunteerXML();

            addSponsor();

            DataContext = animalList;
        }

        private void addSponsor() {
            for (int i = 0; i < animalList.Count; i++) {
                animalList[i].Sponsor = sponsorList[i];
            }
        }

        private ObservableCollection<Volunteer> LoadContentVolunteerXML()
        {
            ObservableCollection<Volunteer> list = new ObservableCollection<Volunteer>();
            // Cargar contenido de prueba
            XmlDocument doc = new XmlDocument();
            var fichero = Application.GetResourceStream(new Uri("Data/volunteers.xml", UriKind.Relative));
            doc.Load(fichero.Stream);
            foreach (XmlNode node in doc.DocumentElement.ChildNodes)
            {
                var newVolunteer = new Volunteer("","",0,"",0,null, "", "","", 0);

                newVolunteer.Firstname = node.Attributes["Firstname"].Value;
                newVolunteer.Lastname = node.Attributes["Lastname"].Value;
                newVolunteer.Dni = Convert.ToInt32(node.Attributes["Dni"].Value);
                newVolunteer.Email = node.Attributes["Email"].Value;
                newVolunteer.PhoneNumber = Convert.ToInt32(node.Attributes["PhoneNumber"].Value);
                newVolunteer.Studies = node.Attributes["Studies"].Value;
                newVolunteer.TimeAvailability = node.Attributes["TimeAvailability"].Value;
                newVolunteer.ZoneAvailability = node.Attributes["ZoneAvailability"].Value;
                newVolunteer.Photo = new Uri(node.Attributes["Photo"].Value, UriKind.RelativeOrAbsolute);
                newVolunteer.Age = Convert.ToInt32(node.Attributes["Age"].Value);

                list.Add(newVolunteer);
            }

            return list;
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

            if (lblName.Content == null)
            {
                index = 0;
            }
            else {
                for (int i = 0; i < animalList.Count; i++)
                {

                    if (lblName.Content.ToString() == animalList[i].Name)
                    {
                        index = i;
                        break;
                    }

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
            fixImageDisplay(getCurrentIndex());
        }

        private void deleteAnimal() {
            if (animalList.Count == 0)
            {
                MessageBox.Show("No existen animales en la lista", "Error al eliminar animal", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else
            {
                deleteAction = true;
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
                        fixImageDisplay(getCurrentIndex());
                    }
                    else
                    {
                        imgPicture.Visibility = Visibility.Hidden;
                    }
                }

                if (animalList.Count == 0) {
                    disableAnimalsButtons();
                }
            }
            deleteAction = false;
        }

        private void btnEliminar_Click(object sender, RoutedEventArgs e)
        {

            switch (((TabItem)tcPestanas.SelectedItem).Header.ToString())
            {
                case "Animales":
                    deleteAnimal();
                    break;
                case "Socios":
                    break;
                case "Voluntarios":
                    break;

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
                    addWindow = new AddAnimal(this, animalList);
                    addWindow.Show();
                    break;
                case "Voluntarios":
                    this.IsEnabled = false;
                    addAction = true;
                    addWindow = new AddVolunteer(this, volunteerList);
                    addWindow.Show();
                    break;
                case "Socios":
                    break;
            }
        }

        private void disableAnimalsButtons() {
            btbNextImage.IsEnabled = false;
            btbPreviousImage.IsEnabled = false;
            btbSponsor.IsEnabled = false;
            btbVideo.IsEnabled = false;
            lblSex.Content = "";
            lblSociableChildren.Content = "";
            lblPpp.Content = "";
            lblSociableDogs.Content = "";
            lblSterilized.Content = "";
        }

        private void enableAnimalsButtons()
        {
            btbNextImage.IsEnabled = true;
            btbPreviousImage.IsEnabled = true;
            btbSponsor.IsEnabled = true;
            btbVideo.IsEnabled = true;
        }

        private void tcPestanas_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (!first)
            {
                switch (((TabItem)tcPestanas.SelectedItem).Header.ToString())
                {
                    case "Animales":
                        if (!animalTab)
                        {
                            
                            btnAdd.IsEnabled = true;
                            btnEdit.IsEnabled = true;
                            btnDelete.IsEnabled = true;
                            btnAdd.ToolTip = "Añadir animal";
                            btnEdit.ToolTip = "Editar animal";
                            btnDelete.ToolTip = "Eliminar animal";
                            DataContext = animalList;
                            if (animalList.Count != 0)
                            {
                                enableAnimalsButtons();
                                lstListaAnimales.SelectedItem = lstListaAnimales.Items[0];
                                lstListaAnimales.ScrollIntoView(lstListaAnimales.Items[0]);
                                if (animalList[0].Video == null) {
                                        btbSponsor.IsEnabled = false;
                                        btbVideo.IsEnabled = false;
                                }
                            }
                            else {
                                disableAnimalsButtons();
                            }
                            
                            animalTab = true;
                            volunteerTab = false;
                            partnerTab = false;
                        }
                        
                        break;
                    case "Voluntarios":
                        if (!volunteerTab) 
                        {
                            
                            btnAdd.IsEnabled = true;
                            btnEdit.IsEnabled = true;
                            btnDelete.IsEnabled = true;
                            btnAdd.ToolTip = "Añadir voluntario";
                            btnEdit.ToolTip = "Editar voluntario";
                            btnDelete.ToolTip = "Eliminar voluntario";
                            DataContext = volunteerList;
                            animalTab = false;
                            volunteerTab = true;
                            partnerTab = false;
                        }
                        break;

                    case "Socios":
                        if (!partnerTab) 
                        {
                           
                            btnAdd.IsEnabled = true;
                            btnEdit.IsEnabled = true;
                            btnDelete.IsEnabled = true;
                            btnAdd.ToolTip = "Añadir socio";
                            btnEdit.ToolTip = "Editar socio";
                            btnDelete.ToolTip = "Eliminar socio";
                            animalTab = false;
                            volunteerTab = false;
                            partnerTab = true;
                        }
                        
                        break;
                    case "Ayuda":
                        btnAdd.IsEnabled = false;
                        btnEdit.IsEnabled = false;
                        btnDelete.IsEnabled = false;
                        animalTab = false;
                        volunteerTab = false;
                        partnerTab = false;
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

        private void fixImageDisplay(int index)
        {
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
                fixImageDisplay(getCurrentIndex());
            }
        }

        private void updateAnimalsDetails(int index)
        {

            if (animalList.Count != 0)
            {
                Animal aux = animalList[index];
                lblSex.Content = aux.Sex;
                lblPpp.Content = aux.Ppp;
                lblSociableChildren.Content = aux.SociableChildren;
                lblSociableDogs.Content = aux.SociableDogs;
                lblSterilized.Content = aux.Sterilized;
            }

            
        }

        private void Window_IsEnabledChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (this.IsEnabled && addAction) {

                switch (((TabItem)tcPestanas.SelectedItem).Header.ToString())
                {
                    case "Animales":
                        if (sizeList != animalList.Count)
                        {
                            sizeList = animalList.Count;
                            addAction = false;
                            int size = lstListaAnimales.Items.Count;
                            lstListaAnimales.SelectedItem = lstListaAnimales.Items[size - 1];
                            lstListaAnimales.ScrollIntoView(lstListaAnimales.Items[size - 1]);
                        }
                        updateAnimalsDetails(getCurrentIndex());
                        fixImageDisplay(getCurrentIndex());
                        break;
                    case "Voluntarios":
                        break;
                    case "Socios":
                        break;

                }

            }
        }

        private void btnEdit_Click(object sender, RoutedEventArgs e)
        {
            switch (((TabItem)tcPestanas.SelectedItem).Header.ToString())
            {
                case "Animales":
                    if (animalList.Count == 0)
                    {
                        MessageBox.Show("No existen animales en la lista", "Error al editar animal", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                    else
                    {
                        this.IsEnabled = false;
                        addAction = true;
                        sizeList = animalList.Count;
                        int index = getCurrentIndex();
                        editWindow = new EditAnimal(this, animalList[index]);
                        editWindow.Show();
                    }
                    break;
                case "Voluntarios":
                    break;
                case "Socios":
                    break;
            }
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

        private void lstListaAnimales_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (((TabItem)tcPestanas.SelectedItem).Header.ToString() == "Animales" && !deleteAction) {
                if (animalTab)
                {
                    updateAnimalsDetails(getCurrentIndex());
                    fixImageDisplay(getCurrentIndex());
                }
                else
                {
                    updateAnimalsDetails(0);
                    fixImageDisplay(0);
                }
            }
            
        }   
    }
}
