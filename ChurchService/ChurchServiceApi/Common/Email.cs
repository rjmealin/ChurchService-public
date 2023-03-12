using SendGrid;
using SendGrid.Helpers.Mail;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ChurchServiceApi.Common
{
    public static class Email
    {


        internal static string AssignedVisitEmail =
            "<div style='width:100%; display: flex;flex-direction: row; justify-content: center;'>" +
            "<div style='width:90%;display:flex;flex-direction: column;justify-content: center;align-items: center; box-sizing: border-box; padding: 15px; border: solid #ddd 1px; box-shadow: 1px 1px 10px #ddd;border-radius: 10px;'>" +
            "<h2 style='font-family: sans-serif;'>Your planned visit at {{churchName}} has been assigned!</h2>" +
            "<p style='font-size:18px; font-family: sans-serif;'>{{name}} has been assigned to your visit on {{date}}</p>" +
            "<p style='font-size:18px; font-family: sans-serif;'>Their email is {{email}}, feel free to contact them at any time regarding any questions</p>" +
            "<p style='font-size:18px; font-family: sans-serif;'>We look foward to seeing you!</p>" +
            "</div>" +
            "</div>";

        /// <summary>
        ///     Sends an email verification email to the recipient.
        /// </summary>
        /// 
        /// <param name="recipientEmail">The email address of the recipient.</param>
        /// <param name="verificationId">The verification id.</param>
        /// 
        public static async Task SendPasswordResetEmail(string recipientEmail, Guid requestId)
        {
            IConfigurationRoot configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json").Build();

            var siteUrl = configuration.GetValue<string>("SiteUrl");

            var body = $"<h1>Password reset</h1><br><p>Please follow this link to reset your password</p><br><a href='{siteUrl}/reset-password/{requestId}'>Click here to reset password</a><br><p>If the link is broken copy and paste this into your browser:</p><br><p>{siteUrl}/reset-password/{requestId}</p>";

            await SendGridEmail("Password Reset Request", recipientEmail, body, true);

        }










        /// <summary>
        ///     Sends an email verification email to the recipient.
        /// </summary>
        /// 
        /// <param name="recipientEmail">The email address of the recipient.</param>
        /// <param name="verificationId">The verification id.</param>
        /// 
        public static async Task SendEmailVerificationEmail(string recipientEmail, Guid verificationId)
        {

            IConfigurationRoot configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json").Build();

            var siteUrl = configuration.GetValue<string>("SiteUrl");

            var body = $"<a href='${siteUrl}/account/email-verify/{verificationId}'>Click here to verify your email</a>";

            await SendGridEmail("Email Verification", recipientEmail, body, true);
        }










        /// <summary>
        ///     Sends a notification email 
        /// </summary>
        /// <param name="recipientEmail"></param>
        /// <returns></returns>
        public static async Task SendVisitorAssignmentEmailAsync(string visitorEmail, string memberName, DateTime visitDate, string memberEmail, string churchName)
        {
            var body = AssignedVisitEmail;

            body = body.Replace("{{name}}", memberName);
            body = body.Replace("{{date}}", visitDate.ToLongDateString());
            body = body.Replace("{{email}}", memberEmail);
            body = body.Replace("{{churchName}}", churchName);

            if (!IsValidEmail(visitorEmail))
                throw new ArgumentException($"Invalid recipient address: {visitorEmail}");

            await SendGridEmail("Planned visit assignment",visitorEmail, body, true);
        }










        /// <summary>
        ///     Returns false if any required condition isn't met.
        /// </summary>
        /// 
        /// <param name="Email">The email message to validate.</param>
        /// 
        /// <remarks>
        ///     Requirements: Sender and Recipients emails must be real.
        /// </remarks>
        /// 
        private static void ValidateMailMessage(System.Net.Mail.MailMessage Email)
        {
            if (!IsValidEmail(Email.From.Address))
                throw new ArgumentException($"Invalid sender address: {Email.From.Address}");

            foreach (var e in Email.To)
                if (!IsValidEmail(e.Address))
                    throw new ArgumentException($"Invalid recipient address: {e.Address}");
        }










        /// <summary>
        ///     Returns true if the given email is valid.
        /// </summary>
        /// 
        /// <param name="email">The email address to test.</param>
        /// 
        public static bool IsValidEmail(string email)
        {
            if (email == null)
                email = "";

            return Regex.IsMatch(email, "\\A(?:[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?)\\Z", RegexOptions.IgnoreCase);
        }










        /// <summary>
        ///     Throws an exception if the attachment is not found.
        /// </summary>
        private static void ValidateAttachment(string Attachment)
        {
            if (Attachment != null && !System.IO.File.Exists(Attachment))
                throw new ArgumentException($"The attachment file {Attachment} could not be found.");
        }





        



        /// <summary>
        ///     Uses the sendgrid library to send an email
        /// </summary>
        /// <param name="subject">the email subject</param>
        /// <param name="toEmail">the receipent email</param>
        /// <param name="body">the email body</param>
        public static async Task SendGridEmail(string subject, string toEmail, string body, bool isHtmlEmail = false) 
        {

            IConfigurationRoot configuration = new ConfigurationBuilder()
                        .SetBasePath(Directory.GetCurrentDirectory())
                        .AddJsonFile("appsettings.json").Build();

            var apiKey = configuration.GetValue<string>("SendGridApiKey");

            var apiEmail = configuration.GetValue<string>("SendGridEmail");


            var client = new SendGridClient(apiKey);
            var from = new EmailAddress(apiEmail, "do not reply");
            var to = new EmailAddress(toEmail, "Example User");

            var htmlContent = "";
            var plainTextContent = "";

            if (isHtmlEmail)
            {
                htmlContent = body;
            }
            else
            {
                plainTextContent = body;
            }


            var msg = MailHelper.CreateSingleEmail(from, to, subject, plainTextContent, htmlContent);

            await client.SendEmailAsync(msg);
        }
    }
}
