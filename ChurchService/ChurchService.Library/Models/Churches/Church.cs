using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using ChurchService.Library.Models.Common;
using Microsoft.EntityFrameworkCore;

namespace ChurchService.Library.Models.Churches
{
    public static class Church
    {





        /// <summary>
        ///     Gets the email for a given church based off its database id
        /// </summary>
        /// <param name="churchId"></param>
        /// <returns></returns>
        public static async Task<string> GetChurchEmail(Guid churchId)
        {
            using (var db = new SQL.ChurchServiceContext())
            {
                return await (from c in db.Churches where c.ChurchId == churchId select c.Email).FirstAsync();
            }
        }










        /// <summary>
        ///     Sets a member account to removed and inactive
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public static async Task RemoveMemberAsync(Guid userId) 
        {
            using (var db = new SQL.ChurchServiceContext())
            {
                var dbMem = await db.Users.Where(u => u.UserId == userId).FirstOrDefaultAsync();

                if (dbMem == null)
                    throw new ArgumentException("This user was not found!");

                dbMem.Active = false;
                dbMem.Removed = true;

                await db.SaveChangesAsync();

            }
        }










        /// <summary>
        ///     Assigns a member to a planned visit
        /// </summary>
        /// <param name="userId">the database id of the user</param>
        /// <param name="plannedVisitId">the database id of the planned visit</param>
        /// <returns> a tuple of both the emails to send notifcations to</returns>
        public static async Task<(string userEmail, string visitorEmail, DateTime visitDate, string? profiledataUrl, string memberName, string churchName)> AssignPlannedVisit(Guid userId, Guid plannedVisitId) 
        {
            using (var db = new SQL.ChurchServiceContext()) 
            {
                var dbVisit = await db.PlannedVisits.Where(v => v.PlannedVisitId == plannedVisitId).FirstOrDefaultAsync();

                if (dbVisit == null)
                    throw new Exception("This visit was not found!");

                var dbUser = await db.Users.Where(u => u.UserId == userId).FirstOrDefaultAsync();

                if (dbUser == null)
                    throw new Exception("This user does not exist!");

                if (dbUser.AllowTextNotifications && dbUser.PhoneVerified)
                    await Utilities.TextNotifications.SendSmsMessageAsync(dbUser.Phone, $"You have been assigned to a new visit! visit date: {dbVisit.VisitDate.ToShortDateString()}");

                var churchName = await db.Churches.Where(c => c.ChurchId == dbVisit.ChurchId).Select(n => n.Name).FirstAsync();

                dbVisit.AssignedUserId = userId;

                await db.SaveChangesAsync();

                return (dbUser.Email, dbVisit.VisitorEmail, dbVisit.VisitDate, dbUser.ProfileImageDataUrl, dbUser.FirstName, churchName);
            }
        }










        /// <summary>
        ///     Removes the assigned user from the Visit
        /// </summary>
        /// 
        public static async Task UnassignVisitAsync(Guid visitId) 
        {
            using (var db = new SQL.ChurchServiceContext())
            {
                var visit = await db.PlannedVisits.Where(v => v.PlannedVisitId == visitId).FirstOrDefaultAsync();

                if (visit == null)
                    throw new Exception("This visit does not exist!");

                visit.AssignedUserId = null;

                await db.SaveChangesAsync();

            }
        }










        /// <summary>
        ///     Update the church record the incoming model
        /// </summary>
        /// <param name="churchId">the database id of the church to update</param>
        /// <param name="model">the model to update the record with</param>
        /// 
        public static async Task UpdateChurchAsync(Guid churchId, ChurchModel model) 
        {
            using (var db = new SQL.ChurchServiceContext()) 
            {
                var church = await db.Churches.Where(c => c.ChurchId == churchId).FirstOrDefaultAsync();

                if (church == null)
                    throw new Exception("This record does not exist!");

                church.PhoneNumber = model.PhoneNumber;
                church.Address = model.Address;
                church.City = model.City;
                church.Email = model.Email;
                church.State = model.State;
                church.Name = model.Name;
                church.Zip = model.Zip;

                //var regex = Regex.Match(model.ChurchLogoDataUrl, @"data:image/(?<type>.+?),(?<data>.+)");

                //var data = regex.Groups["data"].Value;
                //var mime = regex.Groups["type"].Value;

                church.ChurchLogoDataUrl = model.ChurchLogoDataUrl;
                church.ChurchLogoMime = model.ChurchLogoMime ;

                await db.SaveChangesAsync();
            }
        }










        /// <summary>
        ///     Gets the church profile for a given church based off the church id
        /// </summary>
        /// <param name="churchId">the database id of the church</param>
        /// <returns></returns>
        public static async Task<ChurchModel> GetChurchProfileAsync(Guid churchId)
        {
            using (var db = new SQL.ChurchServiceContext()) 
            {
                return await (from c in db.Churches 
                              where c.ChurchId == churchId 
                              select new ChurchModel() 
                              {
                                  Name = c.Name,
                                  Email = c.Email,
                                  PhoneNumber = c.PhoneNumber,
                                  Address = c.Address,
                                  City = c.City,
                                  State = c.State,
                                  Zip = c.Zip,
                                  ChurchLogoDataUrl = c.ChurchLogoDataUrl,
                                  ChurchLogoMime = c.ChurchLogoMime
                              }).FirstAsync();
            }
        }









        /// <summary>
        ///     Gets a list of non admin users connected to a given church id
        /// </summary>
        /// <param name="churchId">The database id of the church</param>
        /// <returns>List of member details</returns>
        public static async Task<List<MemberDetailsModel>> GetChurchMembersAsync(Guid churchId)
        {
            using (var db = new SQL.ChurchServiceContext())
            {
                return await (from m in db.Users 
                              where m.ChurchId == churchId && m.IsAdmin != true && !m.Removed && m.Active
                              select new MemberDetailsModel() 
                              {
                                  FullName = $"{m.FirstName} {m.LastName}",
                                  Email = m.Email,
                                  Phone = m.Phone,
                                  UserId = m.UserId,
                              }).ToListAsync();
            }
        }










        /// <summary>
        ///     Upgrades a member to admin status
        /// </summary>
        /// <param name="memberId"></param>
        /// <returns></returns>
        public static async Task UpgradeMemberToAdmin(Guid memberId) 
        {
            using (var db = new SQL.ChurchServiceContext()) 
            {
                var member = await db.Users.FindAsync(memberId);

                if (member == null)
                    throw new ArgumentException("This member was not found!");

                member.IsAdmin = true;
                 
                await db.SaveChangesAsync();

            }
        }










        /// <summary>
        ///     Gets all the avaiable members on a given date
        /// </summary>
        /// <param name="churchId"></param>
        /// <param name="date"></param>
        /// <returns></returns>
        public static async Task<List<OptionModel>> GetAllAvailableMembers(Guid churchId, DateTime date) 
        {
            using (var db = new SQL.ChurchServiceContext())
            {

                var procedures = new SQL.ChurchServiceContextProcedures(db);

                var dbMembers = await procedures.GetAvailableMembersAsync(date, churchId);

                var members = new List<OptionModel>();
                foreach (var member in dbMembers)
                {
                    var option = new OptionModel()
                    {
                        DatabaseId = member.UserId,
                        Description = member.FullName,
                    };
                    members.Add(option);
                }

                return members;
            }
        }










        /// <summary>
        ///     Returns the data url of the church logo
        /// </summary>
        /// <param name="churchId"></param>
        /// <returns></returns>
        public static async Task<string?> GetChurchLogoAsync(Guid churchId) 
        {
            using (var db = new SQL.ChurchServiceContext()) 
            {
                return await db.Churches.Where(c => c.ChurchId == churchId).Select(c => c.ChurchLogoDataUrl).FirstOrDefaultAsync();
            }
        }

            
    }
}
