using Duende.IdentityServer.Models;
using Duende.IdentityServer.Validation;
using System.Security.Claims;

namespace identity
{
    public class ResourceOwnerPasswordValidator : IResourceOwnerPasswordValidator
    {
        public Task ValidateAsync(ResourceOwnerPasswordValidationContext context)
        {
            if (InMemoryUsers.Users.TryGetValue(context.UserName, out var userInfo))
            {
                if (context.Password == userInfo.Password)
                {
                    var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, context.UserName),
                    new Claim("name", context.UserName)
                };

                    foreach (var role in userInfo.Roles)
                    {
                        claims.Add(new Claim(ClaimTypes.Role, role));
                        claims.Add(new Claim("role", role));
                    }
                    claims.Add(new Claim(ClaimTypes.Email, "asd@asd.com"));
                    claims.Add(new Claim("email", "asd@asd.com"));
                    context.Result = new GrantValidationResult(
                        subject: context.UserName,
                        authenticationMethod: "forms",
                        claims: claims);

                    return Task.CompletedTask;
                }
            }

            context.Result = new GrantValidationResult(TokenRequestErrors.InvalidGrant, "Credenciales inválidas");
            return Task.CompletedTask;
        }
    }
    public static class InMemoryUsers
    {
        public static readonly Dictionary<string, (string Password, string[] Roles)> Users = new()
    {
        { "admin", ("admin123", new[] { "Admin", "User" }) },
        { "user", ("user123", new[] { "User" }) }
    };
    }
}
