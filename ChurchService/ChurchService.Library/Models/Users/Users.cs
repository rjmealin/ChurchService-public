using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;
using Microsoft.EntityFrameworkCore;
using ChurchService.Library.SQL;
using ChurchService.Library.Utilities;

namespace ChurchService.Library.Models.Users
{
    public static class Users
    {


        static HashAlgorithm sha = SHA256.Create();





        /// <summary>
        ///     Creates an sms verificaiton record
        /// </summary>
        /// <param name="userId">The userId to create the verificaiton for</param>
        /// 
        public static async Task CreateSmsVerificaiton(Guid userId)
        {
            using (var db = new SQL.ChurchServiceContext())
            {

                var user = await db.Users.FindAsync(userId);

                if (user == null)
                    throw new ArgumentException("No User with Id was found!");

                var verifications = await db.UserSmsVerifications.Where(v => v.UserId == userId).ToListAsync();

                verifications.ForEach(v => v.Active = false);

                var rand = new Random();

                var code = rand.Next(100000, 999999);


                var smsVerification = new SQL.UserSmsVerification()
                {
                    UserId = userId,
                    PhoneNumber = user.Phone,
                    Verified = false,
                    DateCreated = DateTime.UtcNow,
                    Expiration = DateTime.UtcNow.AddHours(2),
                    Active = true,
                    VerificationCode = code,
                };

                db.UserSmsVerifications.Add(smsVerification);

                await db.SaveChangesAsync();

                await TextNotifications.SendSmsVerificationAsync(user.Phone, code);
            }
        }










        /// <summary>
        ///     Veryifies the phone number for the user
        /// </summary>
        /// <param name="verificationId"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentException"></exception>
        /// <exception cref="ApplicationException"></exception>
        public static async Task VerifySmsNumberAsync(Guid userId, int verifCode)
        {
            using (var db = new ChurchServiceContext())
            {

                var userVerifications = await db.UserSmsVerifications.Where(v => v.UserId == userId && v.Expiration > DateTime.Now).ToListAsync();

                var verification = userVerifications.Where(v => v.VerificationCode == verifCode).FirstOrDefault();

                if (verification == null)
                    throw new ArgumentException("Wrong Code Entered!");
                if (verification.Active == false)
                    throw new ApplicationException("This verification is no longer active");
                if (verification.Expiration < DateTime.UtcNow)
                    throw new ApplicationException("This Verification has Expired");
                if (verification.Verified == true)
                    throw new ApplicationException("This Number has already been verified");

                var user = await db.Users.FindAsync(userId);

                if (user == null)
                    throw new ArgumentException("No User was found with this ID");

                user.PhoneVerified = true;

                verification.Verified = true;

                await db.SaveChangesAsync();

                await TextNotifications.SendSmsMessageAsync(verification.PhoneNumber, "Your number has been verified!");
            }
        }










        /// <summary>
        ///     Creates a new email verification record.
        /// </summary>
        /// 
        /// <returns>
        ///     Returns the guid for the created email verification record.
        /// </returns>
        /// 
        /// <param name="userEmail">The email address of the desired user.</param>
        /// <param name="expirationTime">The amount of time to keep the verification active, in milliseconds.</param>
        /// 
        public static async Task<Guid> CreateEmailVerification(string userEmail, double expirationTime)
        {
            using (var db = new SQL.ChurchServiceContext())
            {
                var user = await db.Users.FirstOrDefaultAsync(f => f.Email == userEmail);

                if (user == null)
                    throw new Exception("User Email not found");

                return await CreateUserEmailVerification(user, expirationTime);
            }
        }










        /// <summary>
        ///     Creates a new email verification record.
        /// </summary>
        /// 
        /// <returns>
        ///     Returns the guid for the created email verification record.
        /// </returns>
        /// 
        /// <param name="userId">The database id of the user to attach to the email verification.</param>
        /// <param name="expirationTime">The amount of time to keep the verification active, in milliseconds.</param>
        /// 
        public static async Task<(Guid emailVerificationId, string userEmail)> CreateEmailVerification(Guid userId, double expirationTime)
        {
            using (var db = new SQL.ChurchServiceContext())
            {
                var user = await db.Users.FindAsync(userId);

                return (await CreateUserEmailVerification(user, expirationTime), user.Email);
            }
        }










        /// <summary>
        ///     Creates the a email verification to upload to the Database
        /// </summary>
        /// <param name="user">The database id of the user to attach the email verification</param>
        /// <param name="expirationTime">The amount of time to keep the verification active, in miliseconds</param>
        /// <returns></returns>
        private static async Task<Guid> CreateUserEmailVerification(SQL.User user, double expirationTime)
        {
            using (var db = new SQL.ChurchServiceContext())
            {

                var emailVerification = new SQL.UserEmailVerification()
                {
                    UserId = user.UserId,
                    DateCreated = DateTime.UtcNow,
                    Expiration = DateTime.UtcNow.AddHours(expirationTime),
                    Email = user.Email,
                    Verified = false,
                    Active = true,
                };

                var dbVerifications = await db.UserEmailVerifications.Where(v => v.UserId == user.UserId && v.Active == true).ToListAsync();

                dbVerifications.ForEach(e => e.Active = false);

                db.UserEmailVerifications.Add(emailVerification);
                await db.SaveChangesAsync();

                return emailVerification.UserEmailVerificationId;
            }
        }










        /// <summary>
        ///     Verifies a user's email address.
        /// </summary>
        /// 
        /// <remarks>
        ///     Throws a key not found exception if the given email 
        ///     verification guid is not found.
        ///     Throws an invalid operation exception if the verification has 
        ///     expired or has already been used.
        /// </remarks>
        /// 
        /// <param name="userEmailVerificationId">The verification guid to opperate against.</param>
        /// 
        public static async Task VerifyUserEmail(Guid userEmailVerificationId)
        {
            using (var db = new SQL.ChurchServiceContext())
            {
                var emailVerification = await db.UserEmailVerifications.FirstOrDefaultAsync(f => f.UserEmailVerificationId == userEmailVerificationId);

                if (emailVerification == null)
                    throw new KeyNotFoundException("Invalid validation guid.");

                else if (emailVerification.Expiration < DateTime.UtcNow)
                    throw new ArgumentException("The email verification is expired");
                else if (!emailVerification.Active)
                    throw new ArgumentException("This Email has already been verified.");
                else if (emailVerification.Verified)
                    throw new ArgumentException("This Email has already been verified.");

                emailVerification.Active = false;

                var user = await db.Users.FindAsync(emailVerification.UserId);

                user.EmailVerified = true;

                await db.SaveChangesAsync();
            }
        }










        /// <summary>
        ///     Creates a new password reset request.
        /// </summary>
        /// 
        /// <returns>
        ///     Returns the request id.
        /// </returns>
        /// 
        /// <param name="email">The email address of the desired user.</param>
        /// <param name="expirationTime">The expiration time, in milliseconds.</param>
        /// 
        public static async Task<Guid> CreateResetPasswordRequestAsync(string email, double expirationTime)
        {
            using (var db = new SQL.ChurchServiceContext())
            {
                var user = await db.Users.FirstOrDefaultAsync(f => f.Email == email);

                if (user == null)
                    throw new ApplicationException("User Email Not found!");

                var now = DateTime.UtcNow;
                var activeRequests = await db.UserPasswordResets.Where(f => f.Email == email && f.Active == true && f.Expiration > now).ToListAsync();

                activeRequests.ForEach(r => r.Active = false);

                var passwordReset = new SQL.UserPasswordReset()
                {
                    UserId = user.UserId,
                    DateCreated = now,
                    Expiration = now.AddHours(expirationTime),
                    Email = email,
                    Active = true
                };

                db.UserPasswordResets.Add(passwordReset);
                await db.SaveChangesAsync();

                return passwordReset.UserPasswordResetId;
            }
        }










        /// <summary>
        ///     Uses the password reset request to validate and, if validated, 
        ///     update the user password.
        /// </summary>
        /// 
        /// <param name="passwordResetId">The reset request id.</param>
        /// <param name="password">The new user password.</param>
        /// 
        public static async Task ResetUserPassword(PasswordResetModel model)
        {
            using (var db = new ChurchServiceContext())
            {
                var resetRequest = await db.UserPasswordResets.FirstOrDefaultAsync(f => f.UserPasswordResetId == model.PasswordResetId);

                if (resetRequest == null)
                    throw new KeyNotFoundException("Password Reset not found!");

                if (resetRequest.Active == false)
                    throw new ApplicationException("This password reset is not active!");

                if (DateTime.UtcNow > resetRequest.Expiration)
                    throw new ApplicationException("This Password reset is expired!");

                resetRequest.Active = false;

                var user = await db.Users.FindAsync(resetRequest.UserId);

                UserHash.SetUserPassword(user, model.Password);

                await db.SaveChangesAsync();


            }
        }

    }
}
