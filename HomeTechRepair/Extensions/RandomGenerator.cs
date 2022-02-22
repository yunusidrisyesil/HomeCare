using System;
using System.Text;

namespace HomeTechRepair.Extensions
{
    public static class RandomGenerator
    {
        public static string CreatePassword(int length)
        {
            const string valid = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890";
            StringBuilder res = new StringBuilder();
            Random rnd = new Random();
            while (0 < length--)
            {
                res.Append(valid[rnd.Next(valid.Length)]);
            }
            return res.ToString();
        }
        public static string CreateUsername(int length)

        {
            const string valid = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890";
            StringBuilder username = new StringBuilder();
            Random rnd = new Random();
            while (0 < length--)
            {
                username.Append(valid[rnd.Next(valid.Length)]);
            }
            return username.ToString();
        }





    }
}
