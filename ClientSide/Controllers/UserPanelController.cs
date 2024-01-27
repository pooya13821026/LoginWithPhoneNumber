using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ServiceLayer.Services.Interfaces;

namespace ClientSide.Controllers
{
    [Authorize(AuthenticationSchemes = CookieAuthenticationDefaults.AuthenticationScheme)]
    public class UserPanelController : Controller
    {
        private readonly IIdentityServices _identityServices;

        public UserPanelController(IIdentityServices identityServices)
        {
            _identityServices = identityServices;
        }
        public IActionResult Dashboard()
        {
            var userInfo = _identityServices.GetUserInfo(User.Identity.Name);
            return View(userInfo);
        }
    }
}
