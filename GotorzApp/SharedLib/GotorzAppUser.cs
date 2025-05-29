using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;


namespace SharedLib;

// Add profile data for application users by adding properties to the ApplicationUser class
public class GotorzAppUser : IdentityUser
{
    public static string DefaultRole = "Customer";

    public List<Chat> SentChats { get; set; } = new List<Chat>();
    public List<Chat> ReceivedChats { get; set; } = new List<Chat>();
}
