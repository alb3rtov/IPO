using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Protectora
{
    public class Partner
    {
        public int Dni { set; get; }
        public string Firstname { set; get; }
        public string Lastname { set; get; }
        public string BankAccountNumber { set; get; }
        public int MonthlyContribution { set; get; }
        public string PaymentMethod { set; get; }
        public string Photo { set; get; }

        public Partner(int dni, string firstname, string lastname, string bankAccountNumber, int monthlyContribution, string paymentMethod, string photo) {
            Dni = dni;
            Firstname = firstname;
            Lastname = lastname;
            BankAccountNumber = bankAccountNumber;
            MonthlyContribution = monthlyContribution;
            PaymentMethod = paymentMethod;
            Photo = photo;
        }
    }
}
