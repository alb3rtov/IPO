using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Protectora
{
    public class Partner
    {
        private int Dni { set; get; }
        private string Firstname { set; get; }
        private string Lastname { set; get; }
        private int PhoneNumber { set; get; }
        private string BankAccountNumber { set; get; }
        private int AmountAid { set; get; }
        private string PaymentMethod { set; get; }

        public Partner(int dni, string firstname, string lastname, int phoneNumber, string bankAccountNumber, int amountAid, string paymentMethod) {
            Dni = dni;
            Firstname = firstname;
            Lastname = lastname;
            PhoneNumber = phoneNumber;
            BankAccountNumber = bankAccountNumber;
            AmountAid = amountAid;
            PaymentMethod = paymentMethod;
        }
    }
}
