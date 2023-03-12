using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChurchService.Library.Models.Users
{
    public class UserModel : User
    {
        public Guid UserId { get; set; }
        public bool IsRemoved { get; set; }
        public bool IsActive { get; set; }

    }
}
