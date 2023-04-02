using System.ComponentModel.DataAnnotations;

namespace ACS_DataManager.Library.ResponseModel;
public class BaseResponse
{
    [Required]
    public int statusCode { get; set; }
    [MaxLength(10, ErrorMessage = "The {0} value cannot exceed {1} characters. ")]
    public string statusMessage { get; set; } = "Failed";
}
