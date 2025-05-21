using System;
using Microsoft.AspNetCore.Identity;
using Shared.Data;

namespace Shared
{
    public class Chat
    {
        public int Id { get; set; }
        public string SenderUserName { get; set; } // FK to AspNetUsers
        public string ReceiverUserName { get; set; } // FK to AspNetUsers
        public string Message { get; set; }
        public DateTime SentAt { get; set; } = DateTime.UtcNow;

        // Navigation property to the user
        public virtual GotorzAppUser User { get; set; }
        public string UserId { get; set; } // FK to AspNetUsers

        public Chat()
        {
            // Default constructor
        }

        public Chat(string senderUserName, /*string receiverUserName,*/ string message)
        {
            SenderUserName = senderUserName;
            //ReceiverUserName = receiverUserName;
            Message = message;
            SentAt = DateTime.UtcNow;
        }
    }
}