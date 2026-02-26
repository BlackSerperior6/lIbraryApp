namespace LibraryAppWeb.Features;

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