// This file contains all the db operations that can be used by multiple files  of the user 



       namespace Chat_Application.src.Controllers.Users
{
   
    public static class CommonUserOperations
        {
            public class UserResponseDTO
            {
                public string Username { get; set; }
                public string Email { get; set; }
                public string UserID { get; set; }
                public string ProfilePicture { get; set; }
                public string ProfileID { get; set; }
                public string CreatedAt { get; set; }
            }

            public class UserResponse : UserResponseDTO
            {
                public string Password { get; set; }
            }

            public static async Task<IEnumerable<UserResponseDTO>> FindUserbyUsername(AppDbContext context, string username, CancellationToken cancellationToken)
            {
                var users = await (from Users in context.Users
                                   join Profile in context.Profiles on Users.Id equals Profile.Userid
                                   where Users.Username.Contains(username)
                                   select new UserResponseDTO
                                   {
                                       Username = Users.Username,
                                       Email = Users.Email,
                                       UserID = Users.Id.ToString(),
                                       ProfilePicture = Profile.Picture,
                                       ProfileID = Profile.Id.ToString(),
                                       CreatedAt = Users.CreatedAt.ToString()
                                   }).ToListAsync(cancellationToken);

                return users;
            }

            public static async Task<UserResponse> FindUserbyEmail(AppDbContext context, string email, CancellationToken cancellationToken)
            {
                return await (from Users in context.Users
                              join Profile in context.Profiles on Users.Id equals Profile.Userid
                              where Users.Email == email.ToLower()
                              select new UserResponse
                              {
                                  Username = Users.Username,
                                  Email = Users.Email,
                                  UserID = Users.Id.ToString(),
                                  ProfilePicture = Profile.Picture,
                                  ProfileID = Profile.Id.ToString(),
                                  CreatedAt = Users.CreatedAt.ToString(),
                                  Password = Users.Password
                              }).FirstOrDefaultAsync(cancellationToken);
            }
        }
}

    
