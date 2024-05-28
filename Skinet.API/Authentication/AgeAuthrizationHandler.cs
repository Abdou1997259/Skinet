using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace Skinet.API.Authentication
{
    public class AgeAuthrizationHandler : AuthorizationHandler<AgeGreaterThan25Requirement>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, AgeGreaterThan25Requirement requirement)
        {
            var dateOfBirth = DateTime.Parse(context.User.FindFirstValue("DateOfBirth"));
            if (DateTime.Today.Year - dateOfBirth.Year >= 25)
                context.Succeed(requirement);

            return Task.CompletedTask;
        }

       
    }
}
