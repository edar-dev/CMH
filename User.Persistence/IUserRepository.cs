using User.Domain;

namespace User.Persistence;

public interface IUserRepository
{
    void Create(AppUser user);

    AppUser Get(Guid userId);

    void Update(AppUser user);

    void Delete(Guid id);
}

public class UserRepository : IUserRepository
{
    private readonly IDictionary<Guid, AppUser> _users;

    public UserRepository()
    {
        _users = new Dictionary<Guid, AppUser>();
    }

    public void Create(AppUser user)
    {
        _users.Add(user.Id, user);
    }

    public AppUser Get(Guid userId)
    {
        return _users[userId];
    }

    public void Update(AppUser user)
    {
        _users[user.Id] = user;
    }

    public void Delete(Guid id)
    {
        _users.Remove(id);
    }
}