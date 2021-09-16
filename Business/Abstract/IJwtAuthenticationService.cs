using Business.Models;

namespace Business.Abstract
{
    public interface IJwtAuthenticationService
    {
        string Authenticate(User user);
    }
}
