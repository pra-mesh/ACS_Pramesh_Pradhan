using System.ComponentModel.DataAnnotations;
namespace ACS_DataManager.Library.Model
{
    public class UserIDModel
    {
        [Required]
        public string UserID { get; set; } = "";
        [Required, MaxLength(50, ErrorMessage = "The {0} value cannot exceed {1} characters.")]
        public string UserName { get; set; }
    }
}
