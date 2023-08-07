using Church.ServiceInterfaces;
using System.Security.Claims;

namespace Church.Middlewares
{
    public class RoleValidationMiddleware
    {
        private readonly RequestDelegate _next;

        public RoleValidationMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context, IRoleService roleService)
        {
            var user = context.User;

            if (user.Identity.IsAuthenticated)
            {
                var userRoleId = user.FindFirstValue(ClaimTypes.Role);
                var userRole = await roleService.GetRoleById(userRoleId);

                if (userRole == null)
                {
                    context.Response.StatusCode = StatusCodes.Status403Forbidden;
                    await context.Response.WriteAsync("Forbidden: Invalid Role");
                    return;
                }
            }

            await _next(context);
        }

    }
}
