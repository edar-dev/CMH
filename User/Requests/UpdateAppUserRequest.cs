using User.Domain;

namespace User.Requests;

public record UpdateAppUserRequest(Guid Id, string Name, string Surname, string Username)
{
    public AppUser ToDto()
    {
        return new AppUser(Id, Name, Surname, Username);
    }
}