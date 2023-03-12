CREATE TABLE [dbo].[UserPasswordResets] (
    [UserPasswordResetId] UNIQUEIDENTIFIER NOT NULL DEFAULT (newid()),
    [UserId]              UNIQUEIDENTIFIER NOT NULL,
    [DateCreated]         DATETIME         NOT NULL,
    [Expiration]          DATETIME         NOT NULL,
    [Email]               NVARCHAR (MAX)   NULL,
    [Active]              BIT              NOT NULL,
    CONSTRAINT [PK_UserPasswordReset] PRIMARY KEY CLUSTERED ([UserPasswordResetId] ASC),
    CONSTRAINT [FK_User_UserPasswordResets] FOREIGN KEY ([UserId]) REFERENCES [dbo].[Users] ([UserId])
);

