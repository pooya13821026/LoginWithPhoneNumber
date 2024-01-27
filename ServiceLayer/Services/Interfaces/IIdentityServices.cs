using ServiceLayer.ViewModels.BaseViewModels;
using ServiceLayer.ViewModels.IdentityViewModels;

namespace ServiceLayer.Services.Interfaces
{
    public interface IIdentityServices
    {
        BaseFilterViewModels<ListUserForAdminViewModel> GetAllUserForAdmin(int pageIndex);
		int RegsisterByNumber(RegsisterViewModel model);
        int IsExistPhoneNumber(string phoneNumber);
        int GetUserStatusForLoginByPhoneNumber(string phoneNumber, string password);
        int GetUserIdByPhoneNumber(string phoneNumber);
        bool IsPhoneNumberExist(string phoneNumber);
        int ConfrimPhoneNumber(string phoneNumber, string code);
        int GetPhoneNumberForgotPass(string phoneNumber);
        int ResetPasswordByMobile(RestPassViewModel model);
        bool CheckPermission(int permissionId, string phoneNumber);
        string GetDisplayNameByPhoneNumber(string phoneNumber);
        UserPanelSideBarViewModel GetUserPanelSideBar(string phoneNumber);
        UserInfoViewModel GetUserInfo(string phoneNumber);
    }
}
