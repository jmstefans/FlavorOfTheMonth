USE [fotm]
GO

/****** Object:  Table [dbo].[PvpStats]    Script Date: 2/2/2016 4:35:31 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[PvpStats](
	[PvpStatsID] [bigint] IDENTITY(1,1) NOT NULL,
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
	[Class] [nvarchar](50) NULL,
	[Race] [nvarchar](50) NULL,
	[Gender] [nvarchar](50) NULL,
	[Spec] [nvarchar](50) NULL,
	[ModifiedDate] [datetime] NULL,
	[ModifiedStatus] [char](1) NULL,
	[ModifiedUserID] [bigint] NULL,
	[CharacterID] [bigint] NULL,
 CONSTRAINT [PK_PvpStats] PRIMARY KEY CLUSTERED 
(
	[PvpStatsID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO

