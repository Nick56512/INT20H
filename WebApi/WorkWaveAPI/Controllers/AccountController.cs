using DAL.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using WorkWaveAPI.ApiConfig;
using WorkWaveAPI.ApiRequestModels;

namespace WorkWaveAPI.Controllers
{
    [ApiController]
    [Route("api/v2/[controller]")]
    public class AccountController : Controller
    {
        UserManager<User> userManager;
        SignInManager<User> signInManager;
        public AccountController(UserManager<User> userManager,SignInManager<User> signInManager)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
        }
        [HttpPost]
        [Route("/registration")]
        public async Task<ActionResult> UserRegistration([FromBody] RegistrationModel model)
        {
            if (ModelState.IsValid)
            {
                UserDto user = new UserDto
                {
                    Email = model.Email,
                    Name = model.Name,
                    Lastname = model.Lastname
                };
                var result = await userManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    return Ok();
                }
                else
                {
                    foreach (var item in result.Errors)
                    {
                        ModelState.AddModelError("", item.Description);
                    }
                }
            }
            return BadRequest(ModelState);
        }

        [HttpPost]
        [Route("/login")]
        public async Task<ActionResult> Token([FromBody] LoginModel model)
        {
            if (ModelState.IsValid)
            {
                User user = await userManager.FindByNameAsync(model.Login);
                var res = await signInManager.PasswordSignInAsync(user, model.Password, false, false);
                if (res.Succeeded)
                {
                    var claims = new Claim[] { new Claim(ClaimTypes.Name, user.UserName) };

                    var timeNow = DateTime.Now;
                    var token = new JwtSecurityToken
                    (
                        issuer: AuthOptions.ISSUER,
                        audience: AuthOptions.AUDIENCE,
                        notBefore: timeNow,
                        claims: claims,
                        expires: timeNow.Add(TimeSpan.FromMinutes(AuthOptions.LIFETIME)),
                        signingCredentials: new SigningCredentials(AuthOptions.GetSymetricKey(), SecurityAlgorithms.HmacSha256)
                    );
                    var encodedJwt = new JwtSecurityTokenHandler().WriteToken(token);
                    var response = new
                    {
                        access_token = encodedJwt,
                        name = user.Name,
                        lastname = user.Lastname,
                        email = user.Email,
                    };
                    return Json(response);
                }
            }
            return BadRequest();
        }



        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpPost("/logout")]
        public async Task<ActionResult> UserLogout()
        {
            await signInManager.SignOut();
            return NoContent();
        }
    }
}
