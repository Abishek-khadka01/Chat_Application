using System.Linq;

using Microsoft.AspNetCore.Mvc;
using Chat_Application.Models;
using Chat_Application.Services;
using Chat_Application.DTOS.Response;
namespace Chat_Application.Controllers
{
    [ApiController]
    [Route("requests")]

    public class RequestController : ControllerBase
    {
        private readonly IRequestService _requestService;
        private readonly ILogger<RequestController> _logger;
        private readonly IUserService _userService;
        public RequestController(IRequestService requestService, ILogger<RequestController> logger, IUserService userService)
        {
            _requestService = requestService;
            _logger = logger;
            _userService = userService;
        }


        [HttpPost]
        [Route("send-request")]
        public async Task<ActionResult<ApiResponse>> SendRequest([FromBody] string RecieverID)
        {
            try
            {
                _logger.LogInformation("Sending the request");

                if (string.IsNullOrEmpty(RecieverID))
                {
                    _logger.LogWarning("Enter the valid receiverid ");
                    return BadRequest(new ApiResponse(false, "Enter the valid receiver id "));
                }

                // checking if the user exists 

                var user = await _userService.UserExistsAsync(Guid.Parse(RecieverID));

                if (!user)
                {
                    _logger.LogWarning("The user with the id is not a valid user");
                    return BadRequest(new ApiResponse(false, $"The user with {RecieverID} do not exist"));
                }

                // Getting the userdetails with the help of middleware 

                var id = HttpContext.User.FindFirst("id").ToString();
                _logger.LogInformation($"The userid from the middleware is {id}");
                var userExists = await _userService.UserExistsAsync(Guid.Parse(id));

                if (!userExists)
                {
                    _logger.LogInformation("The user does not exist");
                    return BadRequest(new ApiResponse(false, "UnAuthorized"));
                }

                var newRequest = new FriendRequest
                {
                    Id = Guid.NewGuid(),
                    SenderId = Guid.Parse(id),
                    ReceiverId = Guid.Parse(RecieverID),

                };

                _requestService.AddRequest(newRequest);

                return Ok(new ApiResponse(true, "Request Send Successfully", newRequest) );
                
            



            }
            catch (System.Exception error)
            {
                _logger.LogError("Error in sending the request", error.Message);
                return StatusCode(500, new ApiResponse(false, "Internal Server Error ", error.Message));

            }



        }

    }



}