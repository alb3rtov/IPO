using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Protectora
{
    public class Sponsor
    {
        public int Dni { set; get; }
        public string Firstname { set; get; }
        public string Lastname { set; get; }
        public int PhoneNumber { set; get; }
        public string PaymentMethod { set; get; }
        public string BankAccountNumber { set; get; }
        public int MonthlyContribution { set; get; }
        public DateTime StartSponsorship { set; get; }
        public string Picture { set; get; }

        public Sponsor(int dni, string firstname, string lastname, int phoneNumber, string paymentGateway, string bankAccountNumber, int monthlyContribution, DateTime startSponsorship, string picture)
        {
            Dni = dni;
            Firstname = firstname;
            Lastname = lastname;
            PhoneNumber = phoneNumber;
            PaymentMethod = paymentGateway;
            BankAccountNumber = bankAccountNumber;
            MonthlyContribution = monthlyContribution;
            StartSponsorship = startSponsorship;
            Picture = picture;
        }
    }
}