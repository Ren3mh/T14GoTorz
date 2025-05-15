using System;
using Shared.Data;

namespace Shared
{
    public class Chat
    {
        public int Id { get; set; }
        public string SenderUserName { get; set; } // FK to AspNetUsers
        public string Message { get; set; }
        public DateTime SentAt { get; set; } = DateTime.UtcNow;

        public Chat()
        {
            // Default constructor
        }

        public Chat(string senderUserName, string message)
        {
            SenderUserName = senderUserName;
            Message = message;
            SentAt = DateTime.UtcNow;
        }
    }
}