using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using Twilio.Rest.Api.V2010.Account;
using Twilio;
using Twilio.Types;
using Microsoft.Extensions.Configuration;

namespace ChurchService.Library.Utilities
{
    public static class TextNotifications
    {


        /// <summary>
        ///     Sends an sms message with the verification code to the phone number
        /// </summary>
        /// <param name="phoneNumber">the phone number attached to the traveler account</param>
        /// <returns></returns>
        public static async Task SendSmsVerificationAsync(string phoneNumber, int verifCode)
        {

            IConfigurationRoot configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json")
            .Build();
            var auth = configuration.GetValue<string>("TwilioAuthToken");

            var SID = configuration.GetValue<string>("TwilioSID");

            var twilioPhone = configuration.GetValue<string>("TwilioPhoneNumber");

            TwilioClient.Init(SID, auth);

            if(!phoneNumber.StartsWith("+1"))
                phoneNumber = "+1" + phoneNumber;


            var messageOptions = new CreateMessageOptions(new PhoneNumber(phoneNumber));
            messageOptions.From = new PhoneNumber(twilioPhone); 
            messageOptions.Body = $"Your code to verify your phone is {verifCode}";


            await MessageResource.CreateAsync(messageOptions);
            
        }









        /// <summary>
        ///     Sends an sms message to the Phone number provided
        /// </summary>
        /// <param name="phoneNumber">The phone number in which to send the message</param>
        /// <param name="message">The message to send</param>
        ///
        public static async Task SendSmsMessageAsync(string phoneNumber, string message)
        {

            IConfigurationRoot configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json").Build();

            var auth = configuration.GetValue<string>("TwilioAuthToken");

            var SID = configuration.GetValue<string>("TwilioSID");

            var twilioPhone = configuration.GetValue<string>("TwilioPhoneNumber");


            TwilioClient.Init(SID, auth);

            var messageOptions = new CreateMessageOptions(new PhoneNumber(phoneNumber));
            messageOptions.From = new PhoneNumber(twilioPhone);
            messageOptions.Body = message
;

            await MessageResource.CreateAsync(messageOptions);
        }
    }
}
