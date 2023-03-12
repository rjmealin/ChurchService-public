using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChurchService.Library.Models.Churches
{
    public class EmailBlastModel
    {
        public string CustomMessage { get; set; }
        public DateTime? EventDate { get; set; }
        public List<string> Emails { get; set; }
        public string Subject { get; set; }
    }
}
