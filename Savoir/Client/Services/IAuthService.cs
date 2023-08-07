using Savoir.Client.Resources;

namespace Savoir.Client.Services
{
    public interface IAuthService
    {
        User User { get; }
        Task Initialize();
        Task<User> Login(LoginModel model);
        Task Logout();
        Task<User> GetUser();
    }
}

