using Microsoft.AspNetCore.Identity;

namespace Clean_E_Commerce_Project.Infrastructure.Repositories.UsersRepos
{
    public interface ITokenRepository
    {
        string CreateJWTToken(IdentityUser user, List<string> roles);
    }
}
