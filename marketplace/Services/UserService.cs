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
        private readonly ICustomerService customerService;

        public UserService(AppDBContext context, IConfiguration configuration, ICustomerService customerService)
        {
            this.context = context;
            this.configuration = configuration;
            this.customerService = customerService;
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

        public async Task<RegisterResponse> Register(RegisterRequest resource, CancellationToken cancellationToken)
        {
            var newUser = new User
            {
                Email = resource.Email,
                PasswordHash = BCrypt.Net.BCrypt.HashPassword(resource.Password)
            };
            var user = await context.Users.AddAsync(newUser, cancellationToken);
            if (user is not null)
                await customerService.AddCustomer(user.Entity, cancellationToken, false);
            await context.SaveChangesAsync(cancellationToken);
            return new RegisterResponse(user.Entity.Id, user.Entity.Email, user.Entity.Role, CreateToken(user.Entity));
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
                new Claim(ClaimTypes.Role, user.Role.ToString())
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
