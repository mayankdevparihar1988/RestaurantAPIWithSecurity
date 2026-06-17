namespace Restaurants.Application.Users;

public record CurrentUser(string Id, string Email, IEnumerable<string> Roles)
{
public bool IsInRole(string rolename) => Roles.Contains(rolename);

}