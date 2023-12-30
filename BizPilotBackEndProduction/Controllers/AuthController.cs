using BizPilotBackEndProduction.Core.dtos;
using BizPilotBackEndProduction.Core.otherObjects;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace BizPilotBackEndProduction.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {

        private readonly UserManager<IdentityUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IConfiguration _configuration;

        public AuthController(UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager, IConfiguration configuration)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _configuration = configuration;
        }




        [HttpPost]
        [Route("seed-roles")]

        public async Task<IActionResult> SeedRoles()
        {

            bool isOwnerRoleExists = await _roleManager.RoleExistsAsync(StaticUserRoles.OWNER);
            bool isAdminRoleExists = await _roleManager.RoleExistsAsync(StaticUserRoles.ADMIN);
            bool isUserRoleExists = await _roleManager.RoleExistsAsync(StaticUserRoles.USER);


            if (isAdminRoleExists && isUserRoleExists && isOwnerRoleExists)
                return Ok("Roles seeding is already done");
            



            await _roleManager.CreateAsync(new IdentityRole(StaticUserRoles.USER));
            await _roleManager.CreateAsync(new IdentityRole(StaticUserRoles.OWNER));
            await _roleManager.CreateAsync(new IdentityRole(StaticUserRoles.ADMIN));

            return Ok("Role seeding done successfully");


        }


        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> Register([FromBody] RegisterDto registerDto)
        {
            var isExistsUser = await _userManager.FindByNameAsync(registerDto.UserName);

            if (isExistsUser != null)
                return BadRequest("UserName already Exists");

            IdentityUser newUser = new IdentityUser()
            {
                Email = registerDto.Email,
                    UserName =registerDto.UserName,
                    SecurityStamp = Guid.NewGuid().ToString(),
            };

            var createUserResult = await _userManager.CreateAsync(newUser , registerDto.Password);

            if(!createUserResult.Succeeded)
            {
                var errorString = "User  creation failed because : ";
                foreach(var error in createUserResult.Errors)
                {
                    errorString += "#" + error.Description;

                }

                return BadRequest(errorString);
            }

            //Add a default user role to all users
            await _userManager.AddToRoleAsync(newUser, StaticUserRoles.USER);
            return Ok("User created succesfully");
        }


        [HttpPost]
        [Route("Login")]

        public async Task<IActionResult> Login([FromBody] LoginDto loginDto)
        { 
        var user = await _userManager.FindByNameAsync(loginDto.UserName);

        if(user is null)
            
                return Unauthorized("Invalid Credentials");

            var isPasswordCorrect = await _userManager.CheckPasswordAsync(user, loginDto.Password);

            if (!isPasswordCorrect)
                return Unauthorized("Invalid Creadentials");

            var userRoles = await _userManager.GetRolesAsync(user);

            var authClaims = new List<Claim>
            {
            new Claim (ClaimTypes.Name , user.UserName),
            new Claim (ClaimTypes.NameIdentifier, user.Id),
            new Claim ("JWTID",Guid.NewGuid().ToString()),
            };

            foreach (var userRole in userRoles)
            {
                authClaims.Add(new Claim(ClaimTypes.Role, userRole));

            }

            var token = GenerateNewJsonWebToken(authClaims);
            return Ok(token);

        }

        private string GenerateNewJsonWebToken(List<Claim> claims)
        {

            var authSecreat = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Secret"]));
            var tokenObject = new JwtSecurityToken(

                issuer: _configuration["JWT:ValidIssuer"],
                audience:_configuration["JWT:ValidAudience"],
                expires:DateTime.Now.AddHours(1),
                claims:claims,
                signingCredentials:new SigningCredentials(authSecreat, SecurityAlgorithms.HmacSha256)
                ) ;


            string token = new JwtSecurityTokenHandler().WriteToken(tokenObject);
            return token;


        }


        //ValidateIssuer = true,
        //    ValidateAudience = true,
        //    ValidIssuer = builder.Configuration["JWT:ValidIssuer"],
        //    ValidAudience = builder.Configuration["JWT:ValidAudience"],
        //    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JWT:Secret"]))



    }
}
