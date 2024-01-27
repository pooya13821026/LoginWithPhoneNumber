using DataLayer.Context;
using DataLayer.Models.Identity;
using Microsoft.EntityFrameworkCore;
using ServiceLayer.PublicClasses;
using ServiceLayer.Services.Interfaces;
using ServiceLayer.ViewModels.BaseViewModels;
using ServiceLayer.ViewModels.IdentityViewModels;

namespace ServiceLayer.Services
{
    public class IdentityServices : IIdentityServices
    {
        private readonly ApplicationDbContext _db;
        private readonly ISmsSend _smsSend;

        public IdentityServices(ApplicationDbContext db, ISmsSend smsSend)
        {
            _db = db;
            _smsSend = smsSend;
        }


        public BaseFilterViewModels<ListUserForAdminViewModel> GetAllUserForAdmin(int pageIndex)
        {
            var userList = _db.Users.Where(x => x.IsDeleted == false).OrderByDescending(x => x.RegisterTimea);
            var take = 10;
            var homeManyPage = 2;
            var pager = PagingHelper.Pages(pageIndex, userList.Count(), take, homeManyPage);

            var result = userList.Select(x => new ListUserForAdminViewModel
            {
                ConfrimPhoneNumber = x.ConfrimPhoneNumber,
                CreateData = MyDateTime.GetShamsiDateFromGregorian(x.RegisterTimea, false),
                UserName = x.DisplayName,
                PhoneNumber = x.PhoneNumber,
                UserId = x.UserId,
            }).ToList();
            var OutPut = PagingHelper.Pagintion<ListUserForAdminViewModel>(result, pager);
            BaseFilterViewModels<ListUserForAdminViewModel> res = new BaseFilterViewModels<ListUserForAdminViewModel>
            {
                EndPage = pager.EndPage,
                Entities = OutPut,
                PageCount = pager.PageCount,
                StartPage = pager.StartPage,
                PageIndex = pager.PageIndex,
            };
            return res;
        }


        public int RegsisterByNumber(RegsisterViewModel model)
        {
            int StatusPhoneNumber = IsExistPhoneNumber(model.PhoneNumber);
            if (StatusPhoneNumber == 0)
            {
                User user = new User
                {
                    ConfrimCode = GeneraiteVerifyCode(),
                    Avatar = "Default.png",
                    ConfrimCodeCreateDate = DateTime.Now,
                    ConfrimPhoneNumber = false,
                    DisplayName = model.DisplayName,
                    IsDeleted = false,
                    Password = PasswordHelper.EncodePasswordMd5(model.Password),
                    PhoneNumber = model.PhoneNumber,
                    RegisterTimea = DateTime.Now,
                };
                _db.Users.Add(user);
                _db.SaveChanges();

            }
            if (StatusPhoneNumber == 1)
            {
                return 1;
            }
            if (StatusPhoneNumber == 2)
            {
                var user = _db.Users.FirstOrDefault(x => x.PhoneNumber == model.PhoneNumber);
                user.DisplayName = model.DisplayName;
                user.ConfrimCode = GeneraiteVerifyCode();
                user.ConfrimCodeCreateDate = DateTime.Now;
                user.Password = PasswordHelper.EncodePasswordMd5(model.Password);
                user.RegisterTimea = DateTime.Now;

                _db.Users.Update(user);
                _db.SaveChanges();
            }
            return -1;// ثبت نام انجام نشد
        }
        public int IsExistPhoneNumber(string phoneNumber)
        {
            var res = _db.Users.FirstOrDefault(x => x.PhoneNumber == phoneNumber);
            if (res == null)
            {
                return 0;// شماره وجود ندارد
            }
            if (res.ConfrimPhoneNumber == true)
            {
                return 1;// شماره قبلا ثبت نام کرده است
            }
            if (res.ConfrimPhoneNumber == false)
            {
                return 2;// شماره تایید نشده
            }
            return -1;// ثبت نام انجام نشد
        }
        public string GeneraiteVerifyCode()
        {
            Random random = new Random();
            return random.Next(12345, 99999).ToString();
        }
        public int GetUserStatusForLoginByPhoneNumber(string phoneNumber, string password)
        {
            var user = _db.Users.FirstOrDefault(x => x.PhoneNumber == phoneNumber);
            if (user == null)
            {
                return 0;
            }
            if (user.Password != PasswordHelper.EncodePasswordMd5(password))
            {
                return 3;// رمز درست نیست
            }
            if (user.ConfrimPhoneNumber == false)
            {
                return 2;
            }
            if (user.IsDeleted == true)
            {
                return 4;// کاربر حذف شده 
            }
            return 1;
        }

        public int GetUserIdByPhoneNumber(string phoneNumber)
        {
            return _db.Users.FirstOrDefault(i => i.PhoneNumber == phoneNumber).UserId;
        }
        public bool IsPhoneNumberExist(string phoneNumber)
        {
            return _db.Users.Any(x => x.PhoneNumber == phoneNumber);
        }
        public int ConfrimPhoneNumber(string phoneNumber, string code)
        {
            var user = _db.Users.FirstOrDefault(x => x.PhoneNumber == phoneNumber);
            if (user == null)
            {
                return 0;
            }
            if (user.ConfrimCodeCreateDate.AddMinutes(10) < DateTime.Now)
            {
                return -1;
            }
            if (user.ConfrimCode != code)
            {
                return -5;//کد تایید تطابق ندارد 
            }
            else
            {
                user.ConfrimCode = GeneraiteVerifyCode();
                user.ConfrimPhoneNumber = true;
                _db.Users.Update(user);
                _db.SaveChanges();
                return 5;// موفقیت امیز
            }
            return 1;
        }
        public int GetPhoneNumberForgotPass(string phoneNumber)
        {
            var user = _db.Users.FirstOrDefault(x => x.PhoneNumber == phoneNumber);
            if (user == null)
            {
                return 0;
            }
            if (user.IsDeleted == true)
            {
                return 4;
            }
            if (user.ConfrimPhoneNumber == false)
            {
                return 2;
            }
            else
            {
                user.ConfrimCode = GeneraiteVerifyCode();
                user.ConfrimCodeCreateDate = DateTime.Now;

                //bool res = _smsSend.SmsSend(2, user.PhoneNumber, user.DisplayName, user.ConfrimCode);

                _db.Users.Update(user);
                _db.SaveChanges();
                return 5;// موفقیت امیز
            }
            return -1;
        }

        public int ResetPasswordByMobile(RestPassViewModel model)
        {
            var user = _db.Users.FirstOrDefault(x => x.PhoneNumber == model.PhoneNumber);

            if (user == null)
            {
                return 0;
            }
            if (user.ConfrimCodeCreateDate.AddMinutes(15) < DateTime.Now)
            {
                return -1;
            }
            if (user.ConfrimCode != model.Code)
            {
                return -5;
            }
            else
            {
                user.ConfrimCode = GeneraiteVerifyCode();
                user.ConfrimCodeCreateDate = DateTime.Now;
                user.Password = PasswordHelper.EncodePasswordMd5(model.Password);

                _db.Users.Update(user);
                _db.SaveChanges();
                return 5;
            }

            return -1;
        }
        public bool CheckPermission(int permissionId, string phoneNumber)
        {
            int userId = _db.Users.FirstOrDefault(x => x.PhoneNumber == phoneNumber).UserId;
            List<int> roleId = _db.UserRoles.Where(x => x.UserId == userId).Select(x => x.RoleId).ToList();
            bool flag = false;
            if (!roleId.Any())
            {
                flag = false;
            }
            else
            {
                foreach (var role in roleId)
                {
                    foreach (var rolePermission in _db.RolePermissions.Where(x => x.RoleId == role).ToList())
                    {
                        if (rolePermission.PermissionId == permissionId) { flag = true; }
                    }
                }
            }
            return flag;
        }
        public string GetDisplayNameByPhoneNumber(string phoneNumber)
        {
            return _db.Users.FirstOrDefault(x => x.PhoneNumber == phoneNumber).DisplayName;
        }
        public UserPanelSideBarViewModel GetUserPanelSideBar(string phoneNumber)
        {
            return _db.Users.Where(x => x.PhoneNumber == phoneNumber).Select(x => new UserPanelSideBarViewModel
            {
                AvatarName = x.Avatar,
                DisplayName = x.DisplayName,
            }).FirstOrDefault();
        }
        public UserInfoViewModel GetUserInfo(string phoneNumber)
        {
            return _db.Users.Where(x => x.PhoneNumber == phoneNumber).Select(x => new UserInfoViewModel
            {
                DisplayName = x.DisplayName,
                RegisterTime = x.RegisterTimea,
                PhoneNumber = phoneNumber,
            }).FirstOrDefault();
        }
    }
}
