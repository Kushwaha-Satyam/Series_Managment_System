using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace SeriesManagment.BAL
{
    public class Crypto
    {
        public static string Encrypt(string clearText)
        {
            byte[] bytes = Encoding.UTF8.GetBytes(clearText);
            return HttpServerUtility.UrlTokenEncode(bytes);
        }

        public static string Decrypt(string encryptedText)
        {
            byte[] bytes = HttpServerUtility.UrlTokenDecode(encryptedText);
            return Encoding.UTF8.GetString(bytes);
        }
    }
}
