using marketplace.Interfaces;
using marketplace.Models;
using marketplace.Resources;
using marketplace.Responses;
using marketplace.Services;
using marketplace.Utilities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text.Unicode;
using System.Threading;

namespace marketplace.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        public static User user = new User();
        private readonly IUserService userService;

        public AuthController(IUserService userService)
        {
            this.userService = userService;
        }

        [HttpPost("Register")]
        public async Task<ActionResult<User>> Register([FromBody] RegisterResource resource, CancellationToken cancellationToken)
        {
            var errors = new ErrorResponse();
            try
            {
                #region Validations
                if (!resource.Email.IsValidEmail())
                    errors.Messages.Add("Invalid Email.");
                else if (await userService.IsExistingEmail(resource.Email))
                    errors.Messages.Add("Email already exist.");

                if (!resource.Password.IsValidPassword())
                    errors.Messages.Add("Invalid Password.");

                if (errors.Messages.Any())
                    return BadRequest(errors);
                #endregion

                var response = await userService.Register(resource, cancellationToken);
                return Ok(response);
            }
            catch (Exception e)
            {
                errors.Messages.Add(e.Message);
                return BadRequest(errors);
            }
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody] LoginResource resource, CancellationToken cancellationToken)
        {
            var errors = new ErrorResponse();
            try
            {
                var response = await userService.Login(resource, cancellationToken);
                return Ok(response);
            }
            catch (Exception e)
            {
                errors.Messages.Add(e.Message);
                return BadRequest(errors);
            }
        }
    }
}
