

namespace Chat_Application.src.Controllers.Users
{


    public static partial class FindUser
    {

        public static  async Task<IResult> UserByID(AppDbContext context, string userID, CancellationToken cancellationToken)
        {
            try
            {
                Log.Information("Finding the user by id ");
                var user = await Database_FindById(context, userID, cancellationToken);

                return TypedResults.Ok(new SuccessFulResponse<CommonUserOperations.UserResponseDTO>(true, "User Found successfully", user));
            }
            catch (TaskCanceledException)
            {
                Log.Information("The Finding Userby Id Method was cancelled");
                return TypedResults.StatusCode(499);
            }
            catch (System.Exception error)
            {
                Log.Error($" Error in finding the user by id {error.Message}");
                return TypedResults.InternalServerError(new ErrorResponse(error.Message));

            }
        }

        private static async Task<CommonUserOperations.UserResponseDTO> Database_FindById(AppDbContext context, string userID, CancellationToken cancellationToken)
        {

            return await (from Users in context.Users
                          join Profile in context.Profiles
              on Users.Id equals Profile.Userid
                          where Users.Id == Guid.Parse(userID)
                          select new CommonUserOperations.UserResponseDTO
                          (
                              Users.Username,
                              Users.Email,
                              Users.Id.ToString(),
                              Profile.Picture,
                               Profile.Id.ToString(),
                              Users.CreatedAt.ToString()

            )).FirstOrDefaultAsync(cancellationToken);

        }
        
    }


}