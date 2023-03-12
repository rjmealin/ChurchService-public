using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChurchService.Library.Models.Members
{
    public class AddMemberModel
    {
        public string Email { get; set; }
        public string Phone { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Password { get; set; }
        public bool IsAdmin { get; set; }



        /// <summary>
        ///     Uses the model to register the member with the database id of the church
        /// </summary>
        /// 
        public async Task RegisterMember(Guid churchId) 
        {
            using (var db = new SQL.ChurchServiceContext()) 
            {
                var check = await db.Users.Where(u => u.Email == this.Email).FirstOrDefaultAsync();

                if (check != null)
                    throw new Exception("This user already exists!");    

                var member = new SQL.User();

                member.ChurchId = churchId;
                member.FirstName = this.FirstName;
                member.LastName = this.LastName;
                member.Email = this.Email;
                member.Phone = this.Phone;
                member.Active = true;
                member.IsAdmin = this.IsAdmin;
                member.DateCreated = DateTime.Now;

                //Lets have email verified to be true if a church admin registers them
                member.EmailVerified = true;

                Users.UserHash.SetUserPassword(member, this.Password);


                await db.SaveChangesAsync();

            }
        }
    }
}
