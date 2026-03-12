namespace LibraryAppWeb.Features

public class DatabaseUser
{
    public string Login;
    public string Password;
    public string Role;

    public DatabaseUser(string login, 
    string password, string role)
    {
        Login = login;
        Password = password;
        Role = role;

    }
}