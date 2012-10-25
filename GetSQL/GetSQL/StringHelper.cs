using System;
using System.Collections.Generic;
using System.Text;

namespace GetSQL
{
    public class StringHelper
    {
        /// <summary>
        /// SQL语句“ ' ”转换, 并加入引号包围。
        /// </summary>
        /// <param name="str"></param>
        public static string QuotedString(string str)
        {
            return "'" + str.Replace("'", "''") + "'";
        }
    }
}
