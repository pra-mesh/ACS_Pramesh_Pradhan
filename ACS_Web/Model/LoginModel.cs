using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;


namespace ACS_Web.Model;

public class LoginModel
{
   
    
    [EmailAddress (ErrorMessage ="Invalid Format"),]

    public string userName { get; set; }
    [Required]
    [DataType(DataType.Password)]
    [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 8)]
    public string password { get; set; } = "";
}
