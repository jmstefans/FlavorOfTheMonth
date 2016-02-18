USE [fotm]
GO

/****** Object:  Table [dbo].[Leaderboard]    Script Date: 1/20/2016 11:56:08 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Leaderboard](
	[LeaderboardID] [bigint] NOT NULL IDENTITY (1, 1),
	[Ranking] [nvarchar](50) NULL,
	[Rating] [nvarchar](50) NULL,
	[Name] [nvarchar](100) NULL,
	[RealmID] [nvarchar](50) NULL,
	[RealmName] [nvarchar](50) NULL,
	[RealmSlug] [nvarchar](50) NULL,
	[RaceID] [int] NULL,
	[ClassID] [int] NULL,
	[SpecID] [int] NULL,
	[FactionID] [int] NULL,
	[GenderID] [int] NULL,
	[SeasonWins] [int] NULL,
	[SeasonLosses] [int] NULL,
	[WeeklyWins] [int] NULL,
	[WeeklyLosses] [int] NULL,
	[ModifiedDate] [datetime] NULL,
	[ModifiedStatus] [char] NULL,
	[ModifiedUserID] [bigint] NULL
 CONSTRAINT [PK_Leaderboard] PRIMARY KEY CLUSTERED 
(
	[LeaderboardID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO


