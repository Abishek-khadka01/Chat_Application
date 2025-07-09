using Chat_Application.Models;

namespace Chat_Application.DTOS.Response
{
    public class UserResponseDTO
    {

        public Guid id { get; set; }    
        public string Username { get; set; } = null!;
        public string Email { get; set; } = null!;

        public string ProfilePicture { get; set; } = null!;

        public UserResponseDTO (User user)
        {
            id = user.Id;
            Username = user.Username;
            Email = user.Email;
            ProfilePicture = user.ProfilePictureUrl ?? string.Empty; // Handle null case for profile picture
        }


    }


}
