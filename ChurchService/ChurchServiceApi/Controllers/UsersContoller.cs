using Microsoft.AspNetCore.Mvc;
using ChurchService.Library;
using ChurchService.Library.Models.Users;
using ChurchServiceApi.Security;
using ChurchService.Library.Models.Members;
using ChurchService.Library.Models.Churches;

namespace ChurchServiceApi.Controllers
{
    [ApiController]
    [Route("Users")]
    public class UsersContoller : BaseController
    {





        /// <summary>
        ///     Perform user login
        /// </summary>
        /// 
        /// <param name="viewModel">L&P model</param>
        /// 
        [HttpPost]
        [Route("login")]
        public IActionResult LoginUser(LoginModel viewModel)
        {
            try
            {
                //var test = CurrentJsonWebToken.ChurchId;

                var model = viewModel.ValidateLogin(viewModel);

                if (!model.Validated)
                    throw new UnauthorizedAccessException();

                return Ok(JsonWebToken.GetUserToken( model.UserId, model.FullName, model.EmailVerified, model.IsAdmin, model.ChurchId));
            }
            catch (Exception ex)
            {
                LogException(ex, viewModel);
                return ReturnException(ex);
            }
        }





















        /// <summary>
        ///     Activates the given user verification id.
        /// </summary>
        /// <param name="id">The verification id.</param>
        /// 
        [HttpGet]
        [Route("verify-email/{id}")]
        public async Task<IActionResult> VerifyUserEmail(Guid id)
        {
            try
            {
                
                await Users.VerifyUserEmail(id);

                return NoContent();

            }
            catch (Exception ex)
            {
                LogException(ex);
                return ReturnException(ex);
            }
        }









        /// <summary>
        ///     Sends a new sms verification per user request
        /// </summary>
        /// 
        [HttpPost]
        [Route("send-sms-verification")]
        public async Task<IActionResult> SendSmsVerification()
        {
            try
            {
             
                await Users.CreateSmsVerificaiton(CurrentJsonWebToken.UserId);

                return NoContent();

            }
            catch (Exception ex)
            {
                LogException(ex);
                return ReturnException(ex);
            }
        }










        /// <summary>
        ///     Verifies the the Sms notifications for the user attached to the sms verification
        /// </summary>
        /// <param name="id">The sms verification Id</param>
        /// 
        [HttpPatch]
        [Route("verify-sms/{code}")]
        public async Task<IActionResult> VerifySmsNotifications(int code)
        {
            try
            {

                await Users.VerifySmsNumberAsync(CurrentJsonWebToken.UserId, code);

                return NoContent();

            }
            catch (Exception ex)
            {
                LogException(ex);
                return ReturnException(ex);
            }
        }











        /// <summary>
        ///     Sends a new email verification to the user email
        /// </summary>
        /// 
        /// <returns>
        ///     Returns the user id.
        /// </returns>
        /// <param name="viewModel">Model with new user information</param>
        [HttpPost]
        [Route("send-email-verification")]
        public async Task<IActionResult> SendVerificationEmail()
        {
            try
            {


                var (verificationId, email) = await Users.CreateEmailVerification(CurrentJsonWebToken.UserId, Resources.AppSettings.EmailVerificationExpiration);

                await Common.Email.SendEmailVerificationEmail(email, verificationId);

                return NoContent();
            }
            catch (Exception ex)
            {
                LogException(ex);
                return ReturnException(ex);
            }
        }








        /// <summary>
        ///     Sends a new password reset request.
        /// </summary>
        /// 
        [HttpPost]
        [Route("password-reset/email")]
        public async Task<IActionResult> SendPasswordResetRequest( string email)
        {
            try
            {
                

                var requestId = await Users.CreateResetPasswordRequestAsync(email, Resources.AppSettings.EmailVerificationExpiration);

                await Common.Email.SendPasswordResetEmail(email, requestId);

                return NoContent();

            }
            catch (Exception ex)
            {
                LogException(ex, email);
                return ReturnException(ex);
            }
        }










        /// <summary>
        ///     Attempts to consume a password reset request to change a user password.
        /// </summary>
        /// 
        /// <param name="viewModel">Model with new user information</param>
        [HttpPost]
        [Route("reset-password")]
        public async Task<IActionResult> ResetUserPassword(PasswordResetModel model)
        {
            try
            {

                await Users.ResetUserPassword(model);

                return NoContent();
            }
            catch (Exception ex)
            {
                LogException(ex, model.PasswordResetId);
                return ReturnException(ex);
            }
        }










        /// <summary>
        ///     Allows the user to change their password
        /// </summary>
        /// 
        [HttpPost]
        [Route("change-password")]
        public async Task<IActionResult> ChangeUserPassword(ChangeUserPasswordModel model)
        {
            try
            {
                await model.ChangeUserPasswordAsync(CurrentJsonWebToken.UserId);

                return NoContent();
            }
            catch (Exception ex)
            {
                LogException(ex);
                return ReturnException(ex);
            }
        }









        /// <summary>
        ///     Gets all the unavailable dates attached to a user id
        /// </summary>
        /// <returns>a list of type datetime</returns>
        [HttpGet]
        [Route("get-unavailable-dates")]
        public async Task<IActionResult> GetMemberUnavailableDates()
        {
            try
            {
                return Ok(await Members.GetAllMemberDatesUnavaiableAsync(CurrentJsonWebToken.UserId));
            }
            catch (Exception ex)
            {
                LogException(ex);
                return ReturnException(ex);
            }
        }










        /// <summary>
        ///     Adds an unavaiable date with the user id attached
        /// </summary>
        /// 
        [HttpPost]
        [Route("add-unavailable-date")]
        public async Task<IActionResult> AddMemberUnavailableDateAsync(DateModel date) 
        {
            try
             {
                await Members.AddMemberUnavailableDate(CurrentJsonWebToken.UserId, date.Date);

                return NoContent();
            }
            catch (Exception ex)
            {
                LogException(ex);
                return ReturnException(ex);
            }

        }










        /// <summary>
        ///     Adds an unavaiable date with the user id attached
        /// </summary>
        /// 
        [HttpPost]
        [Route("remove-unavailable-date")]
        public async Task<IActionResult> RemoveMemberUnavailableDateAsync(DateModel date)
        {
            try
            {
                await Members.RemoveDate(CurrentJsonWebToken.UserId, date.Date);

                return NoContent();
            }
            catch (Exception ex)
            {
                LogException(ex);
                return ReturnException(ex);
            }

        }










        /// <summary>
        ///     Gets the profile for a given member to edit
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("get-profile")]
        public async Task<IActionResult> GetMemberProfileAsync()
        {
            try
            {
                var profile = await Members.GetMemberProfileAsync(CurrentJsonWebToken.UserId);

                return Ok(profile);
            }
            catch (Exception ex)
            {
                LogException(ex);
                return ReturnException(ex);
            }
        }









        /// <summary>
        ///     Updates the member profile with the incoming model
        /// </summary>
        /// <param name="model">teh mdoel to update the member record with</param>
        /// <returns></returns>
        [HttpPatch]
        [Route("update-profile")]
        public async Task<IActionResult> UpdateMemberProfileAsync(MemberProfileModel model)
        {
            try
            {
                await Members.UpdateMemberAsnc(CurrentJsonWebToken.UserId, model);

                return NoContent();
            }
            catch (Exception ex)
            {
                LogException(ex);
                return ReturnException(ex);
            }
        }










        /// <summary>
        ///     Sends all the emails of the visitors an email for invitation
        /// </summary>
        [HttpPost]
        [Route("send-emails")]
        public async Task<IActionResult> SendVisitorEmails(EmailBlastModel blast) 
        {
            if (!CurrentJsonWebToken.IsAdmin) 
            {
                var ex = new Exception("You do not have permission to do this!");
                return ReturnException(ex);
            }

            foreach (var email in blast.Emails) 
            {
                try
                {
                    await Common.Email.SendGridEmail(blast.Subject, email, blast.CustomMessage, false);

                }
                catch (Exception ex)
                {
                    LogException(ex);
                } 
            }
            return NoContent();
        }

    }
}
