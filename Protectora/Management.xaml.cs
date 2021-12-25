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
        public ObservableCollection<Partner> partnerList;
        public List<Sponsor> sponsorList;

        private int imgIndex = 0;
        private int sizeList = 0;

        bool animalTab = false;
        bool volunteerTab = false;
        bool partnerTab = false;

        private int loaded1 = 0;
        private int loaded2 = 0;
        private bool addAction = false;
        private bool deleteAction = false;
        
        private Window exitWindow;
        private Window addWindow;
        private Window editWindow;
        private Window sponsorWindow;
        private Window videoWindow;
        private Window helpWindow;

        public Management(String user)
        {
            InitializeComponent();
            lblUser.Content = user;
            lblLastDate.Content = File.ReadAllText("../../../../Data/datetime.txt");

            /* Load data from XML files */
            animalList = LoadContentAnimalsXML();
            sponsorList = LoadContentSponsorXML();
            volunteerList = LoadContentVolunteerXML();
            partnerList = LoadContentPartnerXML();

            /* Data binding */
            lstListaAnimales.ItemsSource = animalList;
            dgVolunteers.ItemsSource = volunteerList;
            lstListaSocios.ItemsSource = partnerList;

            addSponsor();
        }
            
        /* Set sponsors of animals */
        private void addSponsor() {
            for (int i = 0; i < animalList.Count; i++) {
                animalList[i].Sponsor = sponsorList[i];
            }
        }

        /* Load partnts data */
        private ObservableCollection<Partner> LoadContentPartnerXML()
        {
            ObservableCollection<Partner> list = new ObservableCollection<Partner>();

            XmlDocument doc = new XmlDocument();
            var fichero = Application.GetResourceStream(new Uri("Data/partners.xml", UriKind.Relative));
            doc.Load(fichero.Stream);
            foreach (XmlNode node in doc.DocumentElement.ChildNodes)
            {
                var newPartner = new Partner(0,"","","",0,"","");

                newPartner.Dni = Convert.ToInt32(node.Attributes["Dni"].Value);
                newPartner.Firstname = node.Attributes["Firstname"].Value;
                newPartner.Lastname = node.Attributes["Lastname"].Value;
                newPartner.Photo = node.Attributes["Photo"].Value;
                newPartner.MonthlyContribution = Convert.ToInt32(node.Attributes["MonthlyContribution"].Value);
                newPartner.PaymentMethod = node.Attributes["PaymentMethod"].Value;
                newPartner.BankAccountNumber = node.Attributes["BankAccountNumber"].Value;

                list.Add(newPartner);
            }
            return list;
        }
        /* Load volunteers data */
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

        /* Load sponsors data */
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

        /* Load animals data */
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

        /* Event of logout button */
        private void buttonLogout_Click(object sender, RoutedEventArgs e)
        {
            exitWindow = new ExitWindow();
            exitWindow.Show();
        }

        /* Returns the index of the current animal selected on the list */
        private int getCurrentIndexAnimals() {
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

        /* Returns the index of the current volunteer selected on the list */
        private int getCurrentIndexVolunteers() {
            int index = 0;

            Volunteer vol = (Volunteer)dgVolunteers.SelectedItem;
            string name = vol.Firstname;

            for (int i = 0; i < volunteerList.Count; i++) {
                if (name == volunteerList[i].Firstname) {
                    index = i;
                    break;
                }
            }

            return index;
        }

        /* Event to show previous animal image */
        private void btbPreviousImage_Click(object sender, RoutedEventArgs e)
        {
            int index = getCurrentIndexAnimals();
            int size = animalList[index].Pictures.Count;

            imgIndex--;
            if (imgIndex < 0) {
                imgIndex = imgIndex + size;
            }

            /* Use module operator to make a circular list */
            var bitmap = new BitmapImage(new Uri(animalList[index].Pictures[imgIndex % size], UriKind.RelativeOrAbsolute));

            imgPicture.Source = bitmap;
        }

        /* Event to show next animal image */
        private void btbNextImage_Click(object sender, RoutedEventArgs e)
        {
            int index = getCurrentIndexAnimals();
            int size = animalList[index].Pictures.Count;

            imgIndex++;
            if (imgIndex < 0)
            {
                imgIndex = imgIndex + size;
            }

            /* Use module operator to make a circular list */
            var bitmap = new BitmapImage(new Uri(animalList[index].Pictures[imgIndex % size], UriKind.RelativeOrAbsolute));

            imgPicture.Source = bitmap;
        }

        /* Event to detect when user clicks on the list to update images of animals */
        private void lstListaAnimales_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            fixAnimalImageDisplay(getCurrentIndexAnimals());
        }

        /* Returns the index of the current partner selected on the list */
        private int getCurrentIndexPartners() {
            int index = 0;

            if (lblFirstnameP.Content == null)
            {
                index = 0;
            }
            else
            {
                for (int i = 0; i < partnerList.Count; i++)
                {
                    if (lblFirstnameP.Content.ToString() == partnerList[i].Firstname)
                    {
                        index = i;
                        break;
                    }
                }
            }

            return index;
        }

        /* Deletes selected partner from partner list and show another partner */
        private void deletePartner() {
            if (partnerList.Count == 0)
            {
                MessageBox.Show("No existen socios en la lista", "Error al eliminar socio", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else
            {
                deleteAction = true;
                int index = getCurrentIndexPartners();

                if (MessageBox.Show("¿Desea eliminar el animal " + partnerList[index].Firstname + " " + partnerList[index].Lastname + "?", "Eliminar socio", MessageBoxButton.YesNo, MessageBoxImage.Information) == MessageBoxResult.Yes)
                {
                    partnerList.RemoveAt(index);

                    int size = lstListaSocios.Items.Count;

                    if (size != 0)
                    {
                        imgPicture1.Visibility = Visibility.Visible;
                        lstListaSocios.SelectedItem = lstListaSocios.Items[size - 1];
                        lstListaSocios.ScrollIntoView(lstListaSocios.Items[size - 1]);

                    }
                    else {
                        imgPicture1.Visibility = Visibility.Hidden;
                    }

                }
            }
            deleteAction = false;
        }

        /* Deletes selected animal from animal list and shows another animal */
        private void deleteAnimal() {
            if (animalList.Count == 0)
            {
                MessageBox.Show("No existen animales en la lista", "Error al eliminar animal", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else
            {
                deleteAction = true;
                int index = getCurrentIndexAnimals();

                if (MessageBox.Show("¿Desea eliminar el animal " + animalList[index].Name + "?", "Eliminar animal", MessageBoxButton.YesNo, MessageBoxImage.Information) == MessageBoxResult.Yes)
                {
                    animalList.RemoveAt(index);
                   
                    int size = lstListaAnimales.Items.Count;
                    
                    if (size != 0)
                    {
                        imgPicture.Visibility = Visibility.Visible;
                        lstListaAnimales.SelectedItem = lstListaAnimales.Items[size - 1];
                        lstListaAnimales.ScrollIntoView(lstListaAnimales.Items[size - 1]);
                        fixAnimalImageDisplay(getCurrentIndexAnimals());
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

        /* Deletes selected volunteer from volunteer list and shows another volunteer */
        private void deleteVolunteer() {

            if (volunteerList.Count == 0)
            {
                MessageBox.Show("No existen voluntarios en la lista", "Error al eliminar voluntario", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else {
                int index = getCurrentIndexVolunteers();

                if (MessageBox.Show("¿Desea eliminar el voluntario " + volunteerList[index].Firstname + "?", "Eliminar voluntario", MessageBoxButton.YesNo, MessageBoxImage.Information) == MessageBoxResult.Yes)
                {
                    volunteerList.RemoveAt(index);
                    
                    int size = dgVolunteers.Items.Count;

                    if (size != 0)
                    {
                        dgVolunteers.SelectedItem = dgVolunteers.Items[size - 1];
                        dgVolunteers.ScrollIntoView(dgVolunteers.Items[size - 1]);
                    }
                }
            }
        }

        /* Delete button change funcionality depending on the tab selected */
        private void btnEliminar_Click(object sender, RoutedEventArgs e)
        {
            switch (((TabItem)tcPestanas.SelectedItem).Header.ToString())
            {
                case "Animales":
                    deleteAnimal();
                    break;
                case "Voluntarios":
                    deleteVolunteer();
                    break;
                case "Socios":
                    deletePartner();
                    break;
            }           
        }

        /* Edit button change funcionality depending on the tab selected */
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
                        int index = getCurrentIndexAnimals();
                        editWindow = new EditAnimal(this, animalList[index]);
                        editWindow.Show();
                    }
                    break;
                case "Voluntarios":
                    if (volunteerList.Count == 0) {
                        MessageBox.Show("No existen voluntarios en la lista", "Error al editar voluntario", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                    else
                    {
                        this.IsEnabled = false;
                        addAction = true;
                        int index = getCurrentIndexVolunteers();
                        editWindow = new EditVolunteer(this, volunteerList[index]);
                        editWindow.Show();
                    }
                    break;
                case "Socios":
                    break;
            }
        }

        /* Add button change funcionality depending on the tab selected */
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
                    this.IsEnabled = false;
                    addAction = true;
                    addWindow = new AddPartner(this, partnerList);
                    addWindow.Show();
                    break;
            }
        }

        /* Disable animals buttons showed on details */
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

        /* Enable animals buttons showed on details */
        private void enableAnimalsButtons()
        {
            btbNextImage.IsEnabled = true;
            btbPreviousImage.IsEnabled = true;
            btbSponsor.IsEnabled = true;
            btbVideo.IsEnabled = true;
        }

        /* Change tooltip and set elements depeding on the selected tab */
        private void tcPestanas_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (loaded1 > 3)
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
                            animalTab = false;
                            volunteerTab = true;
                            partnerTab = false;

                            if (volunteerList.Count != 0) {
                                dgVolunteers.SelectedItem = dgVolunteers.Items[0];
                                dgVolunteers.ScrollIntoView(dgVolunteers.Items[0]);
                            }
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
                loaded1++;
            }
        }

        /* Before close app save the current datetime in a file */
        private void Window_Closing(object sender, CancelEventArgs e)
        {
            string datetime = "Último acceso: " + DateTime.Now.ToString("dd-MM-yyyy HH:mm");
            File.WriteAllText("../../../../Data/datetime.txt", datetime);
            Application.Current.Shutdown();
        }

        /* Set current animal images */
        private void fixAnimalImageDisplay(int index)
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

        /* Fix image also when user uses keyboard to navigate through the animal list */
        private void lstListaAnimales_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Down || e.Key == Key.Up) {
                fixAnimalImageDisplay(getCurrentIndexAnimals());
            }
        }

        /* Update animals diferent details */
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

        /* When window is enabled set correct elements depeding on the tab */
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
                        updateAnimalsDetails(getCurrentIndexAnimals());
                        fixAnimalImageDisplay(getCurrentIndexAnimals());
                        break;
                    case "Voluntarios":
                        addAction = false;
                        break;
                    case "Socios":
                        break;
                }
            }
        }

        /* Event to open sponsor window */
        private void btbSponsor_Click(object sender, RoutedEventArgs e)
        {
            int index = getCurrentIndexAnimals();
            Sponsor aux = animalList[index].Sponsor;

            this.IsEnabled = false;
            sponsorWindow = new SponsorDetails(this, aux);
            sponsorWindow.Show();

        }

        /* Event to open video window */
        private void btbVideo_Click(object sender, RoutedEventArgs e)
        {
            int index = getCurrentIndexAnimals();
            Animal selectedAnimal = animalList[index];

            this.IsEnabled = false;
            videoWindow = new VideoWindow(this, selectedAnimal);
            videoWindow.Show();
        }

        /* Fix element display when diffent animal is selected */
        private void lstListaAnimales_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (loaded2 > 3)
            {
                if (((TabItem)tcPestanas.SelectedItem).Header.ToString() == "Animales" && !deleteAction)
                {
                    if (animalTab)
                    {
                        updateAnimalsDetails(getCurrentIndexAnimals());
                        fixAnimalImageDisplay(getCurrentIndexAnimals());
                    }
                    else
                    {
                        updateAnimalsDetails(0);
                        fixAnimalImageDisplay(0);
                    }
                }
            }
            else {
                loaded2++;
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.IsEnabled = false;
            helpWindow = new VersionWindow(this);
            helpWindow.Show();
        }
    }
}
