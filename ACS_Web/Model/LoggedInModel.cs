namespace ACS_Web.Model;

public class LoggedInModel
{
    public string token { get; set; } = "";
    public string ID { get; set; } = "";
    public bool EmailConfirmed { get; set; } = false;
}


