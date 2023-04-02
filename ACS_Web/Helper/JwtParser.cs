﻿using System.Text.Json;
using System.Security.Claims;

namespace ACS_Web.Helper;

public class JwtParser
{
    public static IEnumerable<Claim> ParseClaimsFromJwt (string jwt)
    {
        var claims = new List<Claim> ();
        var payload = jwt.Split('.')[1];
        var jsonBytes = ParseBase64WithoutPadding(payload);
        var keyValuePairs =JsonSerializer.Deserialize<Dictionary<string, object>>(jsonBytes);
        ExtractRolesFromJWT(claims, keyValuePairs);
        claims.AddRange(keyValuePairs.Select(x => new Claim(x.Key, x.Value.ToString())));
        return claims;
    }
    private static void ExtractRolesFromJWT(List<Claim> claims, Dictionary<string, object> keyValuePairs)
    {
        keyValuePairs.TryGetValue(ClaimTypes.Role, out  object roles);
        if (roles is not null)
        {
            var parseRoles = roles.ToString().Trim().TrimStart('[').TrimEnd(']').Split(',');
            if(parseRoles.Length > 1 )
            {
                foreach (var parsedRole in parseRoles)
                {
                    claims.Add(new Claim(ClaimTypes.Role, parsedRole.Trim('"')));

                }
            }
            else
            {
                claims.Add(new Claim(ClaimTypes.Role, parseRoles[0]));
            }
            keyValuePairs.Remove(ClaimTypes.Role);
        }
    }
    private static byte[] ParseBase64WithoutPadding(string base64)
    {
        switch (base64.Length % 4)
        {
            case 2:
                base64 += "==";
                break;
            case 3:
                base64 += "=";
                break;
        }
        return Convert.FromBase64String(base64);
    }
}