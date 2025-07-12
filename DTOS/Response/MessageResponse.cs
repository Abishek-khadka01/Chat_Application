


using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;

namespace Chat_Application.DTOS.Response
{

    public class MessageResponseDTO
    {

        public string sender { get; set; }
        public string groupName { get; set; }

        public string content { get; set; }

        public Guid  userID { get; set; }

        public MessageResponseDTO(string sender
        , string GroupName,
        string content,
        Guid userID)
        {
            this.sender = sender;
            this.groupName = GroupName;
            this.content = content;
            this.userID = userID;
        }


     
    }


}