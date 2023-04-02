﻿//using ACS_DataManager.Library.DataAccess.Interface;

using ACS_DataManager.Helpers;
using ACS_DataManager.Library.DataAccess.Interface;

namespace ACS_DataManager.Authorization;

public class JwtMiddleware
{
    private readonly RequestDelegate _next;


    public JwtMiddleware(RequestDelegate next)
    {
        _next = next;

    }

    public async Task Invoke(HttpContext context, IJwtUtils jwtUtils, IRegistrationData userService)
    {
        var token = context.Request.Cookies["Token"];
        if (token == null)
            token = context.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();
        if (token != null)
        {
            var userId = jwtUtils.ValidateJwtToken(token);
            if (userId != null)
            {
                // attach user to context on successful jwt validation
                context.Items["User"] = userService.UserInfo(userId.ToString());
            }
        }
        await _next(context);
    }
}

