using Clean_E_Commerce_Project.API.DTOs.UserDTOs;
using Clean_E_Commerce_Project.Core.Models;
using Clean_E_Commerce_Project.Infrastructure.Repositories.UsersRepos;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Clean_E_Commerce_Project.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly ITokenRepository tokenRepository;

        public AuthController(UserManager<ApplicationUser> userManager, ITokenRepository tokenRepository)
        {
            this.userManager = userManager;
            this.tokenRepository = tokenRepository;
        }

        [HttpPost]
        [Route("Register")]

        public async Task<IActionResult> Register([FromBody] RegisterDto registerRequestDto)
         {
            var IdentityUser = new ApplicationUser()
            {
                UserName = registerRequestDto.Username,
                Email = registerRequestDto.Email,
            };


            var IdentityResult = await userManager.CreateAsync(IdentityUser, registerRequestDto.Password);

            if (IdentityResult.Succeeded)
            {
                if (registerRequestDto.Roles != null && registerRequestDto.Roles.Any())
                {
                    IdentityResult = await userManager.AddToRolesAsync(IdentityUser, registerRequestDto.Roles);

                    if (IdentityResult.Succeeded)
                    {
                        return Ok("User registered successfully");
                    }
                    else
                    {
                        return BadRequest(IdentityResult.Errors);
                    }
                }
            }
            
                return BadRequest(IdentityResult.Errors);
            

        }


        [HttpPost]
        public async Task<IActionResult> Login([FromBody] LoginDto loginRequestDto)
        {
            var user = await userManager.FindByNameAsync(loginRequestDto.Username);

            if (user != null)
            {
                var checkPasswordResult = await userManager.CheckPasswordAsync(user, loginRequestDto.Password);

                if (checkPasswordResult)
                {

                    //Get roles for this user 
                    var roles = await userManager.GetRolesAsync(user);
                    if (roles != null)
                    {
                        var jwtToken = tokenRepository.CreateJWTToken(user, roles.ToList());

                        var response = new LoginResponseDto()
                        {
                            JwtToken = jwtToken,
                        };
                        return Ok(response);
                    }
                }
            }

            return BadRequest("Username or password is incorrect!");
        }
    }
}
