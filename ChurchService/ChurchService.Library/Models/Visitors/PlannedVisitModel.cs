using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChurchService.Library.Models.Visitors
{
    public class PlannedVisitModel
    {
        public Guid PlannedVisitId { get; set; }
        public Guid? AssignedUserId { get; set; }
        public string? AssignedMemberFirstName { get; set; }
        public string? AssignedMemberLastName { get; set; }
        public DateTime VisitDate { get; set; }
        public string VisitorFirstName { get; set; }
        public string VisitorLastName { get; set; }
        public string VisitorPhone { get; set; }
        public string VisitorEmail { get; set; }
        public string CommentsOrQuestions { get; set; }
    }
}
