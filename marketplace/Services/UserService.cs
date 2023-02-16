using marketplace.Data;
using marketplace.Interfaces;
using marketplace.Models;
using marketplace.Resources;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace marketplace.Services
{
    public class UserService : IUserService
    {
        private readonly AppDBContext context;
        private readonly IConfiguration configuration;

        public UserService(AppDBContext context, IConfiguration configuration)
        {
            this.context = context;
            this.configuration = configuration;
        }

        public async Task<UserResource> Login(LoginResource resource, CancellationToken cancellationToken)
        {
            var user = await context.Users
                .FirstOrDefaultAsync(x => x.Email.Equals(resource.Email), cancellationToken);

            if (user == null)
                throw new Exception("Email does not exist.");

            if (!BCrypt.Net.BCrypt.Verify(resource.Password, user.PasswordHash))
                throw new Exception("Password did not match.");

            return new UserResource(user.Id, user.Email, CreateToken(user));
        }

        public async Task<UserResource?> Register(RegisterResource resource, CancellationToken cancellationToken)
        {
            var newUser = new User
            {
                Email = resource.Email,
                PasswordHash = BCrypt.Net.BCrypt.HashPassword(resource.Password)
            };
            var user = await context.Users.AddAsync(newUser, cancellationToken);
            await context.SaveChangesAsync(cancellationToken);
            return new UserResource(user.Entity.Id, user.Entity.Email, CreateToken(user.Entity));
        }

        public async Task<bool> IsExistingEmail(string email)
        {
            return await context.Users.AnyAsync(user => user.Email.Equals(email));
        }

        private string CreateToken(User user)
        {
            List<Claim> claims = new List<Claim>()
            {
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.Role, "Admin")
            };

            var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8
                .GetBytes(configuration.GetSection("AppSettings:Token").Value));

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.Now.AddDays(1),
                signingCredentials: creds);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
