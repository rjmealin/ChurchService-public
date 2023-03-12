CREATE TABLE [dbo].[UnavailableDates]
(
	[UnavailableDateId] UNIQUEIDENTIFIER NOT NULL DEFAULT NEWID(), 
    [UserId] UNIQUEIDENTIFIER NOT NULL, 
    [DateUnavailable] DATETIME NOT NULL,
    CONSTRAINT [PK_UnavaibleDate] PRIMARY KEY CLUSTERED ([UnavailableDateId] ASC),
    CONSTRAINT [FK_User_unavaiable] FOREIGN KEY ([UserId]) REFERENCES [dbo].[Users] ([UserId]),
)
