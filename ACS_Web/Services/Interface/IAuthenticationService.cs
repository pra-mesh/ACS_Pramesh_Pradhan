using ACS_Web.Model;

namespace ACS_Web.Services.Interface;
public interface IAuthenticationService
{
    Task<string> login(LoginModel loginModel);
    Task Logout();
    Task<bool> Revoke();
}