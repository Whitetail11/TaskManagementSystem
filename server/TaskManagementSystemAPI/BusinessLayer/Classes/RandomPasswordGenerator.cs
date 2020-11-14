using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Classes
{
    public static class RandomPasswordGenerator
    {
        public static string GenerateRandomPassword(int passwordSize = 8)
        {
            string[] charSets = new string[] { 
                "ABCDEFGHIJKLMNOPQRSTUVWXYZ",
                "abcdefghijklmnopqursuvwxyz", 
                "1234567890" 
            };

            char[] password = new char[passwordSize];
            Random random = new Random();

            for (var i = 0; i < passwordSize; i++)
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
