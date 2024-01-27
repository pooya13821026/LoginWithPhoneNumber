namespace ServiceLayer.PublicClasses
{
    public interface ISmsSend
    {
    }
    public class SmsSend : ISmsSend
    {
        public bool SendSms(int type, string PhonNumber, string DisPlayName)
        {
            string patternSms = "";
            string displayName = DisPlayName.Replace("", "_");
            string fromNumber = "";
            string url = "";
            if (type == 1)// register
            {
                patternSms = "";
            }
            if (type == 2)//forgot pass
            {
                patternSms = "";
            }
            return false;
        }
    }
}
