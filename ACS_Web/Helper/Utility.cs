using System.Security.Cryptography;
using System.Text;

namespace ACS_Web.Helper;

public class Utility
{
    public static string Encrypt(string password)
    {
       
        string salt = "S0m3R@nd0mSalt";

        return salt + password;
    }
}
