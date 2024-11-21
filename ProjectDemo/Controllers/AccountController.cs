using Application.DTOs.UserDTOs;
using Domin.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace ProjectDemo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IConfiguration _configuration;
        public AccountController(UserManager<IdentityUser> userManager, IConfiguration configuration)
        {
            _userManager = userManager;
            _configuration = configuration;
        }

        [HttpPost]
        [Route("Register")]
        public async Task<IActionResult> Register([FromForm] NewUserdto dto)
        {
            if (ModelState.IsValid)
            {
                var EmailExist = await _userManager.FindByEmailAsync(dto.Email);

                if (EmailExist != null)
                {
                    return BadRequest(new AuthResult()
                    {
                        Result = false,
                        Errors = new List<string>()
                        {
                            "Email already exist"
                        }
                    });
                }
                var User = new IdentityUser()
                {
                    Email = dto.Email,
                    UserName = dto.UserName,
                };
                var IsCreated = await _userManager.CreateAsync(User, dto.Password);

                if (IsCreated.Succeeded)
                {
                    var token = GenerateJwtToken(User);

                    return Ok(new AuthResult()
                    {
                        Token = token,
                        Result = true,
                        Errors = new List<string>() { "There is no error" }
                    });


                }
                return BadRequest(new AuthResult()
                {
                    Errors = new List<string>()
                    {
                        "Server Error"
                    },
                    Result = false
                });
            }
            return BadRequest();
        }

        [Route("Login")]
        [HttpPost]
        public async Task<IActionResult> Login([FromForm] LoginUserdto dto)
        {
            if (ModelState.IsValid)
            {
                var UserExist = await _userManager.FindByEmailAsync(dto.Email);

                if (UserExist == null)
                {
                    return BadRequest(new AuthResult()
                    {
                        Errors = new List<string> { "Invalid PayLoad" },
                        Result = false
                    });
                }

                var IsCorrect = await _userManager.CheckPasswordAsync(UserExist, dto.Password);

                if (!IsCorrect)
                {
                    return BadRequest(new AuthResult()
                    {
                        Errors = new List<string> { "Invalid Credentials" },
                        Result = false
                    });
                }

                var JwtToken = GenerateJwtToken(UserExist);

                return Ok(new AuthResult()
                {
                    Token = JwtToken,
                    Result = true,
                });


            }
            return BadRequest(new AuthResult()
            {
                Errors = new List<string> { " Invalid Payload" }
            });
        }
        private string GenerateJwtToken(IdentityUser user)
        {
            var jwttokenhandler = new JwtSecurityTokenHandler();

            var key = Encoding.UTF8.GetBytes(_configuration.GetSection("JwtConfig:Secret").Value);

            var tokendisc = new SecurityTokenDescriptor()
            {
                Subject = new System.Security.Claims.ClaimsIdentity(new[]
                {
                    new Claim("ID", user.Id),
                    new Claim(JwtRegisteredClaimNames.Sub,user.Email),
                    new Claim(JwtRegisteredClaimNames.Email,user.Email),
                    new Claim(JwtRegisteredClaimNames.Jti,Guid.NewGuid().ToString()),
                    new Claim(JwtRegisteredClaimNames.Iat,DateTime.Now.ToUniversalTime().ToString())
                }),

                Expires = DateTime.Now.AddHours(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256),
            };

            var token = jwttokenhandler.CreateToken(tokendisc);
            var jwttoken = jwttokenhandler.WriteToken(token);

            return jwttoken;
        }
    }
}

