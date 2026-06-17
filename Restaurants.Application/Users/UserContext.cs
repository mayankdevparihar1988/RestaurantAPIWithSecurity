using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace Restaurants.Application.Users;

public interface IUserContext
{
    CurrentUser? GetCurrentUser();
}

public class UserContext(IHttpContextAccessor contextAccessor, ILogger<UserContext> logger) : IUserContext
{
    public CurrentUser? GetCurrentUser()
    {
        if (contextAccessor.HttpContext != null)
        {
            var user = contextAccessor.HttpContext.User;
            if(user is null)
            {
                throw new InvalidOperationException("User is not exists in http context");
            }
            
            if(user.Identity == null || !user.Identity.IsAuthenticated)
            {
                return null;
            }
            
            var userId = user.FindFirst(c=> c.Type == ClaimTypes.NameIdentifier)!.Value;
            var email = user.FindFirst(c => c.Type == ClaimTypes.Email)!.Value;
            var roles = user.Claims.Where(c => c.Type == ClaimTypes.Role).Select(c => c.Value);
            
            return new CurrentUser(userId, email, roles);


        }
        else
        {
            logger.LogInformation("No HttpContext found");
            return null;
        }
    }
}