
using ACS_DataManager.Library.Models;

namespace ACS_DataManager.Library.ResponseModel
{
    public class UsersResponse : BaseResponse
    {
        public UserInfoModel ? data { get; set; }
    }
}
