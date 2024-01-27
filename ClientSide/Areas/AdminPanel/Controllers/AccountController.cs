using Microsoft.AspNetCore.Mvc;
using ServiceLayer.PublicClasses;
using ServiceLayer.Services.Interfaces;

namespace ClientSide.Areas.AdminPanel.Controllers
{
	[Area(nameof(AdminPanel))]
	public class AccountController : Controller
	{
		private readonly IIdentityServices _identityServices;

		public AccountController(IIdentityServices identityServices)
        {
			_identityServices = identityServices;
		}
        [PermissionCheck(2)]
        public IActionResult UserList(int pageId)
		{
			var userList = _identityServices.GetAllUserForAdmin(pageId);
			return View(userList);
		}
	}
}
