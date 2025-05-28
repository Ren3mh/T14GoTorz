using GotorzApp.Components.Account.Pages.Manage;
using Microsoft.AspNetCore.Identity;
using SharedLib;

namespace GotorzApp;

public class InitializeFakeUsers
{
    private readonly RoleManager<IdentityRole> _roleManager;
    private readonly UserManager<GotorzAppUser> _userManager;

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

    public InitializeFakeUsers(RoleManager<IdentityRole> roleManager, UserManager<GotorzAppUser> userManager)
    {
        _roleManager = roleManager;
        _userManager = userManager;
    }

    public async Task InitializeAsync()
    {
        await InitializeUsersAsync();
        await InitializeRolesAsync();

        var user1 = await _userManager.FindByEmailAsync(users[0].Email);
        if (user1 != null)
        {
            await _userManager.AddToRoleAsync(user1, "Admin");
        }
        var user2 = await _userManager.FindByEmailAsync(users[1].Email);
        if (user2 != null)
        {
            await _userManager.AddToRoleAsync(user2, "Employee");
        }
        var user3 = await _userManager.FindByEmailAsync(users[2].Email);
        if (user3 != null)
        {
            await _userManager.AddToRoleAsync(user3, "Employee");
        }
        var user4 = await _userManager.FindByEmailAsync(users[3].Email);
        if (user4 != null)
        {
            await _userManager.AddToRoleAsync(user4, "Customer");
        }
    }


    public async Task InitializeUsersAsync()
    {
        foreach (var user in users)
        {
            if (await _userManager.FindByEmailAsync(user.Email) == null)
            {
                await _userManager.CreateAsync(user, defaultPassword);
            }
        }
    }

    public async Task InitializeRolesAsync()
    {
        foreach (var role in roles)
        {
            if (!await _roleManager.RoleExistsAsync(role))
            {
                await _roleManager.CreateAsync(new IdentityRole(role));
            }
        }
    }
}
