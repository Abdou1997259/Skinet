using Microsoft.AspNetCore.Authorization;

namespace Skinet.API.Authentication
{
    public class AgeGreaterThan25Requirement:IAuthorizationRequirement
    {
    }
}
