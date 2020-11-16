using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Classes
{
    public static class RandomPasswordGenerator
    {
        public static string GenerateRandomPassword(int passwordLength = 8)
        {
            if (passwordLength < 8)
            {
                throw new ArgumentException("Password length must be greater than or equal to 8.");
            }

            string[] charSets = new string[] { 
                "ABCDEFGHIJKLMNOPQRSTUVWXYZ",
                "abcdefghijklmnopqursuvwxyz", 
                "1234567890" 
            };

            char[] password = new char[passwordLength];
            Random random = new Random();

            for (var i = 0; i < passwordLength; i++)
            {
                if (i < charSets.Length)
                {
                    password[i] = charSets[i][random.Next(charSets[i].Length)];
                    continue;
                }
                var charSet = charSets[random.Next(charSets.Length)];
                password[i] = charSet[random.Next(charSet.Length)];
            }

            return new string(password);
        }
    }
}
