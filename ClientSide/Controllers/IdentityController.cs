using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using ServiceLayer.Services.Interfaces;
using ServiceLayer.ViewModels.IdentityViewModels;
using System.Security.Claims;

namespace ClientSide.Controllers
{
    public class IdentityController : Controller
    {
        private readonly IIdentityServices _identityService;

        public IdentityController(IIdentityServices identityService)
        {
            _identityService = identityService;
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        [Route("Register")]
        public IActionResult RegisterByMobile()
        {
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Home");
            }
            return View();
        }

        [HttpPost]
        [Route("Register")]
        public IActionResult RegisterByMobile(RegsisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                int res = _identityService.RegsisterByNumber(model);
                if (res == 1)
                {
                    TempData["error"] = "شماره موبایل قبلا در سیستم ثبت شده است";
                    return View(model);
                }
                return RedirectToAction("ConfrimByMobile", new { id = model.PhoneNumber });
            }
            return View(model);
        }

        [HttpGet]
        [Route("ConfrimMobile/{id}")]
        public IActionResult ConfrimByMobile(string id)
        {
            bool isMobileExist = _identityService.IsPhoneNumberExist(id);
            if (isMobileExist == false)
            {
                return RedirectToAction("Index", "Home");
            }
            ViewBag.PhoneNumber = id;
            return View();
        }

        [HttpPost]
        [Route("ConfrimMobile/{id}")]
        public IActionResult ConfrimByMobile(ConfrimViewModel model)
        {
            if (ModelState.IsValid)
            {
                int res = _identityService.ConfrimPhoneNumber(model.PhoneNumber, model.Code);
                if (res == 5)
                {
                    int userId = _identityService.GetUserIdByPhoneNumber(model.PhoneNumber);

                    var claims = new List<Claim>()
                    {
                        new Claim(ClaimTypes.NameIdentifier,userId.ToString()),
                        new Claim(ClaimTypes.Name,model.PhoneNumber)
                    };
                    var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                    var principal = new ClaimsPrincipal(identity);

                    var properties = new AuthenticationProperties
                    {
                        IsPersistent = true,
                        AllowRefresh = true
                    };
                    HttpContext.SignInAsync(principal, properties);
                    return View(model);
                    TempData["success"] = "حساب کاربری شما با موفقیت تایید شد";
                    return RedirectToAction("LoginByMobile");
                }
                return RedirectToAction("Index", "Home");
            }
            return View(model);
        }

        [HttpGet]
        [Route("ForgotPassword")]
        public IActionResult ForgotPassword()
        {
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Home");
            }
            return View();
        }

        [HttpPost]
        [Route("ForgotPassword")]
        public IActionResult ForgotPassword(ForgotPassViewModel model)
        {
            if (ModelState.IsValid)
            {
                int res = _identityService.GetPhoneNumberForgotPass(model.PhoneNumber);
                if (res == 0)
                {
                    TempData["error"] = "شماره وجود ندارد";
                    return View();
                }
                if (res == 2)
                {
                    TempData["error"] = "شماره موبایل تایید نشده است";
                    return View();
                }
                if (res == 4)
                {
                    TempData["error"] = "حساب کاربری مسدود می باشد";
                    return View();
                }
                if (res == 5)
                {
                    TempData["success"] = "کد تایید برای حساب کاربری شما با موفقیت ارسال شد";
                    return RedirectToAction("RestPassword", new { id = model.PhoneNumber });
                }
            }
            return View();
        }

        [HttpGet]
        [Route("RestPassword")]
        public IActionResult RestPassword(string id)
        {
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Home");
            }
            ViewBag.phoneNumber = id;
            return View();
        }

        [HttpPost]
        [Route("RestPassword")]
        public IActionResult RestPassword(RestPassViewModel model)
        {
            if (ModelState.IsValid)
            {
                int res = _identityService.ResetPasswordByMobile(model);
                if (res == 0)
                {
                    TempData["error"] = "شماره وجود ندارد";
                    return View();
                }
                if (res == 2)
                {
                    TempData["error"] = "شماره موبایل تایید نشده است";
                    return View();
                }
                if (res == 4)
                {
                    TempData["error"] = "حساب کاربری مسدود می باشد";
                    return View();
                }
                if (res == 5)
                {
                    TempData["success"] = "رمز با موفقیت تغییر کرد";
                    return RedirectToAction("LoginByMobile");
                }
            }
            return View();
        }

        [HttpGet]
        [Route("Login")]
        public IActionResult LoginByMobile()
        {
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Home");
            }
            return View();
        }

        [HttpPost]
        [Route("Login")]
        public IActionResult LoginByMobile(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                int res = _identityService.GetUserStatusForLoginByPhoneNumber(model.PhoneNumber, model.Password);
                if (res == 0)
                {
                    TempData["error"] = "شماره وجود ندارد";
                    return View();
                }
                if (res == 2)
                {
                    TempData["error"] = "شماره موبایل تایید نشده است";
                    return View();
                }
                if (res == 3)
                {
                    TempData["error"] = "رمز درست نیست";
                    return View(model);
                }
                if (res == 4)
                {
                    TempData["error"] = "حساب کاربری مسدود می باشد";
                    return View();
                }
                else
                {
                    int userId = _identityService.GetUserIdByPhoneNumber(model.PhoneNumber);

                    var claims = new List<Claim>()
                    {
                        new Claim(ClaimTypes.NameIdentifier,userId.ToString()),
                        new Claim(ClaimTypes.Name,model.PhoneNumber)
                    };
                    var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                    var principal = new ClaimsPrincipal(identity);

                    var properties = new AuthenticationProperties
                    {
                        IsPersistent = model.RememberMe,
                        AllowRefresh = true
                    };
                    HttpContext.SignInAsync(principal, properties);

                    //if (returnUrl != null)
                    //{
                    //    TempData["success"] = "ورود با موفقیت انجام شد";
                    //    return Redirect(returnUrl);
                    //}
                    TempData["success"] = "ورود با موفقیت انجام شد";
                    return RedirectToAction("Index", "Home");
                }
            }
            return View(model);
        }

        [Route("Logout")]
        public IActionResult Logout()
        {
            HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Index", "Home");
        }
    }
}
