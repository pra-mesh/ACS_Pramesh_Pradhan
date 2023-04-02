using ACS_Web.Model;

namespace ACS_Web.Services.Interface;
public interface IUserServices
{

    Task<LoggedInModel> LoginAsync(LoginModel user);
}