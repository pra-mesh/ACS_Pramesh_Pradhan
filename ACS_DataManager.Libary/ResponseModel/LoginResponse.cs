using ACS_DataManager.Library.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ACS_DataManager.Library.ResponseModel;
public class LoginResponse : BaseResponse
{
    public TokenModel ? data { get; set; }
}