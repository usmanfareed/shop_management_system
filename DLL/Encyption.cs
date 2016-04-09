using System;
using System.Security.Cryptography;
using System.Text;

namespace Shop_Management_System
{
    public static class Encyption
    {
        public static string GetEncrypted(string text)
        {
            return _Encyption(text);

        }

        private static string _Encyption(string text)
        {
            return Convert.ToBase64String(new SHA256Managed().ComputeHash(Encoding.UTF8.GetBytes(text))); ;

        }

    }
}