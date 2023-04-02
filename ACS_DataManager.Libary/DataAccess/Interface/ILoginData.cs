using ACS_DataManager.Library.Models;
using ACS_DataManager.Library.ResponseModel;

namespace ACS_DataManager.Library.DataAccess.Interface;
public interface ILoginData
{
    LoginResponse login(LoginModel loginModel);
}