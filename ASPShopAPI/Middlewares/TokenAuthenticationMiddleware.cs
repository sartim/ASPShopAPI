using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

public class TokenAuthenticationMiddleware
{
    private readonly RequestDelegate _next;

    public TokenAuthenticationMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        // Check if the request contains the Authorization header
        if (context.Request.Headers.ContainsKey("Authorization"))
        {
            string token = context.Request.Headers["Authorization"].ToString().Replace("Bearer ", "");

            // Validate the token here (e.g., verify signature, expiration, etc.)
            bool isTokenValid = ValidateToken(token);

            if (!isTokenValid)
            {
                // Return 401 Unauthorized if the token is not valid
                context.Response.StatusCode = 401;
                return;
            }
        }
        else
        {
            // Return 401 Unauthorized if the Authorization header is missing
            context.Response.StatusCode = 401;
            return;
        }

        // If the token is valid, proceed to the next middleware
        await _next(context);
    }

    private bool ValidateToken(string token)
    {

        // Return true if the token is valid; otherwise, return false
        return true;
    }
}
