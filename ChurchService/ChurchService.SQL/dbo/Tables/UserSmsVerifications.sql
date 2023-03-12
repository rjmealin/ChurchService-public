CREATE TABLE [dbo].[UserSmsVerifications]
(
    [UserSmsVerificationId] UNIQUEIDENTIFIER NOT NULL DEFAULT NewId(), 
    [UserId] UNIQUEIDENTIFIER NOT NULL, 
    [PhoneNumber] NVARCHAR(MAX) NOT NULL, 
    [Verified] BIT NOT NULL DEFAULT 0, 
    [DateCreated] DATETIME NOT NULL, 
    [Expiration] DATETIME NOT NULL, 
    [Active] BIT NOT NULL DEFAULT 0,
    [VerificationCode] INT NOT NULL, 
    CONSTRAINT [PK_UserSmsVerification] PRIMARY KEY CLUSTERED ([UserSmsVerificationId] ASC), 
	CONSTRAINT [FK_UserSmsVerifications_User] FOREIGN KEY ([UserId]) REFERENCES [Users]([UserId]),
)
