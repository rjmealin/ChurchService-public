using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChurchService.Library.Models.Users
{
    public class UserImageModel
    {
        public Guid? UserProfileImageId { get; set; }
        public string ImageName { get; set; }
        public string DataUrl { get; set; }
        public string FileType { get; set; }
        public string AltText { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime? DateModified { get; set; }
    }
}
