using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Authorization;
using System.Net.Http.Headers;
using System.Security.Claims;

namespace ACS_Web.Helper;

public class CustomAuthenticationStateProvider : AuthenticationStateProvider
{
    private readonly HttpClient _httpClient;
    private readonly ILocalStorageService _localStorage;
    private readonly AuthenticationState _anonymous = null;
    public CustomAuthenticationStateProvider(HttpClient httpClient, ILocalStorageService localStorage)
    {
        _httpClient = httpClient;
        _localStorage = localStorage;
       
    }
    public override async Task<AuthenticationState> GetAuthenticationStateAsync()
    {
        var token = await _localStorage.GetItemAsStringAsync("token");
        if (token == null)
        {
            var identity = new ClaimsIdentity(//new[]
                                              //{
                                              //    new Claim(ClaimTypes.Name,"pra@pra.com") 
                                              //},
                                              //"apiauth_type"
        );
            var user = new ClaimsPrincipal(identity);

            return new  AuthenticationState(user);
        }
        _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
       
        
        return new AuthenticationState(
            new ClaimsPrincipal(
                new ClaimsIdentity(
                    JwtParser.ParseClaimsFromJwt(token),
                    "jwtAuthType")));
    }

    public void NotifyUserAuthentication(string token)
    {
        var authenticatedUser = new ClaimsPrincipal(

            new ClaimsIdentity(
                    JwtParser.ParseClaimsFromJwt(token),
                    "jwtAuthType"));
        var authState = Task.FromResult(new AuthenticationState(authenticatedUser));
        NotifyAuthenticationStateChanged(authState);
    }
    public void NotifyUserLogout()
    {
        var authState = Task.FromResult(_anonymous);
        NotifyAuthenticationStateChanged(authState);
    }
}
