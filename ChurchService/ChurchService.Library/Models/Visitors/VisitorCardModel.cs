using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChurchService.Library.Models.Visitors
{
    public enum ReferralTypes
    {
        Email = 1,
        Flyer = 2,
        Website = 3,
        InviteFromMember = 4,
        NewsPaper = 5,
        SocialMedia = 6
    }

    public class VisitorCardModel
    {
        public Guid VisitorCardId { get; set; }
        public Guid ChurchId { get; set; }
        public string? VisitorFirstName { get; set; }
        public string? VisitorLastName { get; set; }
        public string? VisitorEmail { get; set; }
        public string? VisitorPhone { get; set; }
        public string? VisitorAddress { get; set; }
        public string? VisitorCity { get; set; }
        public string? VisitorState { get; set; }
        public string? VisitorZip { get; set; }
        public ReferralTypes? ReferalType { get; set; }
        public string? ReasonForAttendance { get; set; }
        public string? Comments { get; set; }
        public DateTime? DateOfAttendance { get; set; }
        public int? VisitorAge { get; set; }
        public bool? IsNewToArea { get; set; }
        public bool? IsExistingChristian { get; set; }
        public bool? IsReturningVisitor { get; set; }
        public string? HomeChurch { get; set; }
        public bool? IsFirstTimeGuest { get; set; }

    }
}
