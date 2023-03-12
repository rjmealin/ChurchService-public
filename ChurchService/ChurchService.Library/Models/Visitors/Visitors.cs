using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ChurchService.Library.SQL;
using Microsoft.EntityFrameworkCore;

namespace ChurchService.Library.Models.Visitors
{
    public static class Visitors
    {
        /// <summary>
        ///     Saves a visitor card to the database
        /// </summary>
        /// <param name="model">the model sent in from the controller</param>
        /// 
        public static async Task AddVisitorCardAsync(VisitorCardModel model, Guid churchId) 
        {
            using (var db = new ChurchServiceContext())
            {
                var dbCard = new SQL.VisitorCard()
                {
                    ChurchId = churchId,
                    VisitorFirstName = model.VisitorFirstName,
                    VisitorLastName = model.VisitorLastName,    
                    VisitorEmail = model.VisitorEmail,
                    VisitorPhone = model.VisitorPhone,
                    VisitorAddress = model.VisitorAddress,
                    VisitorCity = model.VisitorCity,
                    VisitorState = model.VisitorState,
                    VisitorZip = model.VisitorZip,
                    Comments = model.Comments,
                    ReasonForAttendance = model.ReasonForAttendance,
                    ReferralType = model.ReferalType.HasValue ? (int)model.ReferalType.Value : null,
                    DateOfAttendance = model.DateOfAttendance,
                    IsNewToArea = model.IsNewToArea,
                    IsExistingChristian = model.IsExistingChristian,
                    IsReturningVisitor = model.IsReturningVisitor,
                    HomeChurch = model.HomeChurch,
                    IsFirstTimeGuest = model.IsFirstTimeGuest
                };

                await db.VisitorCards.AddAsync(dbCard);

                await db.SaveChangesAsync();
            }
        }











        /// <summary>
        ///     Gets a list of all visitor cards attached to a given church id
        /// </summary>
        /// <param name="churchId">the database id of the church</param>
        /// <returns>List of visitor cards</returns>
        public static async Task<List<VisitorCardModel>> GetAllVisitorCardsForChurch(Guid churchId) 
        {
            using (var db = new ChurchServiceContext())
            {
                var cards = await (from v in db.VisitorCards
                                   where v.ChurchId == churchId
                                   select new VisitorCardModel() 
                                   {
                                       VisitorCardId = v.VisitorCardId,
                                       ChurchId = churchId,
                                       VisitorFirstName = v.VisitorFirstName,
                                       VisitorLastName = v.VisitorLastName,
                                       VisitorEmail = v.VisitorEmail,
                                       VisitorPhone = v.VisitorPhone,
                                       VisitorAddress = v.VisitorAddress,
                                       VisitorCity = v.VisitorCity,
                                       VisitorState = v.VisitorState,
                                       VisitorZip = v.VisitorZip,
                                       ReferalType = (ReferralTypes?)v.ReferralType,
                                       ReasonForAttendance = v.ReasonForAttendance,
                                       Comments = v.Comments,
                                       DateOfAttendance = v.DateOfAttendance,
                                       IsExistingChristian = v.IsExistingChristian,
                                       IsFirstTimeGuest = v.IsFirstTimeGuest,
                                       IsNewToArea = v.IsNewToArea,
                                       IsReturningVisitor = v.IsReturningVisitor
                                   }).ToListAsync();
                return cards;
            }
        }










        /// <summary>
        ///     deletes a visitor card from the db
        /// </summary>
        /// <param name="cardId"></param>
        /// 
        public static async Task RemoveVisitorCardAsync(Guid cardId)
        {
            using (var db = new SQL.ChurchServiceContext())
            {
                var card = await db.VisitorCards.Where(c => c.VisitorCardId == cardId).FirstOrDefaultAsync();

                if (card == null)
                    throw new Exception("This card does not exist!");

                db.VisitorCards.Remove(card);

                await db.SaveChangesAsync();
            }
        }











        /// <summary>
        ///     Gets a visitor card and all its details
        /// </summary>
        /// <param name="cardId">the database id of the card to view</param>
        /// <returns>returns all the details surrounding a visitor card</returns>
        public static async Task<VisitorCardModel> GetVisitorCardDetails(Guid cardId)
        {
            using (var db = new ChurchServiceContext()) 
            {
                var card = await (from v in db.VisitorCards
                                   where v.VisitorCardId == cardId
                                   select new VisitorCardModel()
                                   {
                                       VisitorCardId = v.VisitorCardId,
                                       ChurchId = v.ChurchId,
                                       VisitorFirstName = v.VisitorFirstName,
                                       VisitorLastName = v.VisitorLastName,
                                       VisitorEmail = v.VisitorEmail,
                                       VisitorPhone = v.VisitorPhone,
                                       VisitorAddress = v.VisitorAddress,
                                       VisitorCity = v.VisitorCity,
                                       VisitorState = v.VisitorState,
                                       VisitorZip = v.VisitorZip,
                                       ReferalType = (ReferralTypes?)v.ReferralType,
                                       ReasonForAttendance = v.ReasonForAttendance,
                                       Comments = v.Comments,
                                       DateOfAttendance = v.DateOfAttendance,
                                       IsExistingChristian = v.IsExistingChristian,
                                       IsFirstTimeGuest = v.IsFirstTimeGuest,
                                       IsNewToArea = v.IsNewToArea,
                                       IsReturningVisitor = v.IsReturningVisitor
                                   }).FirstAsync();

                return card;
            }
        }


    }
}
