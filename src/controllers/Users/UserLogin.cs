namespace Chat_Application.src.Controllers.Users
{


    public static class UserLogin
    {
        public record UserLoginRequest(string Email, string Password);

        public record UserLoginResponse(string AccessToken, CommonUserOperations.UserResponseDTO UserData);

        public class UserLoginRequestValidator : AbstractValidator<UserLoginRequest>
        {

            public UserLoginRequestValidator()
            {
                RuleFor(x => x.Email)
                .EmailAddress().WithMessage("Enter the proper mail");

                RuleFor(x => x.Password)
                .NotEmpty().WithMessage("The password cannot be empty")
                .NotNull().WithMessage("The password cannot be null");

            }

        }



        public static async Task<IResult> LoginAsync(AppDbContext dbContext, UserLoginRequest request, CancellationToken cancellationToken)
        {
            try
            {
                Log.Information("User Login EndPoint hit successfully");
                CommonUserOperations.UserResponse findUser = await CommonUserOperations.FindUserbyEmail(dbContext, request.Email, cancellationToken);

                if (findUser == null)
                {
                    Log.Information("The user does not exist ");
                    return TypedResults.NotFound(new ErrorResponse("Invalid credentials"));
                }

                // TODO: Replace with hashed password verification
                if (request.Password != findUser.Password)
                {
                    Log.Warning("The user password did not match properly ");
                    return TypedResults.BadRequest(new ErrorResponse("Invalid credentials "));
                }

                // TODO:  Create   access token
                var accessToken = "accesstoken";

                var response = new UserLoginResponse(
                    accessToken,
                    new CommonUserOperations.UserResponseDTO
                    {
                        Username = findUser.Username,
                        Email = findUser.Email,
                        UserID = findUser.UserID,
                        ProfilePicture = findUser.ProfilePicture,
                        ProfileID = findUser.ProfileID,
                        CreatedAt = findUser.CreatedAt
                    });

                return TypedResults.Ok(new SuccessFulResponse<UserLoginResponse>(true, "User Login Successful", response));
            }
            catch (TaskCanceledException)
            {
                Log.Warning("Cancellation token was invoked in login endpoint");
                return TypedResults.StatusCode(499);
            }
            catch (System.Exception error)
            {
                Log.Information($"Error in logging in the user {error.Message}");
                return TypedResults.InternalServerError(new ErrorResponse(error.Message));
            }
        }




    }


}