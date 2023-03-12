using Microsoft.AspNetCore.Mvc;
using ChurchService.Library;
using ChurchService.Library.Models.Users;
using ChurchService.Library.Models.Visitors;
using ChurchService.Library.Models.Churches;

namespace ChurchServiceApi.Controllers
{
    [ApiController]
    [Route("church")]
    public class ChurchController : BaseController
    {
        /// <summary>
        ///     Saves a visitors card to the database
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("visitor-card")]
        public async Task<IActionResult> SaveVisitorCard(VisitorCardModel model)
        {
            try
            {

                await Visitors.AddVisitorCardAsync(model, CurrentJsonWebToken.ChurchId);

                return NoContent();

            }
            catch (Exception ex)
            {
                LogException(ex);
                return ReturnException(ex);
            }
        }









        /// <summary>
        ///     Gets a list of all visitor cards attached to a church
        /// </summary>
        ///
        [HttpGet]
        [Route("visitor-cards")]
        public async Task<IActionResult> GetVisitorCards()
        {
            try
            {

                var cards = await Visitors.GetAllVisitorCardsForChurch(CurrentJsonWebToken.ChurchId);

                return Ok(cards);

            }
            catch (Exception ex)
            {
                LogException(ex);
                return ReturnException(ex);
            }
        }










        /// <summary>
        ///     Removes a visitor vard based off of its database id
        /// </summary>
        /// <param name="cardId">the database id of the card to remove</param>
        [HttpDelete]
        [Route("remove-card/{cardId}")]
        public async Task<IActionResult> RemoveVisitorCardAsync(Guid cardId) 
        {
            try
            {
                if (!CurrentJsonWebToken.IsAdmin)
                    throw new UnauthorizedAccessException("You do not have permission to do this!");

                await Visitors.RemoveVisitorCardAsync(cardId);

                return NoContent();

            }
            catch (Exception ex)
            {
                LogException(ex);
                return ReturnException(ex);
            }
        }










        /// <summary>
        ///     Attempts to create a church with the incoming model
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("add-church")]
        public async Task<IActionResult> AddChurchAsync(AddChurchModel model)
        {
            try
            {

                await model.CreateChurch();

                return NoContent();

            }
            catch (Exception ex)
            {
                LogException(ex);
                return ReturnException(ex);
            }
        }









        /// <summary>
        ///     Adds a planned visit to the database
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("add-planned-visit")]
        public async Task<IActionResult> AddPlannedVisitAsync(AddPlannedVisitModel model)
        {
            try
            {
                await PlannedVisits.AddPlannedVisit(model);

                var churchEmail = await Church.GetChurchEmail(model.ChurchId);

                await Common.Email.SendGridEmail("New Planned Visit!", churchEmail , $"There is a new planned visit scheduled! Scheduled on {model.VisitDate.ToLongDateString()}", false);

                //await Common.Email.SendEmail(churchEmail, "New Planned visit!", "You have a new planned visit scheduled!");

                return NoContent();

            }
            catch (Exception ex)
            {
                LogException(ex);
                return ReturnException(ex);
            }
        }









        /// <summary>
        ///     Gets a planned visit details
        /// </summary>
        /// <param name="visitId">the database id of the visit to get</param>
        [HttpGet]
        [Route("get-visit/{visitId}")]
        public async Task<IActionResult> GetPlannedVisitDetails(Guid visitId)
        {
            try
            {
                var visit = await PlannedVisits.GetPlannedVisitAsync(visitId);

                return Ok(visit);

            }
            catch (Exception ex)
            {
                LogException(ex);
                return ReturnException(ex);
            }
        }










        /// <summary>
        ///     Gets all the planned visits
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("planned-visits")]
        public async Task<IActionResult> GetPlannedVisitsAsync() 
        {
            try
            {
                return Ok(await PlannedVisits.GetUpcomingPlannedVisitsAsync(CurrentJsonWebToken.ChurchId));
            }
            catch (Exception ex)
            {
                LogException(ex);
                return ReturnException(ex);
            }
        }










        /// <summary>
        ///     Gets all historical visits
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("past-visits")]
        public async Task<IActionResult> GetPastVisitsAsync()
        {
            try
            {
                return Ok(await PlannedVisits.GetAllPastVisits(CurrentJsonWebToken.ChurchId));
            }
            catch (Exception ex) 
            {
                LogException(ex);
                return ReturnException(ex);
            }
        }










        /// <summary>
        ///     
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("unassigned-visits")]
        public async Task<IActionResult> GetUnassignedVisitCountAsync()
        {
            try
            {
                return Ok(await PlannedVisits.GetUnassignedPlannedVisitCount(CurrentJsonWebToken.ChurchId));
            }
            catch (Exception ex)
            {
                LogException(ex);
                return ReturnException(ex);
            }
        }









        /// <summary>
        ///     Gets all the members attached to a given church, who are not admin
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("members")]
        public async Task<IActionResult> GetChurchMembersAsync() 
        {
            try
            {

                if (!CurrentJsonWebToken.IsAdmin)
                    throw new UnauthorizedAccessException("You do not have permission to do this!");

                var members = await Church.GetChurchMembersAsync(CurrentJsonWebToken.ChurchId);

                return Ok(members);
            }
            catch (Exception ex)
            {
                LogException(ex);
                return ReturnException(ex);
            }
        }










        /// <summary>
        ///     Removes a member from a church
        /// </summary>
        /// <param name="memberId">The database id of the member to remove</param>
        /// 
        [HttpPatch]
        [Route("remove-member/{memberId}")]
        public async Task<IActionResult> RemoveMemberAsync(Guid memberId)
        {
            try
            {

                if (!CurrentJsonWebToken.IsAdmin)
                    throw new UnauthorizedAccessException("You do not have permission to do this!");

                await Church.RemoveMemberAsync(memberId);

                return NoContent();
            }
            catch (Exception ex)
            {
                LogException(ex);
                return ReturnException(ex);
            }
        }










        /// <summary>
        ///     Uses the incoming model to register a user with a church
        /// </summary>
        /// 
        [HttpPost]
        [Route("add-member")]
        public async Task<IActionResult> AddMemberAsync(AddMemberModel model) 
        {
            try
            {

                if (!CurrentJsonWebToken.IsAdmin)
                    throw new UnauthorizedAccessException("You do not have permission to do this!");
                
                await model.CreateChurchMemberAsync(CurrentJsonWebToken.ChurchId);

                return NoContent();
            }
            catch (Exception ex)
            {
                LogException(ex);
                return ReturnException(ex);
            }
        }








        /// <summary>
        ///     Gets the avaible members for a given date
        /// </summary>
        /// <param name="visitDate"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("available-members")]
        public async Task<IActionResult> GetAvailableMembers(DateTime visitDate)
        {
            try
            {
                var members = await Church.GetAllAvailableMembers(CurrentJsonWebToken.ChurchId, visitDate);

                return Ok(members);

            }
            catch (Exception ex)
            {
                LogException(ex);
                return ReturnException(ex);
            }
        }










        /// <summary>
        ///     Assigns a member to a visit and emails the visitor and member to notify them of this update
        /// </summary>
        /// <param name="visitId">The database id of the planned visit</param>
        /// <param name="memberId">the database id of the member</param>
        /// <returns></returns>
        [HttpPatch]
        [Route("Assign-visit/{visitId}/{memberId}")]
        public async Task<IActionResult> AssignMemberToVisit(Guid visitId, Guid memberId)
        {
            try
            {
                var data = await Church.AssignPlannedVisit(memberId, visitId);

                await Common.Email.SendGridEmail("Assigned Visit", data.userEmail, "You have been assigned to a planned visit!", false);

                //The email send to the visitor will be special, might throw in the church logo etc...
                if(data.profiledataUrl != null)
                    await Common.Email.SendVisitorAssignmentEmailAsync(data.visitorEmail, data.memberName, data.visitDate, data.userEmail, data.churchName);
                else
                    await Common.Email.SendVisitorAssignmentEmailAsync(data.visitorEmail, data.memberName, data.visitDate, data.userEmail, data.churchName);

                return NoContent();
            }
            catch (Exception ex)
            {
                LogException(ex);
                return ReturnException(ex);
            }
        }










        /// <summary>
        ///     removes the assigned member from the visit    
        /// </summary>
        /// <param name="visitId">the database id of the planned visit</param>
        /// 
        [HttpPatch]
        [Route("UnassignVisit/{visitId}")]
        public async Task<IActionResult> UnassignMemberFromVisitAsync(Guid visitId) 
        {
            try
            {

                if (!CurrentJsonWebToken.IsAdmin)
                    throw new UnauthorizedAccessException("You do not have permission to do this!");

                await Church.UnassignVisitAsync(visitId);

                return NoContent();
            }
            catch (Exception ex)
            {
                LogException(ex);
                return ReturnException(ex);
            }

        }









        /// <summary>
        ///     Updates the church profile with the incoming model
        /// </summary>
        /// 
        [HttpPut]
        [Route("update-church")]
        public async Task<IActionResult> UpdateChurchProfileAsync(ChurchModel model)
        {
            try
            {

                if (!CurrentJsonWebToken.IsAdmin)
                    throw new UnauthorizedAccessException("You do not have permission to do this!");

                await Church.UpdateChurchAsync(CurrentJsonWebToken.ChurchId, model);

                return NoContent();
            }
            catch (Exception ex)
            {
                LogException(ex);
                return ReturnException(ex);
            }
        }










        /// <summary>
        ///     Gets the church profile with the jwt
        /// </summary>
        /// <returns></returns>
        /// <exception cref="UnauthorizedAccessException"></exception>
        [HttpGet]
        [Route("get-church-profile")]
        public async Task<IActionResult> GetChurchProfileAsync()
        {
            try
            {

                if (!CurrentJsonWebToken.IsAdmin)
                    throw new UnauthorizedAccessException("You do not have permission to do this!");

                var model = await Church.GetChurchProfileAsync(CurrentJsonWebToken.ChurchId);

                return Ok(model);
            }
            catch (Exception ex)
            {
                LogException(ex);
                return ReturnException(ex);
            }

        }



         






        /// <summary>
        ///     Upgrades a member to admin status
        /// </summary>
        /// <param name="memberId">The database id of the </param>
        /// <returns></returns>
        /// <exception cref="UnauthorizedAccessException"></exception>
        [HttpPatch]
        [Route("upgrade-member/{memberId}")]
        public async Task<IActionResult> UpgradeMemberToAdminAsync(Guid memberId) 
        {
            try
            {

                if (!CurrentJsonWebToken.IsAdmin)
                    throw new UnauthorizedAccessException("You do not have permission to do this!");

                await Church.UpgradeMemberToAdmin(memberId);

                return NoContent();
            }
            catch (Exception ex)
            {
                LogException(ex);
                return ReturnException(ex);
            }
        }











        /// <summary>
        ///     Gets all the available members on a given date
        /// </summary>
        /// <param name="date">The date in which to check for available members</param>
        /// 
        [HttpGet]
        [Route("get-available-members/{date}")]
        public async Task<IActionResult> getAllAvailableMembers(DateTime date) 
        {
            try
            {

                if (!CurrentJsonWebToken.IsAdmin)
                    throw new UnauthorizedAccessException("You do not have permission to do this!");

                var members = await Church.GetAllAvailableMembers(CurrentJsonWebToken.ChurchId, date);
                return Ok(members);

            }
            catch (Exception ex)
            {
                LogException(ex);
                return ReturnException(ex);
            }
        }










        /// <summary>
        ///     Gets the data url of the church logo
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("logo")]
        public async Task<IActionResult> GetChurchLogoAsync() 
        {
            try
            {
                var dataUrl = await Church.GetChurchLogoAsync(CurrentJsonWebToken.ChurchId);
                return Ok(dataUrl);

            }
            catch (Exception ex)
            {
                LogException(ex);
                return ReturnException(ex);
            }
        }









        /// <summary>
        ///     Gets the details about a given visitor card
        /// </summary>
        /// <param name="cardId">teh database id of the card to get</param>
        [HttpGet]
        [Route("visitor-card/{cardId}")]
        public async Task<IActionResult> GetVisitorCardDetails(Guid cardId) 
        {
            try
            {
                var card = await Visitors.GetVisitorCardDetails(cardId);
                return Ok(card);

            }
            catch (Exception ex)
            {
                LogException(ex);
                return ReturnException(ex);
            }
        }
    }
}
