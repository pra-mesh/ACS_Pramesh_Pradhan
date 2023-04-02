using ACS_DataManager.Library.DataAccess.Interface;
using ACS_DataManager.Library.DataAccess.Internal;
using ACS_DataManager.Library.Models;
using ACS_DataManager.Library.ResponseModel;
using Dapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ACS_DataManager.Library.DataAccess.Implementation;
public class RegistrationData : IRegistrationData
{
    private readonly ISqlDataAccess _sql;
    public RegistrationData(ISqlDataAccess sql)
    {
        _sql = sql;
    }
    public UsersResponse Registration(UserModel userModel)
    {
        UsersResponse sr = new UsersResponse();

        var p = new DynamicParameters(userModel);
        try
        {
            Guid guid = Guid.NewGuid();
            userModel.Id = guid.ToString();
            _sql.SaveData<dynamic>("dbo.spRegistration", p, "SqlConn");
            sr.statusCode = 0;
            sr.statusMessage = "Success";
            sr.data = UserInfo(userModel.Id).data;
        }
        catch (Exception ex)
        {
            sr.data = null;
            sr.statusCode = 1;
            sr.statusMessage = ex.Message;
        }
        return sr;

    }
    public UsersResponse UserInfo(string userID)
    {
        UsersResponse sr = new UsersResponse();
        UserInfoModel sp = new UserInfoModel();
        var p = new DynamicParameters(userID);
        try
        {
            Guid guid = Guid.NewGuid();
           
            sp = _sql.LoadFirstData<UserInfoModel,dynamic>("Select * from users where Id=@userIDId", p, "SqlConn");
            sr.statusCode = 0;
            sr.statusMessage = "Success";
            sr.data = sp;
        }
        catch (Exception ex)
        {
            sr.data = null;
            sr.statusCode = 1;
            sr.statusMessage = ex.Message;
        }
        return sr;

    }

}
