namespace User.Domain;

public class AppUser
{
    public string Name { get; }
    public string Surname { get; }
    public string Username { get; }
    public Guid Id { get; set; }

    public AppUser(Guid id, string name, string surname, string username)
    {
        Id = id;
        Name = name;
        Surname = surname;
        Username = username;
    }
}