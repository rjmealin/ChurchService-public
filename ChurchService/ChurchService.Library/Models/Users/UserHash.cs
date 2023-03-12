using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using ChurchService.Library.SQL;

namespace ChurchService.Library.Models.Users
{
    public static class UserHash
    {
        static HashAlgorithm sha = SHA256.Create();
        /// <summary>
        ///     Changes the user password into a hashed password.
        /// </summary>
        /// 
        /// <param name="userRecord">User record.</param>
        /// <param name="password">User password.</param>
        /// <returns>A hashed password string. </returns>
        private static string HashPashword(string salt, string password)
        {
            var temp = password + salt.ToLower();
            byte[] check = sha.ComputeHash(System.Text.Encoding.UTF8.GetBytes(temp));
            // convert to a hex string
            StringBuilder sb = new StringBuilder();
            foreach (var x in check)
            {
                sb.AppendFormat("{0:X2}", x);
            }

            return sb.ToString();
        }










        /// <summary>
        ///     Sets the user password with random guid and incoming password from the model
        /// </summary>
        /// <param name="user"></param>
        /// <param name="modelPassword"></param>
        /// <returns></returns>
        public static SQL.User SetUserPassword(SQL.User user, string modelPassword)
        {
            var salt = Guid.NewGuid();

            user.Salt = salt;

            user.Password = HashPashword(salt.ToString(), modelPassword);
            return user;
        }










        /// <summary>
        ///     Verifies the incoming user password with the hashed version of it in the database
        /// </summary>
        /// <param name="salt"></param>
        /// <param name="dbPassword"></param>
        /// <param name="modelPassword"></param>
        /// <returns></returns>
        public static bool VerifyPassword( string salt, string dbPassword, string modelPassword) 
        {

            if (HashPashword(salt, modelPassword) == dbPassword)
                return true;
            else
                return false;
        }

    }
}
