using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Protectora
{
    public class Volunteer
    {
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public int Dni { get; set; }
        public string Email { get; set; }
        public int PhoneNumber { get; set; }
        public Uri Photo { get; set; }
        public string TimeAvailability { get; set; }
        public string ZoneAvailability { get; set; }
        public string Studies { get; set; }

        public Volunteer(string firstname, string lastname, int dni, string email, int phoneNumber, Uri photo, string timeAvailability, string zoneAvailability, string studies)
        {
            Firstname = firstname;
            Lastname = lastname;
            Dni = dni;
            Email = email;
            PhoneNumber = phoneNumber;
            Photo = photo;
            TimeAvailability = timeAvailability;
            ZoneAvailability = zoneAvailability;
            Studies = studies;
        }
    }
}
