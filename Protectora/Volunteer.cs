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
        public DateTime TimeAvailability { get; set; }
        public string ZoneAvailability { get; set; }
        public string Studies { get; set; }
    }
}
