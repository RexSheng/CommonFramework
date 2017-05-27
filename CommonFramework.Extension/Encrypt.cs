using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonFramework.Extension
{
    public class Encrypt
    {
        /// <summary>
        /// 将字符串进行MD5加密
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string GetMD5Str(string str)
        {
            StringBuilder sb = new StringBuilder();

            System.Security.Cryptography.MD5 md5 = System.Security.Cryptography.MD5.Create();

            byte[] s;
            s = new byte[0];
            if (str != null)
            {

                s = md5.ComputeHash(Encoding.UTF8.GetBytes(str));
            }
            for (int i = 0; i < s.Length; i++)
            {
                sb.Append(s[i].ToString("X2"));
            }

            //md5Str就是最后得到加密后的字符串
            string md5Str = sb.ToString();

            return md5Str;
        }
    }
}
