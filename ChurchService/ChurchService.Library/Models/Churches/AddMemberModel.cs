using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ChurchService.Library.Utilities;

namespace ChurchService.Library.Models.Churches
{
    public class AddMemberModel
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Zip { get; set; }
        public string Password { get; set; }





        /// <summary>
        ///     Creates a new member for a given church
        /// </summary>
        /// <param name="churchId">the database id of the church</param>
        ///
        public async Task CreateChurchMemberAsync(Guid churchId)
        {
            using (var db = new SQL.ChurchServiceContext())
            {

                var check = await db.Users.Where(u => u.Email == this.Email).FirstOrDefaultAsync();

                if (check != null)
                    throw new ApplicationException("This email has been used already!");

                var dbUser = new SQL.User()
                {
                    ChurchId = churchId,
                    Email = this.Email,
                    Phone = StringUtilities.CleanPhoneNumber(this.PhoneNumber),
                    FirstName = this.FirstName,
                    LastName = this.LastName,
                    DateCreated = DateTime.UtcNow,
                    Active = true,
                    EmailVerified = true,
                    IsAdmin = false,
                };

                Users.UserHash.SetUserPassword(dbUser, this.Password);

                db.Users.Add(dbUser);

                await db.SaveChangesAsync();
            }
        }


    }
}
