using System;
using System.Security.Cryptography;
using System.Text;

namespace LivestreamingE_commercePlatform.Helper
{
    public static class StringHelper
    {
        /// <summary>
        /// netcore下的实现MD5加密
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string MD5Hash(string input)
        {
            using (var md5 = MD5.Create())
            {
                var result = md5.ComputeHash(Encoding.ASCII.GetBytes(input));
                var strResult = BitConverter.ToString(result);
                return strResult.Replace("-", "");
            }
        }
    }
}
