using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Security.Cryptography;

namespace VarastoApi.Backend {
    //Salasanat hashataan
    public static class PBKDF2Hash {
        //Source: https://github.com/sahgilbert/hashing-passwords-md5-bcrypt-pbkdf2-csharp-dotnetcore/blob/master/SimonGilbert.Blog/CryptographyService.cs
        public static string HashPasswordUsingPBKDF2(string password) {
            var rfc2898DeriveBytes = new Rfc2898DeriveBytes(password, 32) {
                IterationCount = 10000 //Algoritmin hitaus, kannattaa pitää tarpeeksi hitaana turvallisuussyistä.
            };

            byte[] hash = rfc2898DeriveBytes.GetBytes(20);

            byte[] salt = rfc2898DeriveBytes.Salt;

            return Convert.ToBase64String(salt) + "|" + Convert.ToBase64String(hash); //kirjoittaa stringiin siis "suola|hash"
        }
    }
}