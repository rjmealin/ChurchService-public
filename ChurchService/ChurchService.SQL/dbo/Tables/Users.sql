﻿CREATE TABLE [dbo].[Users] (
    [UserId]                      UNIQUEIDENTIFIER DEFAULT (newid()) NOT NULL,
    [ChurchId]                    UNIQUEIDENTIFIER NOT NULL,  
    [Email]                       NVARCHAR (MAX)   NOT NULL,
    [Phone]                       NVARCHAR (MAX)   NOT NULL,
    [FirstName]                   NVARCHAR (MAX)   NULL,
    [LastName]                    NVARCHAR (MAX)   NULL,
    [Password]                    NVARCHAR (MAX)   NOT NULL,
    [Salt]                        UNIQUEIDENTIFIER NOT NULL,
    [DateCreated]                 DATETIME NOT NULL,
    [Removed]                     BIT              NOT NULL DEFAULT 0 ,
    [Active]                      BIT              NOT NULL DEFAULT 0 ,
    [EmailVerified]               BIT              NOT NULL DEFAULT 0 ,
    [AllowTextNotifications]      BIT              NOT NULL DEFAULT 0 ,
    [PhoneVerified]               BIT              NOT NULL Default 0 ,
    [IsAdmin]                     BIT NOT NULL DEFAULT 0, 
    [ProfileImageDataUrl] NVARCHAR(MAX) NULL, 
    [ProfileImageMIME] NVARCHAR(MAX) NULL, 
    [RemovedDate] DATETIME NULL, 
    [ReasonRemoved] NVARCHAR(MAX) NULL, 
    CONSTRAINT [PK_User] PRIMARY KEY CLUSTERED ([UserId] ASC),
    CONSTRAINT [FK_User_Church] FOREIGN KEY ([ChurchId]) REFERENCES [dbo].[Churches] ([ChurchId]),

);
