using Infrastructure.Data;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace Skinet.API.Authentication
{
    public class PermissionBasedAuthorizationFilter(StoreContext _db) : IAsyncAuthorizationFilter
    {

        public async Task OnAuthorizationAsync(AuthorizationFilterContext context)
        {
            var attribute = (CheckPermissionsAttribute)context.ActionDescriptor.EndpointMetadata.FirstOrDefault(x => x is CheckPermissionsAttribute);
            if (attribute != null)
            {
                var claimIdentity = context.HttpContext.User.Identity as ClaimsIdentity;
                if (claimIdentity == null || !claimIdentity.IsAuthenticated) {

                    context.Result = new ForbidResult();
                }
                else
                {
                    var userId = int.Parse(claimIdentity.FindFirst(ClaimTypes.NameIdentifier).Value);
                    var haspermission = await _db.UserPermissions.AnyAsync(x => x.UserId  ==userId && x.PermissionId == (int)attribute.Permission);
                    if (!haspermission)
                    {
                        context.Result = new ForbidResult();
                       
                    }
                }
            }


        }
    }
}
