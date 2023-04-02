using ACS_DataManager.Authorization;
using ACS_DataManager.Library.DataAccess.Interface;
using ACS_DataManager.Library.Models;
using ACS_DataManager.Library.ResponseModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ACSDataManager.Controllers;
[Route("api/[controller]")]
[ApiController]
[Authorize]
public class RegistrationController : ControllerBase
{
    private readonly IRegistrationData _registration;

    public RegistrationController(IRegistrationData registration)
    {
        _registration = registration;
    }
    [AllowAnonymous]
    [HttpPost]
    public UsersResponse Registration(UserModel userModel) => _registration.Registration(userModel);
    
    [HttpGet("{id}")]
    public UsersResponse UserInfp(string id) => _registration.UserInfo(id);
}
