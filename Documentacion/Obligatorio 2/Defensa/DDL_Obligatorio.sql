-- DROP SCHEMA dbo;

--CREATE SCHEMA dbo;
-- TaskPanel.dbo.Teams definition

-- Drop table

-- DROP TABLE TaskPanel.dbo.Teams;

--CREATE SEQUENCE IDeleteableSequence
--AS INT
--START WITH 1
--INCREMENT BY 1;

CREATE TABLE Teams (
	Id int IDENTITY(1,1) NOT NULL,
	Name nvarchar(MAX) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	CreatedOn datetime2 NOT NULL,
	TasksDescription nvarchar(MAX) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	MaxUsers int NOT NULL,
	MembersCount int NOT NULL,
	CONSTRAINT PK_Teams PRIMARY KEY (Id)
);


-- TaskPanel.dbo.Trashes definition

-- Drop table

-- DROP TABLE TaskPanel.dbo.Trashes;

CREATE TABLE Trashes (
	Id int IDENTITY(1,1) NOT NULL,
	ElementsCount int NOT NULL,
	CONSTRAINT PK_Trashes PRIMARY KEY (Id)
);


-- TaskPanel.dbo.[__EFMigrationsHistory] definition

-- Drop table

-- DROP TABLE TaskPanel.dbo.[__EFMigrationsHistory];

CREATE TABLE [__EFMigrationsHistory] (
	MigrationId nvarchar(150) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	ProductVersion nvarchar(32) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	CONSTRAINT PK___EFMigrationsHistory PRIMARY KEY (MigrationId)
);


-- TaskPanel.dbo.Users definition

-- Drop table

-- DROP TABLE TaskPanel.dbo.Users;

CREATE TABLE Users (
	Id int IDENTITY(1,1) NOT NULL,
	Name nvarchar(MAX) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	Surname nvarchar(MAX) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	Email nvarchar(MAX) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	BirthDate datetime2 NOT NULL,
	Password nvarchar(MAX) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	Admin bit NOT NULL,
	PaperBinId int NOT NULL,
	CONSTRAINT PK_Users PRIMARY KEY (Id),
	CONSTRAINT FK_Users_Trashes_PaperBinId FOREIGN KEY (PaperBinId) REFERENCES Trashes(Id) ON DELETE CASCADE
);
 CREATE NONCLUSTERED INDEX IX_Users_PaperBinId ON dbo.Users (  PaperBinId ASC  )  
	 WITH (  PAD_INDEX = OFF ,FILLFACTOR = 100  ,SORT_IN_TEMPDB = OFF , IGNORE_DUP_KEY = OFF , STATISTICS_NORECOMPUTE = OFF , ONLINE = OFF , ALLOW_ROW_LOCKS = ON , ALLOW_PAGE_LOCKS = ON  )
	 ON [PRIMARY ] ;


-- TaskPanel.dbo.Notifications definition

-- Drop table

-- DROP TABLE TaskPanel.dbo.Notifications;

CREATE TABLE Notifications (
	Id int IDENTITY(1,1) NOT NULL,
	[Text] nvarchar(MAX) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	FromUserId int NOT NULL,
	ToUserId int NOT NULL,
	CONSTRAINT PK_Notifications PRIMARY KEY (Id),
	CONSTRAINT FK_Notifications_Users_FromUserId FOREIGN KEY (FromUserId) REFERENCES Users(Id),
	CONSTRAINT FK_Notifications_Users_ToUserId FOREIGN KEY (ToUserId) REFERENCES Users(Id) ON DELETE CASCADE
);
 CREATE NONCLUSTERED INDEX IX_Notifications_FromUserId ON dbo.Notifications (  FromUserId ASC  )  
	 WITH (  PAD_INDEX = OFF ,FILLFACTOR = 100  ,SORT_IN_TEMPDB = OFF , IGNORE_DUP_KEY = OFF , STATISTICS_NORECOMPUTE = OFF , ONLINE = OFF , ALLOW_ROW_LOCKS = ON , ALLOW_PAGE_LOCKS = ON  )
	 ON [PRIMARY ] ;
 CREATE NONCLUSTERED INDEX IX_Notifications_ToUserId ON dbo.Notifications (  ToUserId ASC  )  
	 WITH (  PAD_INDEX = OFF ,FILLFACTOR = 100  ,SORT_IN_TEMPDB = OFF , IGNORE_DUP_KEY = OFF , STATISTICS_NORECOMPUTE = OFF , ONLINE = OFF , ALLOW_ROW_LOCKS = ON , ALLOW_PAGE_LOCKS = ON  )
	 ON [PRIMARY ] ;


-- TaskPanel.dbo.Panels definition

-- Drop table

-- DROP TABLE TaskPanel.dbo.Panels;

CREATE TABLE Panels (
	Id int DEFAULT NEXT VALUE FOR [IDeleteableSequence] NOT NULL,
	Name nvarchar(MAX) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	Description nvarchar(MAX) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	IsDeleted bit NOT NULL,
	TrashId int NULL,
	Team nvarchar(MAX) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	CreatedById int NOT NULL,
	TeamId int NULL,
	CONSTRAINT PK_Panels PRIMARY KEY (Id),
	CONSTRAINT FK_Panels_Teams_TeamId FOREIGN KEY (TeamId) REFERENCES Teams(Id),
	CONSTRAINT FK_Panels_Trashes_TrashId FOREIGN KEY (TrashId) REFERENCES Trashes(Id),
	CONSTRAINT FK_Panels_Users_CreatedById FOREIGN KEY (CreatedById) REFERENCES Users(Id) ON DELETE CASCADE
);
 CREATE NONCLUSTERED INDEX IX_Panels_CreatedById ON dbo.Panels (  CreatedById ASC  )  
	 WITH (  PAD_INDEX = OFF ,FILLFACTOR = 100  ,SORT_IN_TEMPDB = OFF , IGNORE_DUP_KEY = OFF , STATISTICS_NORECOMPUTE = OFF , ONLINE = OFF , ALLOW_ROW_LOCKS = ON , ALLOW_PAGE_LOCKS = ON  )
	 ON [PRIMARY ] ;
 CREATE NONCLUSTERED INDEX IX_Panels_TeamId ON dbo.Panels (  TeamId ASC  )  
	 WITH (  PAD_INDEX = OFF ,FILLFACTOR = 100  ,SORT_IN_TEMPDB = OFF , IGNORE_DUP_KEY = OFF , STATISTICS_NORECOMPUTE = OFF , ONLINE = OFF , ALLOW_ROW_LOCKS = ON , ALLOW_PAGE_LOCKS = ON  )
	 ON [PRIMARY ] ;
 CREATE NONCLUSTERED INDEX IX_Panels_TrashId ON dbo.Panels (  TrashId ASC  )  
	 WITH (  PAD_INDEX = OFF ,FILLFACTOR = 100  ,SORT_IN_TEMPDB = OFF , IGNORE_DUP_KEY = OFF , STATISTICS_NORECOMPUTE = OFF , ONLINE = OFF , ALLOW_ROW_LOCKS = ON , ALLOW_PAGE_LOCKS = ON  )
	 ON [PRIMARY ] ;


-- TaskPanel.dbo.TeamUser definition

-- Drop table

-- DROP TABLE TaskPanel.dbo.TeamUser;

CREATE TABLE TeamUser (
	TeamMembersId int NOT NULL,
	UserTeamsId int NOT NULL,
	CONSTRAINT PK_TeamUser PRIMARY KEY (TeamMembersId,UserTeamsId),
	CONSTRAINT FK_TeamUser_Teams_UserTeamsId FOREIGN KEY (UserTeamsId) REFERENCES Teams(Id) ON DELETE CASCADE,
	CONSTRAINT FK_TeamUser_Users_TeamMembersId FOREIGN KEY (TeamMembersId) REFERENCES Users(Id) ON DELETE CASCADE
);
 CREATE NONCLUSTERED INDEX IX_TeamUser_UserTeamsId ON dbo.TeamUser (  UserTeamsId ASC  )  
	 WITH (  PAD_INDEX = OFF ,FILLFACTOR = 100  ,SORT_IN_TEMPDB = OFF , IGNORE_DUP_KEY = OFF , STATISTICS_NORECOMPUTE = OFF , ONLINE = OFF , ALLOW_ROW_LOCKS = ON , ALLOW_PAGE_LOCKS = ON  )
	 ON [PRIMARY ] ;


-- TaskPanel.dbo.Epic definition

-- Drop table

-- DROP TABLE TaskPanel.dbo.Epic;

CREATE TABLE Epic (
	Id int IDENTITY(1,1) NOT NULL,
	Name nvarchar(MAX) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	Priority int NOT NULL,
	Description nvarchar(MAX) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	ExpirationDate datetime2 NOT NULL,
	FromPanelId int NOT NULL,
	CONSTRAINT PK_Epic PRIMARY KEY (Id),
	CONSTRAINT FK_Epic_Panels_FromPanelId FOREIGN KEY (FromPanelId) REFERENCES Panels(Id) ON DELETE CASCADE
);
 CREATE NONCLUSTERED INDEX IX_Epic_FromPanelId ON dbo.Epic (  FromPanelId ASC  )  
	 WITH (  PAD_INDEX = OFF ,FILLFACTOR = 100  ,SORT_IN_TEMPDB = OFF , IGNORE_DUP_KEY = OFF , STATISTICS_NORECOMPUTE = OFF , ONLINE = OFF , ALLOW_ROW_LOCKS = ON , ALLOW_PAGE_LOCKS = ON  )
	 ON [PRIMARY ] ;


-- TaskPanel.dbo.Tasks definition

-- Drop table

-- DROP TABLE TaskPanel.dbo.Tasks;

CREATE TABLE Tasks (
	Id int DEFAULT NEXT VALUE FOR [IDeleteableSequence] NOT NULL,
	Name nvarchar(MAX) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	Description nvarchar(MAX) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	IsDeleted bit NOT NULL,
	TrashId int NULL,
	Precedence int NOT NULL,
	PanelId int NOT NULL,
	ExpirationDate datetime2 NOT NULL,
	IsInEpic bit NOT NULL,
	ExpectedEffort int NOT NULL,
	InvertedEffort int NOT NULL,
	Ended bit NOT NULL,
	EpicId int NULL,
	CONSTRAINT PK_Tasks PRIMARY KEY (Id),
	CONSTRAINT FK_Tasks_Epic_EpicId FOREIGN KEY (EpicId) REFERENCES Epic(Id),
	CONSTRAINT FK_Tasks_Panels_PanelId FOREIGN KEY (PanelId) REFERENCES Panels(Id) ON DELETE CASCADE,
	CONSTRAINT FK_Tasks_Trashes_TrashId FOREIGN KEY (TrashId) REFERENCES Trashes(Id)
);
 CREATE NONCLUSTERED INDEX IX_Tasks_EpicId ON dbo.Tasks (  EpicId ASC  )  
	 WITH (  PAD_INDEX = OFF ,FILLFACTOR = 100  ,SORT_IN_TEMPDB = OFF , IGNORE_DUP_KEY = OFF , STATISTICS_NORECOMPUTE = OFF , ONLINE = OFF , ALLOW_ROW_LOCKS = ON , ALLOW_PAGE_LOCKS = ON  )
	 ON [PRIMARY ] ;
 CREATE NONCLUSTERED INDEX IX_Tasks_PanelId ON dbo.Tasks (  PanelId ASC  )  
	 WITH (  PAD_INDEX = OFF ,FILLFACTOR = 100  ,SORT_IN_TEMPDB = OFF , IGNORE_DUP_KEY = OFF , STATISTICS_NORECOMPUTE = OFF , ONLINE = OFF , ALLOW_ROW_LOCKS = ON , ALLOW_PAGE_LOCKS = ON  )
	 ON [PRIMARY ] ;
 CREATE NONCLUSTERED INDEX IX_Tasks_TrashId ON dbo.Tasks (  TrashId ASC  )  
	 WITH (  PAD_INDEX = OFF ,FILLFACTOR = 100  ,SORT_IN_TEMPDB = OFF , IGNORE_DUP_KEY = OFF , STATISTICS_NORECOMPUTE = OFF , ONLINE = OFF , ALLOW_ROW_LOCKS = ON , ALLOW_PAGE_LOCKS = ON  )
	 ON [PRIMARY ] ;


-- TaskPanel.dbo.Comments definition

-- Drop table

-- DROP TABLE TaskPanel.dbo.Comments;

CREATE TABLE Comments (
	Id int IDENTITY(1,1) NOT NULL,
	CreatedById int NULL,
	Resolved bit NOT NULL,
	ResolvedById int NULL,
	TaskId int NOT NULL,
	ResolvedTime datetime2 NOT NULL,
	Content nvarchar(MAX) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	CONSTRAINT PK_Comments PRIMARY KEY (Id),
	CONSTRAINT FK_Comments_Tasks_TaskId FOREIGN KEY (TaskId) REFERENCES Tasks(Id) ON DELETE CASCADE,
	CONSTRAINT FK_Comments_Users_CreatedById FOREIGN KEY (CreatedById) REFERENCES Users(Id),
	CONSTRAINT FK_Comments_Users_ResolvedById FOREIGN KEY (ResolvedById) REFERENCES Users(Id)
);
 CREATE NONCLUSTERED INDEX IX_Comments_CreatedById ON dbo.Comments (  CreatedById ASC  )  
	 WITH (  PAD_INDEX = OFF ,FILLFACTOR = 100  ,SORT_IN_TEMPDB = OFF , IGNORE_DUP_KEY = OFF , STATISTICS_NORECOMPUTE = OFF , ONLINE = OFF , ALLOW_ROW_LOCKS = ON , ALLOW_PAGE_LOCKS = ON  )
	 ON [PRIMARY ] ;
 CREATE NONCLUSTERED INDEX IX_Comments_ResolvedById ON dbo.Comments (  ResolvedById ASC  )  
	 WITH (  PAD_INDEX = OFF ,FILLFACTOR = 100  ,SORT_IN_TEMPDB = OFF , IGNORE_DUP_KEY = OFF , STATISTICS_NORECOMPUTE = OFF , ONLINE = OFF , ALLOW_ROW_LOCKS = ON , ALLOW_PAGE_LOCKS = ON  )
	 ON [PRIMARY ] ;
 CREATE NONCLUSTERED INDEX IX_Comments_TaskId ON dbo.Comments (  TaskId ASC  )  
	 WITH (  PAD_INDEX = OFF ,FILLFACTOR = 100  ,SORT_IN_TEMPDB = OFF , IGNORE_DUP_KEY = OFF , STATISTICS_NORECOMPUTE = OFF , ONLINE = OFF , ALLOW_ROW_LOCKS = ON , ALLOW_PAGE_LOCKS = ON  )
	 ON [PRIMARY ] ;