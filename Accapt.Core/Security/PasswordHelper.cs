using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace AccaptFullyVersion.Core.Secutiry
{
    public class PasswordHelper
    {
        public static string EncodePasswordMd5(string password)
        {
            Byte[] originalPassword;
            Byte[] encodePassword;

            MD5 md5;

            md5 = new MD5CryptoServiceProvider();
            originalPassword = ASCIIEncoding.Default.GetBytes(password);
            encodePassword = md5.ComputeHash(originalPassword);

            return BitConverter.ToString(encodePassword);
        }
    }
}
