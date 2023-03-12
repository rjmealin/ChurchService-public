using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ChurchService.Library.SQL;
using Microsoft.EntityFrameworkCore;
using ChurchService.Library.Utilities;

namespace ChurchService.Library.Models.Members
{
    public static class Members
    {

        /// <summary>
        ///     Adds a date unavailable to the table
        /// </summary>
        /// 
        public static async Task AddMemberUnavailableDate(Guid memberId, DateTime dateUnavailable)
        {
            using (var db = new ChurchServiceContext()) 
            {

                var dbDate = new SQL.UnavailableDate()
                {
                    UserId = memberId,
                    DateUnavailable = dateUnavailable
                };


                db.UnavailableDates.Add(dbDate);

                await db.SaveChangesAsync();
            }
        }









        /// <summary>
        ///     Gets all the unavaible dates attached to given member
        /// </summary>
        /// <param name="memberId"></param>
        /// <returns></returns>
        public static async Task<List<DateTime>> GetAllMemberDatesUnavaiableAsync(Guid memberId)
        {
            using (var db = new ChurchServiceContext()) 
            {
                return await (from u in db.UnavailableDates 
                              where u.UserId == memberId && u.DateUnavailable > DateTime.Now 
                              select u.DateUnavailable).ToListAsync();
            }
        }










        /// <summary>
        ///     Deletes a date for a given member
        /// </summary>
        /// <param name="memberId"></param>
        /// <param name="date"></param>
        /// <returns></returns>
        public static async Task RemoveDate(Guid memberId, DateTime date) 
        {
            using (var db = new ChurchServiceContext())
            {
                var dates = await db.UnavailableDates.Where(d => d.DateUnavailable.Date == date.Date && d.UserId == memberId).ToListAsync();

                db.RemoveRange(dates);

                await db.SaveChangesAsync();

            }
        }











        /// <summary>
        ///     Gets the member profile for a given member
        /// </summary>
        /// <returns></returns>
        public static async Task<MemberProfileModel> GetMemberProfileAsync(Guid memberId)
        {
            using (var db = new ChurchServiceContext())
            {
                return await (from u in db.Users 
                              where u.UserId == memberId 
                              select new MemberProfileModel() 
                              {
                                  Email = u.Email,
                                  Phone = u.Phone,
                                  FirstName = u.FirstName,
                                  LastName = u.LastName,
                                  ProfileImageDataUrl = u.ProfileImageDataUrl,
                                  ProfileImageMime = u.ProfileImageMime,
                                  EnableTextNotifications = u.AllowTextNotifications,
                                  PhoneVerified = u.PhoneVerified
                              }).FirstAsync();
            }
        }










        /// <summary>
        ///     Updates the member record with the incoming model
        /// </summary>
        /// <param name="memberId">teh db id of the member</param>
        /// <param name="model">the incoming model</param>
        /// 
        public static async Task UpdateMemberAsnc(Guid memberId, MemberProfileModel model) 
        {
            using (var db = new ChurchServiceContext()) 
            {
                var dbProfile = await db.Users.Where(u => u.UserId == memberId).FirstOrDefaultAsync();

                dbProfile.FirstName = model.FirstName;
                dbProfile.LastName = model.LastName;
                //dbProfile.Email = model.Email;
                dbProfile.ProfileImageDataUrl = model.ProfileImageDataUrl;
                dbProfile.ProfileImageMime = model.ProfileImageMime;
                dbProfile.AllowTextNotifications = model.EnableTextNotifications;
                dbProfile.ProfileImageDataUrl = model.ProfileImageDataUrl;
                dbProfile.ProfileImageMime = model.ProfileImageMime;

                //IF the model and db phone dont match we need to re verify otherwise
                //just modify
                if (model.Phone != dbProfile.Phone)
                {
                    dbProfile.PhoneVerified = false;
                    dbProfile.Phone = StringUtilities.CleanPhoneNumber(model.Phone);
                }

                await db.SaveChangesAsync();

            }
        }

    }
}
