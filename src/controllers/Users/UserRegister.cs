

namespace Chat_Application.src.Controllers.Auth
{



    public static class UserRegister
    {

        public record UserRegisterRequest (string Username , string Email , string Password );

        public class UserRegisterValidator : AbstractValidator<UserRegisterRequest>
        {

            public UserRegisterValidator()
            {
                RuleFor(x => x.Username.Trim())
                .NotEmpty().WithMessage("The Username cannot be null or empty")
                .MinimumLength(5).WithMessage("The minimum length of the message should be 5");


                RuleFor(x => x.Email)
                .EmailAddress().WithMessage("The email must be valid email address ")
            .NotEmpty().WithMessage("The email cannot be empty");

                RuleFor(x => x.Password)
            .MinimumLength(10).WithMessage("The password should be at least length 8")
            .NotEmpty().WithMessage("The password cannot be empty")
            .NotNull().WithMessage("The password cannot be null");
            }


        }


        // public static async Task<Results<Ok<SuccessFulResponse>>, NotFound<string>, StatusCodeHttpResult<ErrorResponse>> RegisterUserAsync(
        //     UserRegisterRequest request, 
        //     AppDbContext dbContext, 
        //     CancellationToken cancellationToken

        // )
        // {

        //     try
        //     {
            


        //     }
        //     catch (TaskCanceledException token)
        //     {
        //         Log.Error("The processs was cancelled by the user ");

        //     }

        //     catch (System.Exception error)
        //     {
        //         Log.Error($"Error in registering the user  {error.Message}");
        //         return TypedResults.InternalServerError(new ErrorResponse(false, error.Message));

        //     }

        // }



            
        


        private static async Task RegisterUserDB(UserRegisterRequest  request , AppDbContext context)
        {
            
        }


    }





}