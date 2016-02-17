USE [master]
GO

/****** Object:  Database [fotm]    Script Date: 2/16/2016 5:32:22 PM ******/
CREATE DATABASE [fotm]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'fotm', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL11.MSSQLSERVER\MSSQL\DATA\fotm.mdf' , SIZE = 3072KB , MAXSIZE = UNLIMITED, FILEGROWTH = 1024KB )
 LOG ON 
( NAME = N'fotm_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL11.MSSQLSERVER\MSSQL\DATA\fotm_log.ldf' , SIZE = 1024KB , MAXSIZE = 2048GB , FILEGROWTH = 10%)
GO

ALTER DATABASE [fotm] SET COMPATIBILITY_LEVEL = 110
GO

IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [fotm].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO

ALTER DATABASE [fotm] SET ANSI_NULL_DEFAULT OFF 
GO

ALTER DATABASE [fotm] SET ANSI_NULLS OFF 
GO

ALTER DATABASE [fotm] SET ANSI_PADDING OFF 
GO

ALTER DATABASE [fotm] SET ANSI_WARNINGS OFF 
GO

ALTER DATABASE [fotm] SET ARITHABORT OFF 
GO

ALTER DATABASE [fotm] SET AUTO_CLOSE OFF 
GO

ALTER DATABASE [fotm] SET AUTO_CREATE_STATISTICS ON 
GO

ALTER DATABASE [fotm] SET AUTO_SHRINK OFF 
GO

ALTER DATABASE [fotm] SET AUTO_UPDATE_STATISTICS ON 
GO

ALTER DATABASE [fotm] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO

ALTER DATABASE [fotm] SET CURSOR_DEFAULT  GLOBAL 
GO

ALTER DATABASE [fotm] SET CONCAT_NULL_YIELDS_NULL OFF 
GO

ALTER DATABASE [fotm] SET NUMERIC_ROUNDABORT OFF 
GO

ALTER DATABASE [fotm] SET QUOTED_IDENTIFIER OFF 
GO

ALTER DATABASE [fotm] SET RECURSIVE_TRIGGERS OFF 
GO

ALTER DATABASE [fotm] SET  DISABLE_BROKER 
GO

ALTER DATABASE [fotm] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO

ALTER DATABASE [fotm] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO

ALTER DATABASE [fotm] SET TRUSTWORTHY OFF 
GO

ALTER DATABASE [fotm] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO

ALTER DATABASE [fotm] SET PARAMETERIZATION SIMPLE 
GO

ALTER DATABASE [fotm] SET READ_COMMITTED_SNAPSHOT OFF 
GO

ALTER DATABASE [fotm] SET HONOR_BROKER_PRIORITY OFF 
GO

ALTER DATABASE [fotm] SET RECOVERY FULL 
GO

ALTER DATABASE [fotm] SET  MULTI_USER 
GO

ALTER DATABASE [fotm] SET PAGE_VERIFY CHECKSUM  
GO

ALTER DATABASE [fotm] SET DB_CHAINING OFF 
GO

ALTER DATABASE [fotm] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO

ALTER DATABASE [fotm] SET TARGET_RECOVERY_TIME = 0 SECONDS 
GO

ALTER DATABASE [fotm] SET  READ_WRITE 
GO

USE [fotm]
GO

/****** Object:  Table [dbo].[Class]    Script Date: 2/16/2016 5:32:36 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Class](
	[ClassID] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
	[ModifiedDate] [datetime2](7) NOT NULL,
	[ModifiedStatus] [nchar](10) NOT NULL,
	[ModifiedUserID] [bigint] NOT NULL,
 CONSTRAINT [PK_Class] PRIMARY KEY CLUSTERED 
(
	[ClassID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

USE [fotm]
GO

/****** Object:  Table [dbo].[Faction]    Script Date: 2/16/2016 5:32:52 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Faction](
	[FactionID] [bit] NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
	[ModifiedDate] [datetime2](7) NOT NULL,
	[ModifiedStatus] [nchar](10) NOT NULL,
	[ModifiedUserID] [bigint] NOT NULL,
 CONSTRAINT [PK_Faction] PRIMARY KEY CLUSTERED 
(
	[FactionID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

USE [fotm]
GO

/****** Object:  Table [dbo].[Gender]    Script Date: 2/16/2016 5:33:00 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Gender](
	[GenderID] [bit] NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
	[ModifiedDate] [datetime2](7) NOT NULL,
	[ModifiedStatus] [nchar](10) NOT NULL,
	[ModifiedUserID] [bigint] NOT NULL,
 CONSTRAINT [PK_Gender] PRIMARY KEY CLUSTERED 
(
	[GenderID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

USE [fotm]
GO

/****** Object:  Table [dbo].[Race]    Script Date: 2/16/2016 5:33:13 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Race](
	[RaceID] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
	[ModifiedDate] [datetime2](7) NOT NULL,
	[ModifiedStatus] [nchar](10) NOT NULL,
	[ModifiedUserID] [bigint] NOT NULL,
 CONSTRAINT [PK_RaceID] PRIMARY KEY CLUSTERED 
(
	[RaceID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

USE [fotm]
GO

/****** Object:  Table [dbo].[Realm]    Script Date: 2/16/2016 5:33:22 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Realm](
	[RealmID] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
	[ModifiedDate] [datetime2](7) NOT NULL,
	[ModifiedStatus] [nchar](10) NOT NULL,
	[ModifiedUserID] [bigint] NOT NULL,
 CONSTRAINT [PK_Realm] PRIMARY KEY CLUSTERED 
(
	[RealmID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

USE [fotm]
GO

/****** Object:  Table [dbo].[Spec]    Script Date: 2/16/2016 5:33:32 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Spec](
	[SpecID] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
	[BlizzName] [nvarchar](50) NOT NULL,
	[ModifiedDate] [datetime2](7) NOT NULL,
	[ModifiedStatus] [nchar](10) NOT NULL,
	[ModifiedUserID] [bigint] NOT NULL,
 CONSTRAINT [PK_Spec] PRIMARY KEY CLUSTERED 
(
	[SpecID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

USE [fotm]
GO

/****** Object:  Table [dbo].[Team]    Script Date: 2/16/2016 5:33:42 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Team](
	[TeamID] [bigint] IDENTITY(1,1) NOT NULL,
	[Bracket] [nvarchar](50) NOT NULL,
	[MeanRatingChange] [float] NOT NULL,
	[MeanRating] [float] NOT NULL,
	[ModifiedDate] [datetime2](7) NOT NULL,
	[ModifiedStatus] [nchar](10) NOT NULL,
	[ModifiedUserID] [bigint] NOT NULL,
 CONSTRAINT [PK_Team] PRIMARY KEY CLUSTERED 
(
	[TeamID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

USE [fotm]
GO

/****** Object:  Table [dbo].[Character]    Script Date: 2/16/2016 5:34:38 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Character](
	[CharacterID] [bigint] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
	[RealmID] [int] NOT NULL,
	[SpecID] [int] NOT NULL,
	[RaceID] [int] NOT NULL,
	[ClassID] [int] NOT NULL,
	[FactionID] [bit] NOT NULL,
	[GenderID] [bit] NOT NULL,
	[SeasonWins] [int] NOT NULL,
	[SeasonLosses] [int] NOT NULL,
	[WeeklyWins] [int] NOT NULL,
	[WeeklyLosses] [int] NOT NULL,
	[ModifiedDate] [datetime2](7) NOT NULL,
	[ModifiedStatus] [nchar](10) NOT NULL,
	[ModifiedUserID] [bigint] NOT NULL,
 CONSTRAINT [PK_Character] PRIMARY KEY CLUSTERED 
(
	[CharacterID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

ALTER TABLE [dbo].[Character]  WITH CHECK ADD FOREIGN KEY([ClassID])
REFERENCES [dbo].[Class] ([ClassID])
GO

ALTER TABLE [dbo].[Character]  WITH CHECK ADD FOREIGN KEY([FactionID])
REFERENCES [dbo].[Faction] ([FactionID])
GO

ALTER TABLE [dbo].[Character]  WITH CHECK ADD FOREIGN KEY([GenderID])
REFERENCES [dbo].[Gender] ([GenderID])
GO

ALTER TABLE [dbo].[Character]  WITH CHECK ADD FOREIGN KEY([RaceID])
REFERENCES [dbo].[Race] ([RaceID])
GO

ALTER TABLE [dbo].[Character]  WITH CHECK ADD  CONSTRAINT [FK__Character__Realm__2D27B809] FOREIGN KEY([RealmID])
REFERENCES [dbo].[Realm] ([RealmID])
GO

ALTER TABLE [dbo].[Character] CHECK CONSTRAINT [FK__Character__Realm__2D27B809]
GO

ALTER TABLE [dbo].[Character]  WITH CHECK ADD FOREIGN KEY([SpecID])
REFERENCES [dbo].[Spec] ([SpecID])
GO

USE [fotm]
GO

/****** Object:  Table [dbo].[PvpStats]    Script Date: 2/16/2016 5:34:53 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[PvpStats](
	[PvpStatsID] [bigint] IDENTITY(1,1) NOT NULL,
	[Ranking] [int] NOT NULL,
	[Rating] [int] NOT NULL,
	[CharacterID] [bigint] NOT NULL,
	[ModifiedDate] [datetime2](7) NOT NULL,
	[ModifiedStatus] [nchar](10) NOT NULL,
	[ModifiedUserID] [bigint] NOT NULL,
 CONSTRAINT [PK_PvpStats] PRIMARY KEY CLUSTERED 
(
	[PvpStatsID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

ALTER TABLE [dbo].[PvpStats]  WITH CHECK ADD FOREIGN KEY([CharacterID])
REFERENCES [dbo].[Character] ([CharacterID])
GO

USE [fotm]
GO

/****** Object:  Table [dbo].[TeamMember]    Script Date: 2/16/2016 5:35:09 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[TeamMember](
	[TeamMemberID] [bigint] IDENTITY(1,1) NOT NULL,
	[TeamID] [bigint] NOT NULL,
	[RatingChangeValue] [int] NOT NULL,
	[CurrentRating] [int] NOT NULL,
	[CharacterID] [bigint] NOT NULL,
	[SpecID] [int] NOT NULL,
	[RaceID] [int] NOT NULL,
	[FactionID] [bit] NOT NULL,
	[GenderID] [bit] NOT NULL,
	[ModifiedDate] [datetime2](7) NOT NULL,
	[ModifiedStatus] [nchar](10) NOT NULL,
	[ModifiedUserID] [bigint] NOT NULL,
 CONSTRAINT [PK_TeamMember] PRIMARY KEY CLUSTERED 
(
	[TeamMemberID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

ALTER TABLE [dbo].[TeamMember]  WITH CHECK ADD  CONSTRAINT [FK__TeamMembe__Chara__3B75D760] FOREIGN KEY([CharacterID])
REFERENCES [dbo].[Character] ([CharacterID])
GO

ALTER TABLE [dbo].[TeamMember] CHECK CONSTRAINT [FK__TeamMembe__Chara__3B75D760]
GO

ALTER TABLE [dbo].[TeamMember]  WITH CHECK ADD FOREIGN KEY([FactionID])
REFERENCES [dbo].[Faction] ([FactionID])
GO

ALTER TABLE [dbo].[TeamMember]  WITH CHECK ADD FOREIGN KEY([GenderID])
REFERENCES [dbo].[Gender] ([GenderID])
GO

ALTER TABLE [dbo].[TeamMember]  WITH CHECK ADD  CONSTRAINT [FK__TeamMembe__RaceI__3D5E1FD2] FOREIGN KEY([RaceID])
REFERENCES [dbo].[Race] ([RaceID])
GO

ALTER TABLE [dbo].[TeamMember] CHECK CONSTRAINT [FK__TeamMembe__RaceI__3D5E1FD2]
GO

ALTER TABLE [dbo].[TeamMember]  WITH CHECK ADD  CONSTRAINT [FK__TeamMembe__SpecI__3C69FB99] FOREIGN KEY([SpecID])
REFERENCES [dbo].[Spec] ([SpecID])
GO

ALTER TABLE [dbo].[TeamMember] CHECK CONSTRAINT [FK__TeamMembe__SpecI__3C69FB99]
GO

ALTER TABLE [dbo].[TeamMember]  WITH CHECK ADD  CONSTRAINT [FK__TeamMembe__TeamI__3A81B327] FOREIGN KEY([TeamID])
REFERENCES [dbo].[Team] ([TeamID])
GO

ALTER TABLE [dbo].[TeamMember] CHECK CONSTRAINT [FK__TeamMembe__TeamI__3A81B327]
GO

use fotm

insert into Faction (FactionID, Name, ModifiedDate, ModifiedStatus, ModifiedUserID)
	values (0, 'Alliance', SYSDATETIME(), 'I', 0)

insert into Faction (FactionID, Name, ModifiedDate, ModifiedStatus, ModifiedUserID)
	values (1, 'Horde', SYSDATETIME(), 'I', 0)