CREATE TABLE [dbo].[UserEmailVerifications]
(
	[UserEmailVerificationId] UNIQUEIDENTIFIER NOT NULL DEFAULT NewId(), 
    [UserId] UNIQUEIDENTIFIER NOT NULL, 
    [Email] NVARCHAR(MAX) NULL, 
    [Verified] BIT NOT NULL DEFAULT 0, 
    [DateCreated] DATETIME NOT NULL, 
    [Expiration] DATETIME NOT NULL, 
    [Active] BIT NOT NULL DEFAULT 0,
    CONSTRAINT [PK_UserEmailVerification] PRIMARY KEY CLUSTERED ([UserEmailVerificationId] ASC), 
	CONSTRAINT [FK_UserEmailVerifications_User] FOREIGN KEY ([UserId]) REFERENCES [Users]([UserId]),
)
