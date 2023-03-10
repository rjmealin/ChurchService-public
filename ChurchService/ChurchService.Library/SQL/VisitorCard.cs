// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;

namespace ChurchService.Library.SQL
{
    public partial class VisitorCard
    {
        public Guid VisitorCardId { get; set; }
        public Guid ChurchId { get; set; }
        public string VisitorFirstName { get; set; }
        public string VisitorLastName { get; set; }
        public string VisitorEmail { get; set; }
        public string VisitorPhone { get; set; }
        public string VisitorAddress { get; set; }
        public string VisitorCity { get; set; }
        public string VisitorState { get; set; }
        public string VisitorZip { get; set; }
        public int? VisitorAge { get; set; }
        public DateTime? DateOfAttendance { get; set; }
        public bool? IsNewToArea { get; set; }
        public bool? IsExistingChristian { get; set; }
        public bool? IsReturningVisitor { get; set; }
        public int? ReferralType { get; set; }
        public string ReasonForAttendance { get; set; }
        public string Comments { get; set; }
        public string HomeChurch { get; set; }
        public bool? IsFirstTimeGuest { get; set; }

        public virtual Church Church { get; set; }
    }
}