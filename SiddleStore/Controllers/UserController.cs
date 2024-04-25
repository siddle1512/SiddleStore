using BusinessObject;
using DataAcess.Repository.User;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using DataAcess.ViewModels;

namespace SiddleStore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        public IUserRepository userRepository;

        public UserController()
        {
            userRepository = new UserRepository();
        }

        [HttpPost("Login")]
        public IActionResult Login(string username, string password)
        {
            UserObject userLogin = userRepository.Login(username, password);
            if (userLogin == null)
            {
                return Unauthorized();
            }

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, userLogin.UserId.ToString()),
                new Claim(ClaimTypes.Role, userLogin.Role.RoleName.ToString()),
             };

            var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config.AppSetting["JWT:Secret"]));
            var signinCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);
            var tokeOptions = new JwtSecurityToken(
                issuer: config.AppSetting["JWT:ValidIssuer"],
                audience: config.AppSetting["JWT:ValidAudience"],
                claims: claims,
                expires: DateTime.Now.AddMinutes(60),
                signingCredentials: signinCredentials
            );
            var tokenString = new JwtSecurityTokenHandler().WriteToken(tokeOptions);
            return Ok(new { Token = tokenString });
        }

        [HttpPost("CreateAccount")]
        public IActionResult CreateAccount(AccountViewModel viewModel)
        {
            try
            {
                userRepository.CreateAccount(viewModel);              
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return Ok(new { mess = "Creating an account successfully!" });
        }

        [HttpPut("ActivateAccount")]
        public IActionResult ActivateAccount(int userId, bool activate)
        {
            try
            {
                userRepository.AccountActivate(userId, activate);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return Ok(new { mess = "Successfully!" });
        }
    }
}
