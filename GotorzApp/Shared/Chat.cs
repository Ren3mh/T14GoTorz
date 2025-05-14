using System;
using Shared.Data;

namespace Shared
{
    public class Chat
    {
        public int Id { get; set; }
        public string SenderId { get; set; } = string.Empty; // FK to AspNetUsers
        public string ReceiverId { get; set; } // 
        public string Message { get; set; } = string.Empty;
        public DateTime SentAt { get; set; } = DateTime.UtcNow;

        // Navigation property (optional)
        public GotorzAppUser? Sender { get; set; }
    }
}