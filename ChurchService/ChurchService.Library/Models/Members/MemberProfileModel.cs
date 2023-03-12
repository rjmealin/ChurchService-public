using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChurchService.Library.Models.Members
{
    public class MemberProfileModel
    {
        public string Email { get; set; }
        public string Phone { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public bool EnableTextNotifications { get; set; }
        public bool PhoneVerified { get; set; }
        public string? ProfileImageDataUrl { get; set; }
        public string? ProfileImageMime { get; set; }
    }
}
