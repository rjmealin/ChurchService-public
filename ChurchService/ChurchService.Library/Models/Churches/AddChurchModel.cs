using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChurchService.Library.Models.Churches
{
    public class AddChurchModel
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string PhoneNumber {get; set;}
        public string Address {get; set;}
        public string City {get; set;}
        public string State {get; set;}
        public string Zip {get; set; }
        public string Password { get; set; }









        /// <summary>
        ///     Creates a church record and an admin user account for the church
        /// </summary>
        /// <returns></returns>
        public async Task CreateChurch() 
        {
            using (var db = new SQL.ChurchServiceContext())
            {
                var dbChurch = new SQL.Church()
                {
                    Name = this.Name,
                    Email = this.Email,
                    PhoneNumber = this.PhoneNumber,
                    Address = this.Address,
                    City = this.City,
                    State = this.State,
                    Zip = this.Zip,
                    Verified = true,
                };

                db.Churches.Add(dbChurch);

                await db.SaveChangesAsync();

                var dbUser = new SQL.User()
                {
                    ChurchId = dbChurch.ChurchId,
                    Email = this.Email,
                    Phone = this.PhoneNumber,
                    FirstName = this.Name,
                    LastName = this.Name,
                    DateCreated = DateTime.UtcNow,
                    Active = true,
                    EmailVerified = true,
                    IsAdmin = true,
                };

                Users.UserHash.SetUserPassword(dbUser, this.Password);

                db.Users.Add(dbUser);

                await db.SaveChangesAsync();

            }
        }

    }
}
