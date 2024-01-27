using System.Globalization;

namespace ServiceLayer.PublicClasses
{
	public static class MyDateTime
	{

		public static string GetShamsiDateFromGregorian(this DateTime value, bool time = true)
		{
			PersianCalendar pc = new PersianCalendar();
			var year = pc.GetYear(value);
			var month = pc.GetMonth(value);
			var day = pc.GetDayOfMonth(value);
			var hour = pc.GetHour(value);
			var minute = pc.GetMinute(value);
			try
			{
				if (time)
				{
					return year.ToString() + "/" + month.ToString("00") + "/" + day.ToString("00") + "-" + hour.ToString("00") + ":" + minute.ToString("00");
				}
				else
				{
					return year.ToString() + "/" + month.ToString("00") + "/" + day.ToString("00");
				}
			}
			catch
			{
				return "متاسفانه تاریخ قابل بازیابی نبود";
			}
		}


		public static int GetMonthOfGeogrianDateInShamsi(DateTime date)
		{
			PersianCalendar pc = new PersianCalendar();
			return pc.GetMonth(date);
		}

		public static int GetYearOfGeogrianDateInShamsi(DateTime date)
		{
			PersianCalendar pc = new PersianCalendar();
			return pc.GetYear(date);
		}

		public static int GetDayOfMonthOfGeogrianDateInShamsi(DateTime date)
		{
			PersianCalendar pc = new PersianCalendar();
			return pc.GetDayOfMonth(date);
		}

		public static int GetYearOfGeogrianDateInShamsiEX(this DateTime? value)
		{
			DateTime date = (DateTime)value;
			return GetYearOfGeogrianDateInShamsi(date);
		}

		public static int GetMonthOfGeogrianDateInShamsiEX(this DateTime? value)
		{
			DateTime date = (DateTime)value;
			return GetDayOfMonthOfGeogrianDateInShamsi(date);
		}

		public static DateTime GetGeogranDateFromShamsiMonthAndYear(int year, int month)
		{
			DateTime date = new DateTime(year, month, 1, new PersianCalendar());
			return date;
		}

		public static DateTime GetGeogranDateMines2MonthsFromShamsiMonthAndYear(int year, int month)
		{
			int myMonth = month;
			int myYear = year;

			if (month > 2)
			{
				myMonth = month - 2;
			}
			else
			{
				myYear = year - 1;
				if (month == 1)
				{
					myMonth = 11;
				}
				else if (month == 2)
				{
					myMonth = 12;
				}
			}

			DateTime date = new DateTime(myYear, myMonth, 1, new PersianCalendar());
			return date;
		}

		public static DateTime GetGeogranDatePlus2MonthsFromShamsiMonthAndYear(int year, int month)
		{
			int myMonth = month;
			int myYear = year;

			if (month < 11)
			{
				myMonth = month + 2;
			}
			else
			{
				myYear = year + 1;
				if (month == 11)
				{
					myMonth = 1;
				}
				else if (month == 12)
				{
					myMonth = 2;
				}
			}

			DateTime date = new DateTime(myYear, myMonth, 1, new PersianCalendar());
			return date;
		}

		public static DateTime GetDateTimeFromShamsiString(string shamsiDate)
		{
			string[] startDateArray = shamsiDate.Split("/");
			DateTime startDateConverted = new DateTime(int.Parse(startDateArray[0].convertToEn()), int.Parse(startDateArray[1].convertToEn()), int.Parse(startDateArray[2].convertToEn()), new PersianCalendar());
			return startDateConverted;
		}

	}

	public static class ConvertToEnDijit
	{
		public static string convertToEn(this string input)
		{
			string[] persian = new string[10] { "۰", "۱", "۲", "۳", "۴", "۵", "۶", "۷", "۸", "۹" };

			for (int j = 0; j < persian.Length; j++)
				input = input.Replace(persian[j], j.ToString());

			return input;

		}
	}
}