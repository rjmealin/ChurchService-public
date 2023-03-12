using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ChurchService.Library.Utilities
{
    public static class StringUtilities
    {
        /// <summary>
        ///     Removes all Non digit charaters from the incoming phone number
        /// </summary>
        public static string CleanPhoneNumber(string phoneNumber)
        {
            Regex rgx = new Regex(@"\D");
            return rgx.Replace(phoneNumber, "");
        }

    }
}
