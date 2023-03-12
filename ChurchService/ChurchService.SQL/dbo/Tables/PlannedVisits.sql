CREATE TABLE [dbo].[PlannedVisits]
(
	[PlannedVisitId] uniqueIdentifier NOT NULL default newid(), 
    [ChurchId] UNIQUEIDENTIFIER NOT NULL, 
    [AssignedUserId] UNIQUEIDENTIFIER NULL, 
    [VisitDate] DATETIME NOT NULL, 
    [VisitorFirstName] NVARCHAR(MAX) NULL, 
    [VisitorLastName] NVARCHAR(MAX) NULL, 
    [VisitorPhone] NVARCHAR(MAX) NULL, 
    [VisitorEmail] NVARCHAR(MAX) NULL,

    [CommentsOrQuestions] NVARCHAR(MAX) NULL, 
    CONSTRAINT [PK_PlannedVisit] PRIMARY KEY CLUSTERED ([PlannedVisitId] ASC),
    CONSTRAINT [FK_PlannedVisit_Church] FOREIGN KEY ([ChurchId]) REFERENCES [dbo].[Churches] ([ChurchId]),
    CONSTRAINT [FK_PlannedVisit_AssignedUser] FOREIGN KEY ([AssignedUserId]) REFERENCES [dbo].[Users] ([UserId]),


)
