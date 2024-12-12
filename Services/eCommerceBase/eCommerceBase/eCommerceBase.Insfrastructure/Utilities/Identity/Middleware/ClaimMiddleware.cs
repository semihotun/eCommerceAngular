using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Security.Claims;

namespace eCommerceBase.Insfrastructure.Utilities.Identity.Middleware
{
    public class ClaimMiddleware(RequestDelegate next, IConfiguration configuration)
    {
        private readonly string[] notIgnorePath = ["/metrics", "/healthcheck"];
        private readonly RequestDelegate _next = next;
        private readonly IConfiguration _configuration = configuration;
        public async Task InvokeAsync(HttpContext httpContext)
        {
            if (httpContext.GetEndpoint()?.Metadata.GetMetadata<AllowAnonymousAttribute>() == null &&
                !notIgnorePath.Any(x => x == httpContext.Request.Path.Value))
            {
                if ((httpContext.User.Identity?.IsAuthenticated) == false)
                {
                    throw new UnauthorizedAccessException("User Not Logged In");
                }
            
                //Geçici kapalı
                //if (!httpContext.User
                //    .FindAll(ClaimTypes.Role)
                //    .Any(x => x.Value == httpContext.Request.Path.Value?.Replace("/api", _configuration["RegionName"]).ToLower()))
                //{
                //    throw new UnauthorizedAccessException("Not Access");
                //}
            }
            if ((httpContext.User.Identity?.IsAuthenticated) == true)
            {
                var _userContext = httpContext.RequestServices.GetRequiredService<UserScoped>();
                _userContext.Id = Guid.Parse(httpContext.User.FindFirst("id")?.Value!);
                _userContext.Roles = httpContext.User.FindFirst("roles")?.Value;
            }
            await _next(httpContext);
        }
    }
    public class UserScoped
    {
        public Guid Id { get; set; }
        public string? Roles { get; set; }
        public Guid? UserGroupId { get; set; }
    }
}
