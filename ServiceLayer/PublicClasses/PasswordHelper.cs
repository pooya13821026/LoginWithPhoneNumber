using System.Security.Cryptography;
using System.Text;

namespace ServiceLayer.PublicClasses
{
    public static class PasswordHelper
    {
        public static string EncodePasswordMd5(string pass)
        {
            byte[] originaBytes;
            byte[] encodeBytes;
            MD5 md5;
            md5 = new MD5CryptoServiceProvider();
            originaBytes = Encoding.Default.GetBytes(pass);
            encodeBytes = md5.ComputeHash(originaBytes);
            return BitConverter.ToString(encodeBytes);
        }
    }
}
