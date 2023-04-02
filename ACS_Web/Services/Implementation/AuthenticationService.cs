using ACS_Web.Helper;
using ACS_Web.Model;
using ACS_Web.Model.ResponseModel;
using ACS_Web.Services.Interface;
using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Authorization;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Reflection;
using System.Text;
using System.Text.Json;
using JsonSerializer = System.Text.Json.JsonSerializer;

namespace ACS_Web.Services.Implementation;

public class AuthenticationService : IAuthenticationService
{
    private readonly HttpClient _httpClient;
    private readonly AuthenticationStateProvider _auth;
    private readonly ILocalStorageService _localStorage;

    public AuthenticationService(HttpClient httpClient,
                              AuthenticationStateProvider Auth,
                              ILocalStorageService localStorage)
    {
        _httpClient = httpClient;
        _auth = Auth;
        _localStorage = localStorage;
    }

    public async Task<string> login(LoginModel loginModel)
    {
        LoginResponse lm = new LoginResponse();
        var serializedUser = new StringContent(
        loginModel.ToString(),
        Encoding.UTF8,
        "application/json");
        try
        {
            var authResult = await _httpClient.PostAsJsonAsync("https://localhost:7059/api/Login/token", loginModel);
            var data = await authResult.Content.ReadFromJsonAsync<JsonElement>();
            if (authResult.IsSuccessStatusCode == false)
            {
                return null;
            }
            var result = JsonSerializer.Deserialize<LoginResponse>(
                data,
                new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

            lm= result;
            if (lm.statusCode == 0)
            {
                LoggedInModel loggedID = lm.data;
                await _localStorage.SetItemAsync("token", loggedID.token);
                ((CustomAuthenticationStateProvider)_auth).NotifyUserAuthentication(loggedID.token);
                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", loggedID.token);
            }
        }
        catch (Exception ex)
        {
        
        }
       
        return lm.statusMessage;
    }
    public async Task<bool> Revoke()
    {
      
     
        try
        {
            string model = "md";
            
            //var postBody = new { model = "Blazor POST Request Example" };

            var authResult = await _httpClient.PostAsync("https://localhost:7059/api/Login/revoke-token/model" + model, null);
            var data = await authResult.Content.ReadFromJsonAsync<JsonElement>();
            var s = authResult.RequestMessage;
            var st = authResult.StatusCode;
            if (authResult.IsSuccessStatusCode == false)
            {
                var br = JsonSerializer.Deserialize<LoginResponse>(
                data,
                new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
                return false;
            }
            var result = JsonSerializer.Deserialize<LoginResponse>(
                data,
                new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

           
           
        }
        catch (Exception ex)
        {

        }

        return false;
    }
    public async Task Logout()
    {
        await _localStorage.RemoveItemAsync("token");
        ((CustomAuthenticationStateProvider)_auth).NotifyUserLogout();
        _httpClient.DefaultRequestHeaders.Authorization = null;
    }
}
