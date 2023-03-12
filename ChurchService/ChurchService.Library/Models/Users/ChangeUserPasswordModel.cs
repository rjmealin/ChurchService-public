using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ChurchService.Library.SQL;

namespace ChurchService.Library.Models.Users
{
    public class ChangeUserPasswordModel
    {
        public string NewPassword { get; set; }
        public string OldPassword { get; set; }











        /// <summary>
        ///     Changes the User password, confirms the old password imcoming matches
        /// </summary>
        /// <param name="userId">the database id of the current user</param>
        /// <returns></returns>
        /// 
        public async Task ChangeUserPasswordAsync(Guid userId)
        {
            using (var db = new ChurchServiceContext())
            {
                var user = await db.Users.FindAsync(userId);

                if (user == null)
                    throw new ArgumentException("User Not Found!");

                bool verified = UserHash.VerifyPassword(user.Salt.ToString(), user.Password, OldPassword);

                if (!verified)
                    throw new ArgumentException("The password provided does not match the exisiting password");

                UserHash.SetUserPassword(user, NewPassword);

                await db.SaveChangesAsync();

            }
        }
    }
}
