using Chat_Application.DTOS.Request;
using Chat_Application.Services;
using Chat_Application.DTOS.Response;
using Microsoft.AspNetCore.Mvc;
using Chat_Application.Models;  

namespace Chat_Application.Controllers
{

    [ApiController]
    [Route("/users")]
    public class UserController : Controller
    {
        private readonly IUserService _service;
        private readonly ILogger<UserController> _logger;
        private readonly IAuthService _authService;

        public UserController(IUserService service, ILogger<UserController> logger, IAuthService authService)
        {
            _service = service;
            _logger = logger;
            _authService = authService;
        }

        //[HttpPost("register")]
        public async Task<ActionResult<ApiResponse>> UserRegister(UserRegisterDTO request)
        {
            try
            {
                _logger.LogInformation("User registration started");

                if (!ModelState.IsValid)
                {
                    _logger.LogWarning("Invalid model state for user registration");
                    return BadRequest(new ApiResponse(false, "Invalid model state", ModelState));
                }


                // Check if the user already exists 

                var ExistingUser = await _service.UserExistsByEmailAsync(request.Email);

                if (ExistingUser)
                {
                    _logger.LogWarning("User with email {Email} already exists", request.Email);
                    return BadRequest(new ApiResponse(false, "User with same mail already exists"));
                }

                
                var newUser = new User
                {
                    Username = request.Username,
                    Email = request.Email,
                    PasswordHash= _authService.HashPasswordAsync(request.Password)
                };

                // Add the new user to the database
                await _service.AddUserAsync(newUser);   

                _logger.LogInformation("User {Username} registered successfully", request.Username);

                return Ok(new ApiResponse(true, "User registered successfully"));  

            }
            catch (Exception error)
            {
                    _logger.LogError(error, "An error occurred while registering the user.");
                    return StatusCode(500, new ApiResponse(false, "Internal Server Error ", error));
            }
        }


        [HttpPost("login")]
        public async Task<ActionResult<ApiResponse>> UserLogin(UserLoginDTO request)
        {
            try
            {
                _logger.LogInformation("User login started");
                if (!ModelState.IsValid)
                {
                    _logger.LogWarning("Invalid model state for user login");
                    return BadRequest(new ApiResponse(false, "Invalid model state", ModelState));
                }

                // Check if the user exists by email    

                var user = await _service.FindUserByEmailAsync(request.Email);

                if (user == null)
                {
                   _logger.LogWarning("User with email {Email} not found", request.Email);
                    return NotFound(new ApiResponse(false, "Invalid Credentials"));
                }

                // checking the password
                var isPasswordValid = _authService.VerifyPasswordAsync(request.Password, user.PasswordHash);
                if (!isPasswordValid)
                {
                    _logger.LogWarning("Invalid password for user with email {Email}", request.Email);
                    return Unauthorized(new ApiResponse(false, "Invalid Credentials"));
                }

                // Generate JWT token
                var RefreshToken = _authService.GenerateRefreshToken(user);
                user.RefreshToken = RefreshToken;
                user.RefreshTokenExpiresAt = DateTime.UtcNow.AddDays(7);
                await _service.UpdateUserAsync(user);

                var AccessToken = _authService.GenerateAccessToken(user);

                _logger.LogInformation("User {Username} logged in successfully", user.Username);


                return Ok(new ApiResponse(true, "User Login Successful", new
                {
                    user = new UserResponseDTO(user),
                    refreshtoken = RefreshToken,
                    accessToken = AccessToken

                }));


            }
            catch (Exception error)
            {
                _logger.LogError(error, "An error occurred while logging in  the user.");
                return StatusCode(500, new ApiResponse(false, "Internal Server Error ", error));
            }
        }

    }
}
