using System;
using System.Configuration;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading;

namespace Dev.Common.Develop
{
    public static class SequenceNoUtils
    {
        #region Private Fields

        private static readonly char[] characters = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789".ToCharArray();
        private static readonly object lockObject = new object();
        private static char machineKey;

        #endregion Private Fields

        #region Public Methods

        static SequenceNoUtils()
        {
            try
            {
                machineKey = ConfigurationManager.AppSettings.Get("AppKey").First();
            }
            catch (Exception)
            {
                machineKey = ' ';
            }
        }

        public static string GenerateNo(char orderType)
        {
            DateTime currentTime;
            lock (lockObject)
            {
                currentTime = DateTime.Now;
                Thread.Sleep(new TimeSpan(0, 0, 0, 0, 3));
            }

            int year = currentTime.Year - 2013;
            if (year > 35 || year < 0)
            {
                year = 0;
            }
            int month = currentTime.Month;
            int day = currentTime.Day;
            int hour = currentTime.Hour;

            string yearChar = characters[year].ToString(CultureInfo.InvariantCulture);
            string monthChar = characters[month].ToString(CultureInfo.InvariantCulture);
            string dayChar = characters[day].ToString(CultureInfo.InvariantCulture);
            string hourChar = characters[hour].ToString(CultureInfo.InvariantCulture);

            string time = DateTime.Now.ToString("mmssffff");
            StringBuilder sb = new StringBuilder();
            sb.Append(orderType).Append(machineKey).Append(yearChar).Append(monthChar).Append(dayChar).Append(hourChar).Append(time);
            return sb.ToString().Replace(" ","");
        }

        #endregion Public Methods
    }
}