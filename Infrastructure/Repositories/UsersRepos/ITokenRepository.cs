using Clean_E_Commerce_Project.Core.Models;
using Microsoft.AspNetCore.Identity;

namespace Clean_E_Commerce_Project.Infrastructure.Repositories.UsersRepos
{
    public interface ITokenRepository
    {
        string CreateJWTToken(ApplicationUser user, List<string> roles);
    }
}
