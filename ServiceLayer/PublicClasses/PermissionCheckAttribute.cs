using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using ServiceLayer.Services.Interfaces;

namespace ServiceLayer.PublicClasses
{
    public class PermissionCheckAttribute : Attribute, IAuthorizationFilter
    {
        private readonly int _permissionId;
        private IIdentityServices _identityServies;

        public PermissionCheckAttribute(int permissionId)
        {
            _permissionId = permissionId;
        }
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            if (context.HttpContext.User.Identity.IsAuthenticated)
            {
                _identityServies = (IIdentityServices)context.HttpContext.RequestServices.GetService(typeof(IIdentityServices));
                string phoneNumber = context.HttpContext.User.Identity.Name;
                if (!_identityServies.CheckPermission(_permissionId, phoneNumber))
                {
                    context.Result = new RedirectResult("/login");//profile
                }
            }
            else
            {
                context.Result = new RedirectResult("/login");
            }
        }
    }
}
