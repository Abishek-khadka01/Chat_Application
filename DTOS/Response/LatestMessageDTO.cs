namespace Chat_Application.DTOS.Response
{
    public class LatestMessageDTO
    {

        public Guid ConversationID { get; set; }
        public bool IsGroup { get; set; }

        public string Name { get; set; }

        public string Message { get; set; }

        public DateTime SentAt { get; set; }

        public string ProfilePicture { get; set; }
        
        
    }
}