using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryApplication.Structs
{
    public struct Reader
    {
        public string LastName;
        public string Name;
        public string Patronymic;
        public DateTime DateTime;

        public Reader(string lastName, string name, string patronymic, DateTime dateTime) 
        {
            LastName = lastName;
            Name = name;
            Patronymic = patronymic;
            DateTime = dateTime;
        }
    }
}
