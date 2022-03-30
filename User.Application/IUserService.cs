using User.Domain;
using User.Persistence;

namespace User.Application;

public interface IUserService
{
    void Create(AppUser appUser);

    AppUser Get(Guid id);

    void Update(AppUser appUser);

    void Delete(Guid id);
}

public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;

    public UserService(IUserRepository userRepository)
    {
        _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
    }

    public void Create(AppUser appUser)
    {
        _userRepository.Create(appUser);
    }

    public AppUser Get(Guid id)
    {
        return _userRepository.Get(id);
    }

    public void Update(AppUser appUser)
    {
        _userRepository.Update(appUser);
    }

    public void Delete(Guid id)
    {
        _userRepository.Delete(id);
    }
}