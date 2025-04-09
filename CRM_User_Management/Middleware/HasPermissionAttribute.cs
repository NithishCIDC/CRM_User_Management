using Microsoft.AspNetCore.Mvc.Filters;

namespace CRM_User.Web.Middleware
{

    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Class, AllowMultiple = true)]
    public class HasPermissionAttribute : Attribute
    {
        public string Permission { get; }

        public HasPermissionAttribute(string permission)
        {
            Permission = permission;
        }
    }
}
