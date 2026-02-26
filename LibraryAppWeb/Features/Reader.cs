namespace LibraryAppWeb.Features;

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