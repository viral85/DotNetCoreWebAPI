using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;

namespace MyTestProjectAPI.ServiceInstaller
{
    /// <summary>
    /// Static class for static methods
    /// </summary>
    public static class Utilities
    { /// <summary>
      /// 
      /// </summary>
      /// <param name="msg"></param>
      /// <param name="number"></param>
      /// <returns></returns>
        public static int SendSMS(string msg, string number)
        {
            int result = 0;
            String message = HttpUtility.UrlEncode(msg);
            string smsapikey = ConfigurationManager.AppSettings["TLAPIKey"];

            using (var wb = new WebClient())
            {
                byte[] response = wb.UploadValues("https://api.textlocal.in/send/", new NameValueCollection()
                {
                {"apikey" , smsapikey},
                {"numbers" , "91"+number},
                {"message" , message},
                {"sender" , "TXTLCL"}
                });

                //{"errors":
                //[{"code":192,"message":"Messages can only be sent between 9am to 9pm as restricted by TRAI NCCP regulation"}],
                //"status":"failure"}
                string temp = System.Text.Encoding.UTF8.GetString(response);

                if (temp.Contains("failure"))
                {
                    result = 0;
                }
                else
                {
                    result = 1;
                }

            }


            return result;
        }

        /// <summary>
        /// Method for creating random password.
        /// </summary>
        /// <returns></returns>
        public static string CreateRandomPassword()
        {
            string allowedChars = "abcdefghijkmnopqrstuvwxyzABCDEFGHJKLMNOPQRSTUVWXYZ0123456789!@$";
            char[] chars = new char[8];
            Random rd = new Random();

            for (int i = 0; i < 8; i++)
            {
                chars[i] = allowedChars[rd.Next(0, allowedChars.Length)];
            }

            return new string(chars);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static string CreateRandomTransactionId()
        {
            string allowedChars = "abcdefghijkmnopqrstuvwxyzABCDEFGHJKLMNOPQRSTUVWXYZ0123456789";
            char[] chars = new char[6];

            Random rd = new Random();

            for (int i = 0; i < 6; i++)
            {
                chars[i] = allowedChars[rd.Next(0, allowedChars.Length)];
            }

            string s1 = new string(chars);
            Int64 s2 = Convert.ToInt64(DateTime.Now.ToString("HHmmss"));
            string s3 = s1.ToString() + "" + s2.ToString();

            return s3.ToUpper();
        }
    }
}
