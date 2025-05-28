using Microsoft.AspNetCore.Identity;
using SharedLib;
using SharedLib.Service;

namespace GotorzApp;

public class InitializeFakeUsers
{
    private readonly RoleManagerService _roleManagerService;
    private readonly UserManagerService _userManagerService;

    public GotorzAppUser[] users =
    [
        new GotorzAppUser() { UserName = "admin@gotorz.com", Email = "admin@gotorz.com" },
        new GotorzAppUser() { UserName = "employee1@gotorz.com", Email = "employee1@gotorz.com" },
        new GotorzAppUser() { UserName = "employee2@gotorz.com", Email = "employee2@gotorz.com" },
        new GotorzAppUser() { UserName = "rene@email.com", Email = "rene@email.com" },
        new GotorzAppUser() { UserName = "sander@email.com", Email = "sander@email.com" },
        new GotorzAppUser() { UserName = "christian@email.com", Email = "christian@email.com" },
    ];

    private readonly string defaultPassword = "Pwd!23";

    public string[] roles = ["Admin", "Customer", "Employee"];

    public InitializeFakeUsers(RoleManagerService roleManagerService, UserManagerService userManagerService)
    {
        _roleManagerService = roleManagerService;
        _userManagerService = userManagerService;
    }

    public async Task InitializeAsync()
    {
        await InitializeUsersAsync();
        await InitializeRolesAsync();

        // Example: Assign a role to a user
        await _roleManagerService.AssignRoleToUserAsync(users[0].Email, "Admin");
        await _roleManagerService.AssignRoleToUserAsync(users[1].Email, "Employee");
        await _roleManagerService.AssignRoleToUserAsync(users[2].Email, "Employee");
        await _roleManagerService.AssignRoleToUserAsync(users[3].Email, "Customer");
    }


    public async Task InitializeUsersAsync()
    {
        foreach (var user in users)
        {
            if (await _userManagerService.FindUserByEmailAsync(user.Email) == null)
            {
                await _userManagerService.CreateUserAsync(user, defaultPassword);
            }
        }
    }

    public async Task InitializeRolesAsync()
    {
        foreach (var role in roles)
        {
            if (!await _roleManagerService.RoleExistsAsync(role))
            {
                await _roleManagerService.CreateRoleAsync(role);
            }
        }
    }
}
