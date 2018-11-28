using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;

namespace ThePublicNoteBook.Models
{
    public class MySecurity
    {
        public static string EncryptPass(string pass)
        {
            SHA256 sha = SHA256Managed.Create();
            string signdata = "";
            byte[] result = sha.ComputeHash(Encoding.Unicode.GetBytes(pass + signdata));

            return BitConverter.ToString(result).Replace("-", "").ToLower();

        }
    }
}