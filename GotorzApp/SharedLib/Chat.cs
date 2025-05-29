
using System.ComponentModel.DataAnnotations;

namespace SharedLib;

public class Chat
{
    public int Id { get; set; }
    public string? SenderUserName => Sender?.UserName;
    public string? ReceiverUserName => Receiver?.UserName;

    [Required]
    public string Message { get; set; }

    [Required]
    public DateTime SentAt { get; set; } = DateTime.UtcNow;

    public virtual GotorzAppUser? Sender { get; set; }
    public virtual string SenderUserId { get; set; }

    public virtual GotorzAppUser? Receiver { get; set; }
    public virtual string ReceiverUserId { get; set; }

    public Chat(string message,string senderUserId, string receiverUserId, DateTime sentAt)
    {
        Message = message;
        SenderUserId = senderUserId;
        ReceiverUserId = receiverUserId;
        SentAt = sentAt;
    }
}