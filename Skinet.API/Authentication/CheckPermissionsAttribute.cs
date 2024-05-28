using Core.Entities;

namespace Skinet.API.Authentication
{
    [AttributeUsage(AttributeTargets.Method,AllowMultiple =false)]
    public class CheckPermissionsAttribute:Attribute
    {
        public CheckPermissionsAttribute(Permission permission)
        {
                Permission = permission;
        }
        public Permission Permission { get; }
    }
}
