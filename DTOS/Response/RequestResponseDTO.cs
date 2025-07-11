using Chat_Application.Models;

namespace Chat_Application.DTOS.Response
{
    public class RequestResponseDTO
    {

        public Guid Guid { get; set; }

        public RequestResponseDTO(FriendRequest request)
        {
            Guid = request.Id;
        }
    }
}
