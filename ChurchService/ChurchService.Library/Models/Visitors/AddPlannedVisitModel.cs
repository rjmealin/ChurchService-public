using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChurchService.Library.Models.Visitors
{
    public class AddPlannedVisitModel
    {
        public Guid ChurchId { get; set; }
        public DateTime VisitDate { get; set; }
        public string VisitorFirstName { get; set; }
        public string VisitorLastName { get; set; }
        public string VisitorPhone { get; set; }
        public string VisitorEmail { get; set; }
        public string CommentsOrQuestions { get; set; }
    }
}
