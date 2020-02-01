/*

CREATE TABLE dbo.Characters (
	CharacterID INT NOT NULL IDENTITY (1,1) PRIMARY KEY,
	Character_Name NVARCHAR(50) UNIQUE NOT NULL
);

CREATE TABLE dbo.Events (
	EventID INT NOT NULL IDENTITY(1,1) PRIMARY KEY,
	Event_Name NVARCHAR(100) UNIQUE NOT NULL
);

CREATE TABLE dbo.Players (
	PlayerID INT NOT NULL IDENTITY(1,1) PRIMARY KEY,
	Player_Name NVARCHAR(60) UNIQUE NOT NULL
);

CREATE TABLE dbo.Videos (
	VideoID INT NOT NULL IDENTITY(1,1) PRIMARY KEY,
	EventName NVARCHAR(100) NOT NULL,
	P1_Character NVARCHAR(50) NOT NULL,
	P2_Character NVARCHAR(50) NOT NULL,
	WinnerCharacter NVARCHAR(50) NOT NULL,
	FOREIGN KEY (P1_Character)
	REFERENCES dbo.Characters(Character_Name),
	FOREIGN KEY (P2_Character)
	REFERENCES dbo.Characters(Character_Name),
	FOREIGN KEY (WinnerCharacter)
	REFERENCES dbo.Characters(Character_Name),
	P1_Player NVARCHAR(60) NOT NULL,
	P2_Player NVARCHAR(60) NOT NULL,
	WinnerPlayer NVARCHAR(60) NOT NULL,
	VideoLink NVARCHAR(120) NOT NULL
);

GO

INSERT INTO dbo.Characters
	([Character_Name])
VALUES
	(N'Gran'),
	(N'Katalina'),
	(N'Charlotta'),
	(N'Lancelot'),
	(N'Percival'),
	(N'Ferry'),
	(N'Lowain'),
	(N'Ladiva'),
	(N'Metera'),
	(N'Zeta'),
	(N'Vaseraga')
GO

INSERT INTO dbo.Videos
([EventName], [P1_Character], [P2_Character], [WinnerCharacter], [P1_Player], [P2_Player], [WinnerPlayer],
[VideoLink]
)
VALUES
	(N'Granblue Fantasy Fes 2019', N'Metera', N'Ladiva', N'Ladiva', N'Spooky', N'freakmasta', N'freakmasta', N'https://youtu.be/mUYrqn7y4yY?t=35'),
	(N'ArcRevo 2019', N'Lancelot', N'Percival', N'Lancelot', N'Kizzie Kay', N'BeautifulDude', N'Kizzie Kay', N'https://youtu.be/z7jnI4RY32k?t=2172')
GO

These default insertions into the Events and Players table are temporary. I'm just going to be using them to ensure
that my API calls work.

INSERT INTO dbo.Events
([Event_Name])
VALUES
	(N'Granblue Fantasy Fes 2019'),
	(N'ArcRevo 2019')
GO

INSERT INTO dbo.Players
([Player_Name])
VALUES
	(N'Spooky'),
	(N'freakmasta'),
	(N'BeautifulDude'),
	(N'Kizzie Kay')
GO

The information that's placed into these tables is based on the event and player entries from the videos table.

After every video insertion, a query atttempt will be made to insert an event or player into the table. If the
player or Event has been entered into the table before, it won't go through.

CREATE TABLE dbo.VideoTags (
	TagID INT NOT NULL PRIMARY KEY IDENTITY(1,1),
	Tag_Name NVARCHAR(20) NOT NULL UNIQUE
);

GO

INSERT INTO dbo.VideoTags
([Tag_Name])
VALUES
	(N'Perfect'),
	(N'Comeback'),
	(N'Pressure'),
	(N'Spacing')
GO

CREATE TABLE dbo.MapVideoTag (
	MapID INT NOT NULL PRIMARY KEY IDENTITY(1,1),
	Video_ID INT NOT NULL,
	Tag_ID INT NOT NULL,
	FOREIGN KEY (Video_ID)
	REFERENCES dbo.Videos(VideoID),
	FOREIGN KEY (Tag_ID)
	REFERENCES dbo.VideoTags(TagID)
);

GO

*/