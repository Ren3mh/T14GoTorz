using Microsoft.AspNetCore.Identity;
using SharedLib;
using System.Threading.Tasks;

namespace SharedLib.Service;

public class RoleManagerService
{
    private readonly RoleManager<IdentityRole> _roleManager;
    private readonly UserManager<GotorzAppUser> _userManager;

    public RoleManagerService(RoleManager<IdentityRole> roleManager, UserManager<GotorzAppUser> userManager)
    {
        _roleManager = roleManager;
        _userManager = userManager;
    }

    // Create a new role
    public async Task<IdentityResult> CreateRoleAsync(string roleName)
    {
        if (!await _roleManager.RoleExistsAsync(roleName))
        {
            return await _roleManager.CreateAsync(new IdentityRole(roleName));
        }
        return IdentityResult.Success;
    }

    // Delete an existing role
    public async Task<IdentityResult> DeleteRoleAsync(string roleName)
    {
        var role = await _roleManager.FindByNameAsync(roleName);
        if (role != null)
        {
            return await _roleManager.DeleteAsync(role);
        }
        return IdentityResult.Failed(new IdentityError { Description = $"Role '{roleName}' not found." });
    }

    // Check if a role exists
    public async Task<bool> RoleExistsAsync(string roleName)
    {
        return await _roleManager.RoleExistsAsync(roleName);
    }

    // Assign a role to a user by email
    public async Task<IdentityResult> AssignRoleToUserAsync(string email, string roleName)
    {
        var user = await _userManager.FindByEmailAsync(email);
        if (user != null)
        {
            return await _userManager.AddToRoleAsync(user, roleName);
        }
        return IdentityResult.Failed(new IdentityError { Description = $"User with email '{email}' not found." });
    }

    // Remove a role from a user by email
    public async Task<IdentityResult> RemoveRoleFromUserAsync(string email, string roleName)
    {
        var user = await _userManager.FindByEmailAsync(email);
        if (user != null)
        {
            return await _userManager.RemoveFromRoleAsync(user, roleName);
        }
        return IdentityResult.Failed(new IdentityError { Description = $"User with email '{email}' not found." });
    }

    // Get all roles
    public IEnumerable<IdentityRole> GetAllRoles()
    {
        return _roleManager.Roles;
    }

    // Get all roles for a specific user
    public async Task<IList<string>> GetUserRolesAsync(string email)
    {
        var user = await _userManager.FindByEmailAsync(email);
        if (user != null)
        {
            return await _userManager.GetRolesAsync(user);
        }
        return new List<string>();
    }
}