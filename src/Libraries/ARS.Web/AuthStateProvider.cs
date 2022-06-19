using System.Security.Claims;
using ARS.Common.Collections;
using Microsoft.AspNetCore.Components.Authorization;

namespace ARS.Web;

public class AuthStateProvider : AuthenticationStateProvider
{
    private readonly UserCollection _userCollection;

    public AuthStateProvider(UserCollection userCollection)
    {
        _userCollection = userCollection;
    }

    public override async Task<AuthenticationState> GetAuthenticationStateAsync()
    {
        //var user = await _userService.GetUserAsync();
        var identity = new ClaimsIdentity(new[]
        {
            new Claim(ClaimTypes.Name, "TestUser"),
            new Claim(ClaimTypes.Role, "Admin"),
        }, "Server authentication");
       
        var user = new ClaimsPrincipal(identity);
        
        return new AuthenticationState(user);
    }
}