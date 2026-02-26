using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryApplication.Structs
{
    public class Reader
    {
        public string LastName;
        public string FirstName;
        public string Patronymic;
        public DateTime IssuedDate;
        public DateTime BirthDate;

        public Reader(string lastName, string firstName, string patronymic, DateTime issuedDate, DateTime birthDate)
        {
            LastName = lastName;
            FirstName = firstName;
            Patronymic = patronymic;
            IssuedDate = issuedDate;
            BirthDate = birthDate;
        }
    }
}
