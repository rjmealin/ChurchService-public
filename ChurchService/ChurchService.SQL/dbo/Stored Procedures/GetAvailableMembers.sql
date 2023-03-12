CREATE PROCEDURE [dbo].[GetAvailableMembers]
	@visitDate Date,
	@churchId uniqueIdentifier
AS
	select 
		u.UserId,
		CONCAT(u.FirstName, ' ', u.LastName) as FullName
	from 
		Users as u
	inner join 
		Churches as c on c.ChurchId = u.ChurchId
	left join 
		UnavailableDates as ud on ud.UserId = u.UserId and ud.DateUnavailable = @visitDate
	where c.ChurchId = @churchId and ud.DateUnavailable is null and u.IsAdmin = 0
	

RETURN 0
