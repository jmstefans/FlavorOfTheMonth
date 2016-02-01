USE [fotm]
GO

/****** Object:  Table [dbo].[PvpStats]    Script Date: 2/01/2016 10:00:00 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[PvpStats](
	[PvpStatsID] [bigint] NOT NULL IDENTITY (1, 1),
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
 CONSTRAINT [PK_PvpStats] PRIMARY KEY CLUSTERED 
(
	[PvpStatsID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO