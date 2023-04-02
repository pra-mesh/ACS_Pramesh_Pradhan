using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace ACS_DataManager.Library.Models;
public class TokenModel
{
    public string token { get; set; } = "";
    public string ID { get; set; }

    [JsonIgnore] // refresh token is returned in http only cookie
    public string RefreshToken { get; set; } = "";

    public bool EmailConfirmed { get; set; } = false;
}
