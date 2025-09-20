// This file contains all the db operations that can be used by multiple files  of the user 

using System.Text.RegularExpressions;

namespace Chat_Application.src.Controllers.Users
{

    
    public static class CommonUserOperations
    {
        public record UserResponseDTO(string Username, string Email, string UserID, string ProfilePicture, string ProfileID, string CreatedAt);



        public static async Task<IEnumerable<UserResponseDTO>> FindUserbyUsername(AppDbContext context, string username, CancellationToken cancellationToken)
        {

            var users = await (from Users in context.Users
                               join Profile in context.Profiles
                   on Users.Id equals Profile.Userid
                               where Users.Username.Contains(username)
                               select new UserResponseDTO(
                                   Users.Username,
                                   Users.Email,
                                   Users.Id.ToString(),
                                   Profile.Picture,
                                   Profile.Id.ToString(),
                                   Users.CreatedAt.ToString())


                        ).ToListAsync(cancellationToken);



            return users;

        }



        public static async Task<UserResponseDTO> FindUserbyEmail(AppDbContext context, string email, CancellationToken cancellationToken)
        {
            return await (from Users in context.Users
                          join Profile in context.Profiles
              on Users.Id equals Profile.Userid
                          where Users.Email == email
                          select new UserResponseDTO(
                              Users.Username,
                              Users.Email,
                              Users.Id.ToString(),
                              Profile.Picture,
                              Profile.Id.ToString(),
                              Users.CreatedAt.ToString())


                     ).FirstAsync(cancellationToken);

        }
 

    }
}