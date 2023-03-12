﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;

namespace ChurchService.Library.SQL
{
    public partial class User
    {
        public User()
        {
            PlannedVisits = new HashSet<PlannedVisit>();
            UnavailableDates = new HashSet<UnavailableDate>();
            UserEmailVerifications = new HashSet<UserEmailVerification>();
            UserPasswordResets = new HashSet<UserPasswordReset>();
            UserSmsVerifications = new HashSet<UserSmsVerification>();
        }

        public Guid UserId { get; set; }
        public Guid ChurchId { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Password { get; set; }
        public Guid Salt { get; set; }
        public DateTime DateCreated { get; set; }
        public bool Removed { get; set; }
        public bool Active { get; set; }
        public bool EmailVerified { get; set; }
        public bool AllowTextNotifications { get; set; }
        public bool PhoneVerified { get; set; }
        public bool IsAdmin { get; set; }
        public string ProfileImageDataUrl { get; set; }
        public string ProfileImageMime { get; set; }
        public DateTime? RemovedDate { get; set; }
        public string ReasonRemoved { get; set; }

        public virtual Church Church { get; set; }
        public virtual ICollection<PlannedVisit> PlannedVisits { get; set; }
        public virtual ICollection<UnavailableDate> UnavailableDates { get; set; }
        public virtual ICollection<UserEmailVerification> UserEmailVerifications { get; set; }
        public virtual ICollection<UserPasswordReset> UserPasswordResets { get; set; }
        public virtual ICollection<UserSmsVerification> UserSmsVerifications { get; set; }
    }
}