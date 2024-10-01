using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Accapt.Core.Generator
{
    public class CodeGeneratorForTwoFactory
    {
        public static void Main()
        {
            int verificationCode = GenerateSecureRandomNumber();
            Console.WriteLine($"Your verification code is: {verificationCode}");
        }

        public static int GenerateSecureRandomNumber()
        {
            using (var rng = new RNGCryptoServiceProvider())
            {
                byte[] randomBytes = new byte[4];
                rng.GetBytes(randomBytes);
                int randomNumber = Math.Abs(BitConverter.ToInt32(randomBytes, 0)) % 100000;
                return randomNumber;
            }
        }
    }
}
