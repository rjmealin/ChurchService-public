CREATE TABLE [dbo].[ErrorLogs] (
    [ErrorLogId]       UNIQUEIDENTIFIER NOT NULL DEFAULT NEWID(),
    [ParentErrorLogId] UNIQUEIDENTIFIER NULL,
    [ExceptionMessage] NVARCHAR (MAX)   NOT NULL,
    [StackTrace]       NVARCHAR (MAX)   NOT NULL,
    [TimeStamp]        DATETIME         NOT NULL,
    [UserId]           UNIQUEIDENTIFIER NULL,
    [UserIpAddress]    NVARCHAR (50)    NULL,
    [ViewModel]        NVARCHAR (MAX)   NULL,
    [URL]              NVARCHAR (MAX)   NULL,
    CONSTRAINT [PK_ErrorLog] PRIMARY KEY CLUSTERED ([ErrorLogId] ASC)
);
