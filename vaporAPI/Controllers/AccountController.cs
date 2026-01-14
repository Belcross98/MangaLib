using System.Net;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using vaporAPI.Dtos.Account;
using vaporAPI.Helpers;
using vaporAPI.Interfaces.Service;
using vaporAPI.Models;


namespace vaporAPI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly UserManager<User> _userMangager;
        private readonly ITokenService _tokenService;
        private readonly SignInManager<User> _signInManager;
        public AccountController(UserManager<User> userManager, ITokenService tokenService, SignInManager<User> signInManager)
        {
            _userMangager = userManager;
            _tokenService = tokenService;
            _signInManager = signInManager;
        }
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDto loginDto)
        {
            var user = await _userMangager.FindByNameAsync(loginDto.Username);

            if (user == null)
            {
                return Unauthorized(new ApiResponse<LoginDto>(loginDto, false, "Invalid username", HttpStatusCode.Unauthorized));
            }

            var result = await _signInManager.CheckPasswordSignInAsync(user, loginDto.Password, false);

            if (!result.Succeeded)
            {
                return Unauthorized(new ApiResponse<LoginDto>(loginDto, false, "Invalid password", HttpStatusCode.Unauthorized));
            }
            return Ok(
                new ApiResponse<NewUserDto>(new NewUserDto
                {

                    Username = user.UserName!,
                    Email = user.Email!,
                    Tokens = await _tokenService.CreateToken(user)
                }, true, "User logged in successfully", HttpStatusCode.OK)
            );
        }
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterDto registerDto)
        {
            try
            {
                var user = new User
                {
                    UserName = registerDto.Username,
                    Email = registerDto.Email
                };
                var createdUser = await _userMangager.CreateAsync(user, registerDto.Password!);

                if (createdUser.Succeeded)
                {
                    var roleResult = await _userMangager.AddToRoleAsync(user, "User");

                    if (roleResult.Succeeded)
                    {
                        return Ok(new ApiResponse<NewUserDto>(
                            new NewUserDto
                            {
                                Username = user.UserName!,
                                Email = user.Email!,
                                Tokens = await _tokenService.CreateToken(user)

                            }, true, "User registered successfully", HttpStatusCode.Created)

                        );
                    }
                    else
                    {
                        string error = roleResult.Errors.Select(e => e.Description).First();
                        return StatusCode(500, new ApiResponse<RegisterDto>(registerDto, false, error, HttpStatusCode.InternalServerError));
                    }
                }
                else
                {
                    string error = createdUser.Errors.Select(e => e.Description).First();
                    return StatusCode(500, new ApiResponse<RegisterDto>(registerDto, false, error, HttpStatusCode.InternalServerError));
                }
            }
            catch (Exception e)
            {
                return StatusCode(500, new ApiResponse<Exception>(e, false, e.Message, HttpStatusCode.InternalServerError));
            }
        }

        // [HttpPost("make-admin")]
        // public async Task<IActionResult> MakeAdmin()
        // {

        //     var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

        //     if (userId == null) return Unauthorized();

        //     var user = await _userMangager.FindByIdAsync(userId);

        //     if (user == null) return NotFound();

        //     await _userMangager.AddToRoleAsync(user, "Admin");

        //     return Ok();
        // }

    }
}