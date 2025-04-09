using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace CRM_User.Web.Middleware
{
    public class PermissionMiddleware
    {
        private readonly RequestDelegate _next;

        public PermissionMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            // 1. If no endpoint info, move to next
            var endpoint = context.GetEndpoint();
            if (endpoint == null)
            {
                await _next(context);
                return;
            }

            // 2. Check if endpoint has HasPermissionAttribute
            var requiredPermissions = endpoint
                .Metadata
                .GetOrderedMetadata<HasPermissionAttribute>();

            if (!requiredPermissions.Any())
            {
                await _next(context);
                return;
            }

            // 3. Get user's permissions from token / claims
            var userPermissions = context.User
                .Claims
                .Where(c => c.Type == "Permission")
                .Select(c => c.Value)
                .ToList();

            // 4. Verify user has required permission
            foreach (var permissionAttribute in requiredPermissions)
            {
                if (!userPermissions.Contains(permissionAttribute.Permission))
                {
                    context.Response.StatusCode = StatusCodes.Status403Forbidden;
                    await context.Response.WriteAsync("You don't have permission to access this resource.");
                    return;
                }
            }

            // 5. If permission matched, continue
            await _next(context);
        }
    }

}
