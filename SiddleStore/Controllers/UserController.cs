using BusinessObject;
using DataAcess.Repository.User;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using DataAcess.ViewModels;
using Microsoft.AspNetCore.Authorization;
using SiddleStore.Configurations;
using SiddleStore.ExceptionFilterHandeling;

namespace SiddleStore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserRepository _userRepository;

        public UserController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        [HttpPost]
        [Route("Login", Name = "Login")]
        [TypeFilter(typeof(ExceptionFilter))]
        public async Task<IActionResult> Login([FromBody] LoginViewModel viewModel)
        {
            UserObject userLogin = await _userRepository.Login(viewModel.UserName, viewModel.Password);
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

        [Authorize(Roles = "Manager, Employee, Customer")]
        [HttpPost]
        [TypeFilter(typeof(ExceptionFilter))]
        public async Task<IActionResult> CreateAccount([FromBody] AccountViewModel viewModel)
        {
            await _userRepository.CreateAccount(viewModel);
            return Ok(new { mess = "Creating an account successfully!" });
        }

        [Authorize(Roles = "Manager")]
        [HttpPut("{id:int}")]
        [TypeFilter(typeof(ExceptionFilter))]
        public async Task<IActionResult> ActivateAccount(int id, [FromBody] bool activate)
        {
            var result = await _userRepository.AccountActivate(id, activate);
            return Ok(new { mess = "Successfully!" });
        }
    }
}
