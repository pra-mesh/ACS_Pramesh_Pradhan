
using ACS_DataManager.Library.DataAccess.Interface;
using ACS_DataManager.Library.DataAccess.Internal;
using ACS_DataManager.Library.Models;
using ACS_DataManager.Library.ResponseModel;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ACS_DataManager.Library.DataAccess.Implementation;
public class LoginData : ILoginData
{
    private readonly ISqlDataAccess _sql;

    public LoginData(ISqlDataAccess sql)
    {
        _sql = sql;
    }
    public LoginResponse login(LoginModel loginModel)
    {
        LoginResponse lr = new LoginResponse();
        try
        {
            string q = "spLogin";
            var output = _sql.LoadData<TokenModel, LoginModel>(q, loginModel, "SqlConn");
            if (output == null || output.Count < 1)
            {

                lr.statusCode = 1;
                lr.statusMessage = "Invalid User login";
                return lr;
            }

            lr.statusCode = 0;
            lr.statusMessage = "Success";
            lr.data = output.FirstOrDefault();


        }
        catch (Exception ex)
        {
            lr.statusCode = 2;
            lr.statusMessage = ex.Message;

        }
        return lr;

    }
}
