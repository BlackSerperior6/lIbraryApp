using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryApplication.Structs
{
    public class Book
    {
        public string Title;
        public string Author;
        public DateTime ArrivalDate;
        public DateTime ReleasedDate;

        public Book(string title, string author, DateTime arrivalDate, DateTime releaseDate) 
        { 
            Title = title;
            Author = author;
            ArrivalDate = arrivalDate;
            ReleasedDate = releaseDate;
        }
    }
}
