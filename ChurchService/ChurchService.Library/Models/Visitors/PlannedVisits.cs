using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ChurchService.Library.SQL;
using Microsoft.EntityFrameworkCore;

namespace ChurchService.Library.Models.Visitors
{
    public static class PlannedVisits
    {





        /// <summary>
        ///     Saves the incoming planned visit to the database
        /// </summary>
        public static async Task AddPlannedVisit(AddPlannedVisitModel model)
        {
            using (var db = new ChurchServiceContext())
            {
                var plannedVisit = new PlannedVisit()
                {
                    ChurchId = model.ChurchId,
                    VisitorFirstName = model.VisitorFirstName,
                    VisitorLastName = model.VisitorLastName,
                    VisitorEmail = model.VisitorEmail,
                    VisitorPhone = model.VisitorPhone,
                    VisitDate = model.VisitDate,
                    CommentsOrQuestions = model.CommentsOrQuestions, 
                };

                db.PlannedVisits.Add(plannedVisit);

                await db.SaveChangesAsync();

            }
        }











        /// <summary>
        ///     Gets all the planned visits that are yet to be assigned
        /// </summary>
        /// <param name="churchId"></param>
        /// <returns></returns>
        public static async Task<int> GetUnassignedPlannedVisitCount(Guid churchId)
        {
            using (var db = new ChurchServiceContext())
            {
                return await (from pv in db.PlannedVisits
                              where pv.ChurchId == churchId && pv.AssignedUserId == null && pv.VisitDate > DateTime.Now
                              select pv).CountAsync();
            }


        }










        /// <summary>
        ///     Gets all the upcoming planned visits
        /// </summary>
        /// <param name="churchId"></param>
        /// <returns></returns>
        public static async Task<List<PlannedVisitModel>> GetUpcomingPlannedVisitsAsync(Guid churchId)
        {
            using (var db = new ChurchServiceContext())
            {
                return await (from pv in db.PlannedVisits
                              join u in db.Users on pv.AssignedUserId equals u.UserId into assigned_u
                              from au in assigned_u.DefaultIfEmpty()
                              where churchId == pv.ChurchId && pv.VisitDate > DateTime.Now
                              orderby pv.VisitDate ascending
                              select new PlannedVisitModel() 
                              {
                                PlannedVisitId = pv.PlannedVisitId,
                                AssignedUserId = pv.AssignedUserId,
                                VisitDate = pv.VisitDate,
                                AssignedMemberFirstName = au.FirstName,
                                AssignedMemberLastName = au.LastName,
                                VisitorEmail = pv.VisitorEmail,
                                VisitorFirstName = pv.VisitorFirstName,
                                VisitorLastName = pv.VisitorLastName, 
                                VisitorPhone = pv.VisitorPhone,
                              }).ToListAsync();
            }
        }











        /// <summary>
        ///     Gets the details for a given planned visit
        /// </summary>
        /// <param name="visitId">the database id of the visit to get</param>
        /// <returns>returns a planned visit model</returns>
        public static async Task<PlannedVisitModel> GetPlannedVisitAsync(Guid visitId)
        {
            using (var db = new ChurchServiceContext())
            {
                return await (from v in db.PlannedVisits 
                              join u in db.Users on v.AssignedUserId equals u.UserId into assigned_u
                              from au in assigned_u.DefaultIfEmpty()
                              where v.PlannedVisitId == visitId 
                              select new PlannedVisitModel() 
                              {
                                  PlannedVisitId = v.PlannedVisitId,
                                  AssignedUserId = v.AssignedUserId,
                                  VisitDate = v.VisitDate,
                                  AssignedMemberFirstName = au.FirstName,
                                  AssignedMemberLastName = au.LastName,
                                  VisitorEmail = v.VisitorEmail,
                                  VisitorFirstName = v.VisitorFirstName,
                                  VisitorLastName = v.VisitorLastName,
                                  VisitorPhone = v.VisitorPhone,
                                  CommentsOrQuestions = v.CommentsOrQuestions
                              }).FirstAsync();
            }
        }










        /// <summary>
        ///     Gets all the past visits for a given church
        /// </summary>
        /// <param name="churchId"></param>
        /// <returns></returns>
        public static async Task<List<SQL.PlannedVisit>> GetAllPastVisits(Guid churchId) 
        {
            using (var db = new ChurchServiceContext())
            {
                return await (from pv in db.PlannedVisits
                              where churchId == pv.ChurchId && pv.VisitDate < DateTime.Now
                              select pv).ToListAsync();
            }
        }
    }
}
