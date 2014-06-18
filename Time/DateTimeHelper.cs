using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace R2.Helper.Time
{
    public class DateTimeHelper
    {
        /// <summary>
        /// 将日期转化为常规字符串，精确1/10000000秒，通常用来作为唯一ID
        /// </summary>
        /// <param name="dateTime"></param>
        /// <returns></returns>
        public static String ConvertToIDString(DateTime dateTime)
        {
            return dateTime.ToString("yyyyMMddHHmmssfffffff");
        }
    }
}
