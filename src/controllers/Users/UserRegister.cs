

using chat_application.Models;

namespace Chat_Application.src.Controllers.Users
{

    public static class UserRegister
    {

        public record UserRegisterRequest (string Username , string Email , string Password );

        public class UserRegisterValidator : AbstractValidator<UserRegisterRequest>
        {

            public UserRegisterValidator()
            {
                RuleFor(x => x.Username)
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


        public static async Task<IResult> RegisterUserAsync(
            UserRegisterRequest request, 
            AppDbContext dbContext, 
            CancellationToken cancellationToken
        )
        {

            try
            {

                Log.Information("User Register EndPoint is running ");

                var UserExists = await CommonUserOperations.FindUserbyEmail(dbContext, request.Email.ToLower(), cancellationToken);

                if (UserExists != null)
                {
                    Log.Warning($"The user with the same mail already  exists  ");
                   return  TypedResults.Conflict(new ErrorResponse("User with same email already exists"));
                }


                await RegisterUserDB(request, dbContext, cancellationToken);


                return TypedResults.Ok(new SuccessFulResponse<string>(true, "User is registered successfully", ""));

            }
            catch (TaskCanceledException)
            {
                Log.Error("The processs was cancelled by the user ");
                return TypedResults.StatusCode(499);

            }

            catch (System.Exception error)
            {
                Log.Error($"Error in registering the user  {error.Message}");
                return TypedResults.InternalServerError(new ErrorResponse( error.Message));

            }

        }





        private static async Task RegisterUserDB(UserRegisterRequest request, AppDbContext context, CancellationToken cancellationToken)
        {

            var user = new User
            {
                Username = request.Username.ToLower(),
                Email = request.Email.ToLower(),
                Password = request.Password // todo : hash the password 

            };

            context.Users.Add(user);
            await context.SaveChangesAsync(cancellationToken);

        }

    }

}