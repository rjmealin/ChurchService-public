using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChurchService.Library.Models.Users
{
    public class LoginModel
    {   
        public string Email { get; set; }
        public string Password { get; set; }
        public bool ShouldSerializePassword()
        {
            return false;
        }










        /// <summary>
        ///     Validates the login.
        /// </summary>
        /// 
        /// <param name="model">Login information.</param>
        /// <param name="fullname">Full name of the user.</param>
        /// <param name="id">User id</param>
        /// <param name="lastRole">Last role used. 0 for owner, 1 for pro</param>
        /// <returns>Returns a flag to indicate if the user is valid or not.</returns>
        /// 
        public LoginValidatedModel ValidateLogin(LoginModel model)
        {
            using (var db = new SQL.ChurchServiceContext())
            {
                var userRecord = db.Users.FirstOrDefault(x => x.Email == model.Email);

                var validationModel = new LoginValidatedModel();
                validationModel.Validated = false;

                if (userRecord == null)
                    return (validationModel);

                //TODO: Implement system for email verifications
                //if (!userRecord.EmailVerified)
                //    throw new UnauthorizedAccessException("Your Email has not been verified");


                bool verified = UserHash.VerifyPassword(userRecord.Salt.ToString(), userRecord.Password, model.Password);

                if (verified)
                {

                    validationModel.Validated = true;
                    validationModel.UserId = userRecord.UserId;
                    validationModel.ChurchId = userRecord.ChurchId;
                    validationModel.FullName = userRecord.FirstName + " " + userRecord.LastName;                    
                    validationModel.EmailVerified = userRecord.EmailVerified;
                    validationModel.IsAdmin = userRecord.IsAdmin;

                    return (validationModel);
                }
                else
                    return (validationModel);
            }
        }
    }
}
