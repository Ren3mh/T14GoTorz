using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;


namespace SharedLib.Service;

public class UserManagerService
{
    private readonly UserManager<GotorzAppUser> _userManager;

    public UserManagerService(UserManager<GotorzAppUser> userManager)
    {
        _userManager = userManager;
    }

    public async Task<IdentityResult> CreateUserAsync(GotorzAppUser user, string password)
    {
        var result = await _userManager.CreateAsync(user, password);
        if (result.Succeeded)
        {
            // Assign "Customer" role by default
            await _userManager.AddToRoleAsync(user, "Customer");
        }
        return result;
    }

    public async Task<GotorzAppUser> FindUserByEmailAsync(string email)
    {
        return await _userManager.FindByEmailAsync(email);
    }

    public async Task<IdentityResult> UpdateUserAsync(GotorzAppUser user)
    {
        return await _userManager.UpdateAsync(user);
    }

    public async Task<IdentityResult> DeleteUserAsync(GotorzAppUser user)
    {
        return await _userManager.DeleteAsync(user);
    }

    public async Task<IdentityResult> ChangePasswordAsync(GotorzAppUser user, string currentPassword, string newPassword)
    {
        return await _userManager.ChangePasswordAsync(user, currentPassword, newPassword);
    }

    public async Task<bool> IsInRoleAsync(GotorzAppUser user, string roleName)
    {
        return await _userManager.IsInRoleAsync(user, roleName);
    }

    public async Task<IdentityResult> AddToRoleAsync(GotorzAppUser user, string roleName)
    {
        return await _userManager.AddToRoleAsync(user, roleName);
    }

    public async Task<IdentityResult> RemoveFromRoleAsync(GotorzAppUser user, string roleName)
    {
        return await _userManager.RemoveFromRoleAsync(user, roleName);
    }
}
