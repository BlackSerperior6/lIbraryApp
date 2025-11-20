using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryApplication.Structs
{
    public struct Book
    {
        public string Title;
        public string Author;
        public DateTime ReleasedDate;

        public Book(string title, string author, DateTime releaseDate) 
        { 
            Title = title;
            Author = author;
            ReleasedDate = releaseDate;
        }
    }
}
