using BizPilotBackEndProduction.Core.dtos;
using BizPilotBackEndProduction.Core.otherObjects;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

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

    }
}
