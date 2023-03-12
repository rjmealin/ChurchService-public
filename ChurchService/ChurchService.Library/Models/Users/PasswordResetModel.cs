using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChurchService.Library.Models.Users
{
    public class PasswordResetModel
    {
        public Guid PasswordResetId { get; set; }
        public string Password { get; set; }
        public bool ShouldSerializePassword()
        {
            return false;
        }
    }
}
