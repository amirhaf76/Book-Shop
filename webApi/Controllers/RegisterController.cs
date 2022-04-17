using AuthenticationPlugin;
using BookShopWebApi.Data;
using BookShopWebApi.Models;
using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace BookShopWebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class AuthController : Controller
    {

        private readonly BookShopDbContext _dbContext;
        private readonly IConfiguration _configuration;
        private readonly AuthService _auth;

        public AuthController(BookShopDbContext dbContext, IConfiguration configuration)
        {
            _dbContext = dbContext;
            _configuration = configuration;
            _auth = new AuthService(_configuration);
        }

        [HttpPost]
        public IActionResult Register([FromBody] User user)
        {
            if (_dbContext.Users.Find(user.Username) is not null)
                return BadRequest("نام کاربری تکراری می‌باشد. لطفا نام کاربری دیگری انتخاب کنید");

            user.Password = SecurePasswordHasherHelper.Hash(user.Password);

            _dbContext.Users.Add(user);
            _dbContext.SaveChanges();

            return CreatedAtAction(nameof(Register), $"User {user.Username} is created successfully.");
        }

        [HttpPost]
        public IActionResult Login([FromBody] User user)
        {
            User? db_user = _dbContext.Users.Find(user.Username);

            if (db_user is null)
                return NotFound();
            
            if (!SecurePasswordHasherHelper.Verify(user.Password, db_user.Password))
                return Unauthorized();

            var claims = new[]
             {
               new Claim(JwtRegisteredClaimNames.Email, "abc@email.com"),
               new Claim(ClaimTypes.Email, "abc@email.com"),
             };

            var token = _auth.GenerateAccessToken(claims);

            return Ok(new
            {
                access_token = token.AccessToken,
                expires_in = token.ExpiresIn,
                token_type = token.TokenType,
                creation_Time = token.ValidFrom,
                expiration_Time = token.ValidTo,
            });
        }
    }
}
