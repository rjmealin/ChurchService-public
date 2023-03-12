using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChurchService.Library.Models.Churches
{
    public class ChurchModel
    {
        public Guid ChurchId { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Zip { get; set; }
        public string ChurchLogoDataUrl { get; set; }
        public string ChurchLogoMime { get; set; }
    }
}
