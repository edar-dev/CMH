using User.Domain;

namespace User.Requests;

public record CreateAppUserRequest(string Name, string Surname, string Username)
{
    public AppUser ToDto()
    {
        return new AppUser(Guid.NewGuid(), Name, Surname, Username);
    }
}