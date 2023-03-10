// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;

namespace ChurchService.Library.SQL
{
    public partial class Church
    {
        public Church()
        {
            PlannedVisits = new HashSet<PlannedVisit>();
            Users = new HashSet<User>();
            VisitorCards = new HashSet<VisitorCard>();
        }

        public Guid ChurchId { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Zip { get; set; }
        public bool? Active { get; set; }
        public bool Verified { get; set; }
        public bool Removed { get; set; }
        public string ChurchLogoDataUrl { get; set; }
        public string ChurchLogoMime { get; set; }

        public virtual ICollection<PlannedVisit> PlannedVisits { get; set; }
        public virtual ICollection<User> Users { get; set; }
        public virtual ICollection<VisitorCard> VisitorCards { get; set; }
    }
}