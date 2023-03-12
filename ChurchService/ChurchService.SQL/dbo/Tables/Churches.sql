CREATE TABLE [dbo].[Churches]
(
	[ChurchId] UNIQUEIDENTIFIER NOT NULL DEFAULT NEWID(), 
    [Name] NVARCHAR(MAX) NOT NULL,
    [Email] NVARCHAR(MAX) NULL, 
    [PhoneNumber] NVARCHAR(MAX) NULL, 
    [Address] NVARCHAR(MAX) NULL, 
    [City] NVARCHAR(MAX) NULL, 
    [State] NVARCHAR(MAX) NULL, 
    [Zip] NVARCHAR(MAX) NULL, 
    [Active] BIT NOT NULL DEFAULT 1, 
    [Verified] BIT NOT NULL, 
    [Removed] BIT NOT NULL DEFAULT 0, 
    [ChurchLogoDataUrl] NVARCHAR(MAX) NULL, 
    [ChurchLogoMIME] NVARCHAR(MAX) NULL, 
    constraint [PK_Church] Primary key clustered ([ChurchId] ASC)
)
