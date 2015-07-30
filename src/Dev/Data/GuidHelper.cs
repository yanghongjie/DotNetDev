using System;

namespace Dev.Data
{
    /// <summary>
    ///     GUID类型操作类
    /// </summary>
    public static class GuidHelper
    {
        private static readonly long EpochMilliseconds = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc).Ticks/
                                                         10000L;

        /// <summary>
        ///     返回Guid用于数据库操作，特定的时间代码可以提高检索效率
        /// </summary>
        /// <returns>Guid</returns>
        public static Guid New()
        {
            byte[] guidArray = Guid.NewGuid().ToByteArray();
            var dtBase = new DateTime(1900, 1, 1);
            DateTime dtNow = DateTime.Now;
            //获取用于生成byte字符串的天数与毫秒数
            var days = new TimeSpan(dtNow.Ticks - dtBase.Ticks);
            var msecs = new TimeSpan(dtNow.Ticks - (new DateTime(dtNow.Year, dtNow.Month, dtNow.Day).Ticks));
            //转换成byte数组
            //注意SqlServer的时间计数只能精确到1/300秒
            byte[] daysArray = BitConverter.GetBytes(days.Days);
            byte[] msecsArray = BitConverter.GetBytes((long) (msecs.TotalMilliseconds/3.333333));

            //反转字节以符合SqlServer的排序
            Array.Reverse(daysArray);
            Array.Reverse(msecsArray);

            //把字节复制到Guid中
            Array.Copy(daysArray, daysArray.Length - 2, guidArray, guidArray.Length - 6, 2);
            Array.Copy(msecsArray, msecsArray.Length - 4, guidArray, guidArray.Length - 4, 4);
            return new Guid(guidArray);
        }

        /// <summary>
        ///     从SQL Server 返回的Guid中生成时间信息
        /// </summary>
        public static DateTime GetDateFrom(Guid id)
        {
            var baseDate = new DateTime(1900, 1, 1);
            var daysArray = new byte[4];
            var msecsArray = new byte[4];
            byte[] guidArray = id.ToByteArray();

            // Copy the date parts of the guid to the respective byte arrays. 
            Array.Copy(guidArray, guidArray.Length - 6, daysArray, 2, 2);
            Array.Copy(guidArray, guidArray.Length - 4, msecsArray, 0, 4);

            // Reverse the arrays to put them into the appropriate order 
            Array.Reverse(daysArray);
            Array.Reverse(msecsArray);

            // Convert the bytes to ints 
            int days = BitConverter.ToInt32(daysArray, 0);
            int msecs = BitConverter.ToInt32(msecsArray, 0);

            DateTime date = baseDate.AddDays(days);
            date = date.AddMilliseconds(msecs*3.333333);

            return date;
        }

        /// <summary>
        ///     获取Guid去除-格式字符串
        /// </summary>
        public static string NewGuidString()
        {
            return NewSequentialId().ToString().Replace("-", "");
        }

        /// <summary>
        ///     Creates a sequential GUID according to SQL Server's ordering rules.
        /// </summary>
        public static Guid NewSequentialGuid()
        {
            return NewSequentialId();
        }

        /// <summary>
        ///     Creates a sequential GUID according to SQL Server's ordering rules.
        /// </summary>
        private static Guid NewSequentialId()
        {
            // This code was not reviewed to guarantee uniqueness under most conditions, nor
            // completely optimize for avoiding page splits in SQL Server when doing inserts from
            // multiple hosts, so do not re-use in production systems.
            byte[] guidBytes = Guid.NewGuid().ToByteArray();

            // get the milliseconds since Jan 1 1970
            byte[] sequential = BitConverter.GetBytes((DateTime.Now.Ticks/10000L) - EpochMilliseconds);

            // discard the 2 most significant bytes, as we only care about the milliseconds
            // increasing, but the highest ones should be 0 for several thousand years to come (non-issue).
            if (BitConverter.IsLittleEndian)
            {
                guidBytes[10] = sequential[5];
                guidBytes[11] = sequential[4];
                guidBytes[12] = sequential[3];
                guidBytes[13] = sequential[2];
                guidBytes[14] = sequential[1];
                guidBytes[15] = sequential[0];
            }
            else
            {
                Buffer.BlockCopy(sequential, 2, guidBytes, 10, 6);
            }

            return new Guid(guidBytes);
        }
    }
}