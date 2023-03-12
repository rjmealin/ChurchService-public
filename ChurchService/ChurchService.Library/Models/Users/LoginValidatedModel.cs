using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChurchService.Library.Models.Users
{
    public class LoginValidatedModel
    {
        public Guid UserId { get; set; }
        public Guid ChurchId { get; set; }
        public bool IsAdmin { get; set; } = false;
        public string FullName { get; set; }
        public bool Validated { get; set; } = false;
        public bool EmailVerified { get; set; }

    }
}
