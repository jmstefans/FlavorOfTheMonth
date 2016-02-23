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

PRINT 'Fotm Database created.'


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

PRINT 'Faction table created.'

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

PRINT 'Gender table created.'

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

PRINT 'Race table created.'


USE [fotm]
GO

/****** Object:  Table [dbo].[Spec]    Script Date: 2/21/2016 6:55:43 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Spec](
	[SpecID] [int] IDENTITY(1,1) NOT NULL,
	[SpecName] [nvarchar](50) NOT NULL,
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


PRINT 'Spec table created.'

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

PRINT 'Team table created.'


USE [fotm]
GO

/****** Object:  Table [dbo].[Region]    Script Date: 2/17/2016 7:46:23 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Region](
	[RegionID] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
	[ModifiedDate] [datetime2](7) NOT NULL,
	[ModifiedStatus] [nchar](10) NOT NULL,
	[ModifiedUserID] [bigint] NOT NULL,
 CONSTRAINT [PK_Region] PRIMARY KEY CLUSTERED 
(
	[RegionID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

PRINT 'Region table created.'

USE [fotm]
GO

/****** Object:  Table [dbo].[Realm]    Script Date: 2/19/2016 2:43:24 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Realm](
	[RealmID] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
	[RegionID] [int] NOT NULL,
	[ModifiedDate] [datetime2](7) NOT NULL,
	[ModifiedStatus] [nchar](10) NOT NULL,
	[ModifiedUserID] [bigint] NOT NULL,
 CONSTRAINT [PK_Realm] PRIMARY KEY CLUSTERED 
(
	[RealmID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

ALTER TABLE [dbo].[Realm]  WITH CHECK ADD FOREIGN KEY([RegionID])
REFERENCES [dbo].[Region] ([RegionID])
GO

PRINT 'Realm table created.'


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

PRINT 'Character table created.'

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

PRINT 'PvpStats table created.'

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

PRINT 'TeamMember table created.'


USE [fotm]
GO

/****** Object:  Table [dbo].[UserProfile]    Script Date: 2/16/2016 8:12:53 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[UserProfile](
	[UserId] [int] IDENTITY(1,1) NOT NULL,
	[UserName] [nvarchar](56) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY],
UNIQUE NONCLUSTERED 
(
	[UserName] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

PRINT 'UserProfile table created.'


USE [fotm]
GO

/****** Object:  Table [dbo].[webpages_Membership]    Script Date: 2/16/2016 8:13:02 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[webpages_Membership](
	[UserId] [int] NOT NULL,
	[CreateDate] [datetime] NULL,
	[ConfirmationToken] [nvarchar](128) NULL,
	[IsConfirmed] [bit] NULL,
	[LastPasswordFailureDate] [datetime] NULL,
	[PasswordFailuresSinceLastSuccess] [int] NOT NULL,
	[Password] [nvarchar](128) NOT NULL,
	[PasswordChangedDate] [datetime] NULL,
	[PasswordSalt] [nvarchar](128) NOT NULL,
	[PasswordVerificationToken] [nvarchar](128) NULL,
	[PasswordVerificationTokenExpirationDate] [datetime] NULL,
PRIMARY KEY CLUSTERED 
(
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

ALTER TABLE [dbo].[webpages_Membership] ADD  DEFAULT ((0)) FOR [IsConfirmed]
GO

ALTER TABLE [dbo].[webpages_Membership] ADD  DEFAULT ((0)) FOR [PasswordFailuresSinceLastSuccess]
GO

PRINT 'webpages_Membership table created.'


USE [fotm]
GO

/****** Object:  Table [dbo].[webpages_OAuthMembership]    Script Date: 2/16/2016 8:13:13 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[webpages_OAuthMembership](
	[Provider] [nvarchar](30) NOT NULL,
	[ProviderUserId] [nvarchar](100) NOT NULL,
	[UserId] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Provider] ASC,
	[ProviderUserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

PRINT 'webpages_OAuthMembership table created.'


USE [fotm]
GO

/****** Object:  Table [dbo].[webpages_Roles]    Script Date: 2/16/2016 8:13:30 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[webpages_Roles](
	[RoleId] [int] IDENTITY(1,1) NOT NULL,
	[RoleName] [nvarchar](256) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[RoleId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY],
UNIQUE NONCLUSTERED 
(
	[RoleName] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

PRINT 'webpages_Roles table created.'


USE [fotm]
GO

/****** Object:  Table [dbo].[webpages_UsersInRoles]    Script Date: 2/16/2016 8:13:38 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[webpages_UsersInRoles](
	[UserId] [int] NOT NULL,
	[RoleId] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[UserId] ASC,
	[RoleId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

ALTER TABLE [dbo].[webpages_UsersInRoles]  WITH CHECK ADD  CONSTRAINT [fk_RoleId] FOREIGN KEY([RoleId])
REFERENCES [dbo].[webpages_Roles] ([RoleId])
GO

ALTER TABLE [dbo].[webpages_UsersInRoles] CHECK CONSTRAINT [fk_RoleId]
GO

ALTER TABLE [dbo].[webpages_UsersInRoles]  WITH CHECK ADD  CONSTRAINT [fk_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[UserProfile] ([UserId])
GO

ALTER TABLE [dbo].[webpages_UsersInRoles] CHECK CONSTRAINT [fk_UserId]
GO

PRINT 'webpages_UsersInRoles table created.'


PRINT 'Inserting Factions'

insert into Faction (FactionID, Name, ModifiedDate, ModifiedStatus, ModifiedUserID)
	values (0, 'Alliance', SYSDATETIME(), 'I', 0)

insert into Faction (FactionID, Name, ModifiedDate, ModifiedStatus, ModifiedUserID)
	values (1, 'Horde', SYSDATETIME(), 'I', 0)


PRINT 'Inserting Classes'

SET IDENTITY_INSERT Class ON

insert into Class (ClassID, Name, ModifiedDate, ModifiedStatus, ModifiedUserID)
	values (1, 'Warrior', SYSDATETIME(), 'I', 0)

insert into Class (ClassID, Name, ModifiedDate, ModifiedStatus, ModifiedUserID)
	values (2, 'Paladin', SYSDATETIME(), 'I', 0)

insert into Class (ClassID, Name, ModifiedDate, ModifiedStatus, ModifiedUserID)
	values (3, 'Hunter', SYSDATETIME(), 'I', 0)

insert into Class (ClassID, Name, ModifiedDate, ModifiedStatus, ModifiedUserID)
	values (4, 'Rogue', SYSDATETIME(), 'I', 0)

insert into Class (ClassID, Name, ModifiedDate, ModifiedStatus, ModifiedUserID)
	values (5, 'Priest', SYSDATETIME(), 'I', 0)

insert into Class (ClassID, Name, ModifiedDate, ModifiedStatus, ModifiedUserID)
	values (6, 'Death Knight', SYSDATETIME(), 'I', 0)

insert into Class (ClassID, Name, ModifiedDate, ModifiedStatus, ModifiedUserID)
	values (7, 'Shaman', SYSDATETIME(), 'I', 0)

insert into Class (ClassID, Name, ModifiedDate, ModifiedStatus, ModifiedUserID)
	values (8, 'Mage', SYSDATETIME(), 'I', 0)

insert into Class (ClassID, Name, ModifiedDate, ModifiedStatus, ModifiedUserID)
	values (9, 'Warlock', SYSDATETIME(), 'I', 0)

insert into Class (ClassID, Name, ModifiedDate, ModifiedStatus, ModifiedUserID)
	values (10, 'Monk', SYSDATETIME(), 'I', 0)

insert into Class (ClassID, Name, ModifiedDate, ModifiedStatus, ModifiedUserID)
	values (11, 'Druid', SYSDATETIME(), 'I', 0)

SET IDENTITY_INSERT Class OFF


PRINT 'Inserting Genders'

insert into Gender (GenderID, Name, ModifiedDate, ModifiedStatus, ModifiedUserID)
	values (0, 'Male', SYSDATETIME(), 'I', 0)

insert into Gender (GenderID, Name, ModifiedDate, ModifiedStatus, ModifiedUserID)
	values (1, 'Female', SYSDATETIME(), 'I', 0)


PRINT 'Inserting Races'

SET IDENTITY_INSERT Race ON

insert into Race (RaceID, Name, ModifiedDate, ModifiedStatus, ModifiedUserID)
	values (1, 'Human', SYSDATETIME(), 'I', 0)

insert into Race (RaceID, Name, ModifiedDate, ModifiedStatus, ModifiedUserID)
	values (2, 'Orc', SYSDATETIME(), 'I', 0)

insert into Race (RaceID, Name, ModifiedDate, ModifiedStatus, ModifiedUserID)
	values (3, 'Dwarf', SYSDATETIME(), 'I', 0)

insert into Race (RaceID, Name, ModifiedDate, ModifiedStatus, ModifiedUserID)
	values (4, 'Night Elf', SYSDATETIME(), 'I', 0)

insert into Race (RaceID, Name, ModifiedDate, ModifiedStatus, ModifiedUserID)
	values (5, 'Undead', SYSDATETIME(), 'I', 0)

insert into Race (RaceID, Name, ModifiedDate, ModifiedStatus, ModifiedUserID)
	values (6, 'Tauren', SYSDATETIME(), 'I', 0)

insert into Race (RaceID, Name, ModifiedDate, ModifiedStatus, ModifiedUserID)
	values (7, 'Gnome', SYSDATETIME(), 'I', 0)

insert into Race (RaceID, Name, ModifiedDate, ModifiedStatus, ModifiedUserID)
	values (8, 'Troll', SYSDATETIME(), 'I', 0)

insert into Race (RaceID, Name, ModifiedDate, ModifiedStatus, ModifiedUserID)
	values (9, 'Goblin', SYSDATETIME(), 'I', 0)

insert into Race (RaceID, Name, ModifiedDate, ModifiedStatus, ModifiedUserID)
	values (10, 'Blood Elf', SYSDATETIME(), 'I', 0)

insert into Race (RaceID, Name, ModifiedDate, ModifiedStatus, ModifiedUserID)
	values (11, 'Draenei', SYSDATETIME(), 'I', 0)

insert into Race (RaceID, Name, ModifiedDate, ModifiedStatus, ModifiedUserID)
	values (22, 'Worgen', SYSDATETIME(), 'I', 0)

insert into Race (RaceID, Name, ModifiedDate, ModifiedStatus, ModifiedUserID)
	values (24, 'Pandaren', SYSDATETIME(), 'I', 0)

insert into Race (RaceID, Name, ModifiedDate, ModifiedStatus, ModifiedUserID)
	values (25, 'Pandaren', SYSDATETIME(), 'I', 0)

insert into Race (RaceID, Name, ModifiedDate, ModifiedStatus, ModifiedUserID)
	values (26, 'Pandaren', SYSDATETIME(), 'I', 0)

SET IDENTITY_INSERT Race OFF


PRINT 'Inserting Regions'

SET IDENTITY_INSERT Region ON

insert into Region (RegionID, Name, ModifiedDate, ModifiedStatus, ModifiedUserID)
	values (0, 'US', SYSDATETIME(), 'I', 0)

insert into Region (RegionID, Name, ModifiedDate, ModifiedStatus, ModifiedUserID)
	values (1, 'EU', SYSDATETIME(), 'I', 0)

insert into Region (RegionID, Name, ModifiedDate, ModifiedStatus, ModifiedUserID)
	values (2, 'KR', SYSDATETIME(), 'I', 0)

insert into Region (RegionID, Name, ModifiedDate, ModifiedStatus, ModifiedUserID)
	values (3, 'TW', SYSDATETIME(), 'I', 0)

insert into Region (RegionID, Name, ModifiedDate, ModifiedStatus, ModifiedUserID)
	values (4, 'CN', SYSDATETIME(), 'I', 0)

insert into Region (RegionID, Name, ModifiedDate, ModifiedStatus, ModifiedUserID)
	values (5, 'SEA', SYSDATETIME(), 'I', 0)

SET IDENTITY_INSERT Region OFF


PRINT 'Inserting Specs'

SET IDENTITY_INSERT Spec ON

insert into Spec (SpecID, Name, BlizzName, ModifiedDate, ModifiedStatus, ModifiedUserID)
	values (62, 'Arcane', 'MAGE_ARCANE', SYSDATETIME(), 'I', 0)

insert into Spec (SpecID, Name, BlizzName, ModifiedDate, ModifiedStatus, ModifiedUserID)
	values (63, 'Fire', 'MAGE_FIRE', SYSDATETIME(), 'I', 0)

insert into Spec (SpecID, Name, BlizzName, ModifiedDate, ModifiedStatus, ModifiedUserID)
	values (64, 'Frost', 'MAKE_FROST', SYSDATETIME(), 'I', 0)

insert into Spec (SpecID, Name, BlizzName, ModifiedDate, ModifiedStatus, ModifiedUserID)
	values (65, 'Holy', 'PALADIN_HOLY', SYSDATETIME(), 'I', 0)

insert into Spec (SpecID, Name, BlizzName, ModifiedDate, ModifiedStatus, ModifiedUserID)
	values (66, 'Protection', 'PALADIN_PROTECTION', SYSDATETIME(), 'I', 0)

insert into Spec (SpecID, Name, BlizzName, ModifiedDate, ModifiedStatus, ModifiedUserID)
	values (70, 'Retribution', 'PALADIN_RETRIBUTION', SYSDATETIME(), 'I', 0)

insert into Spec (SpecID, Name, BlizzName, ModifiedDate, ModifiedStatus, ModifiedUserID)
	values (71, 'Arms', 'WARRIOR_ARMS', SYSDATETIME(), 'I', 0)

insert into Spec (SpecID, Name, BlizzName, ModifiedDate, ModifiedStatus, ModifiedUserID)
	values (72, 'Fury', 'WARRIOR_FURY', SYSDATETIME(), 'I', 0)

insert into Spec (SpecID, Name, BlizzName, ModifiedDate, ModifiedStatus, ModifiedUserID)
	values (73, 'Protection', 'WARRIOR_PROTECTION', SYSDATETIME(), 'I', 0)

insert into Spec (SpecID, Name, BlizzName, ModifiedDate, ModifiedStatus, ModifiedUserID)
	values (102, 'Balance', 'DRUID_BALANCE', SYSDATETIME(), 'I', 0)

insert into Spec (SpecID, Name, BlizzName, ModifiedDate, ModifiedStatus, ModifiedUserID)
	values (103, 'Feral', 'DRUID_FERAL', SYSDATETIME(), 'I', 0)

insert into Spec (SpecID, Name, BlizzName, ModifiedDate, ModifiedStatus, ModifiedUserID)
	values (104, 'Guardian', 'DRUID_GUARDIAN', SYSDATETIME(), 'I', 0)

insert into Spec (SpecID, Name, BlizzName, ModifiedDate, ModifiedStatus, ModifiedUserID)
	values (105, 'Restoration', 'DRUID_RESTOR', SYSDATETIME(), 'I', 0)

insert into Spec (SpecID, Name, BlizzName, ModifiedDate, ModifiedStatus, ModifiedUserID)
	values (250, 'Blood', 'DK_BLOOD', SYSDATETIME(), 'I', 0)

insert into Spec (SpecID, Name, BlizzName, ModifiedDate, ModifiedStatus, ModifiedUserID)
	values (251, 'Frost', 'DK_FROST', SYSDATETIME(), 'I', 0)

insert into Spec (SpecID, Name, BlizzName, ModifiedDate, ModifiedStatus, ModifiedUserID)
	values (252, 'Unholy', 'DK_UNHOLY', SYSDATETIME(), 'I', 0)

insert into Spec (SpecID, Name, BlizzName, ModifiedDate, ModifiedStatus, ModifiedUserID)
	values (253, 'Beast Mastery', 'HUNTER_BEASTMASTER', SYSDATETIME(), 'I', 0)

insert into Spec (SpecID, Name, BlizzName, ModifiedDate, ModifiedStatus, ModifiedUserID)
	values (254, 'Marksmanship', 'HUNTER_MARKSMAN', SYSDATETIME(), 'I', 0)

insert into Spec (SpecID, Name, BlizzName, ModifiedDate, ModifiedStatus, ModifiedUserID)
	values (255, 'Survival', 'HUNTER_SURVIVAL', SYSDATETIME(), 'I', 0)

insert into Spec (SpecID, Name, BlizzName, ModifiedDate, ModifiedStatus, ModifiedUserID)
	values (256, 'Discipline', 'PRIEST_DISCIPLINE', SYSDATETIME(), 'I', 0)

insert into Spec (SpecID, Name, BlizzName, ModifiedDate, ModifiedStatus, ModifiedUserID)
	values (257, 'Holy', 'PRIEST_HOLY', SYSDATETIME(), 'I', 0)

insert into Spec (SpecID, Name, BlizzName, ModifiedDate, ModifiedStatus, ModifiedUserID)
	values (258, 'Shadow', 'PRIEST_SHADOW', SYSDATETIME(), 'I', 0)

insert into Spec (SpecID, Name, BlizzName, ModifiedDate, ModifiedStatus, ModifiedUserID)
	values (259, 'Assassination', 'ROGUE_ASSASSINATION', SYSDATETIME(), 'I', 0)

insert into Spec (SpecID, Name, BlizzName, ModifiedDate, ModifiedStatus, ModifiedUserID)
	values (260, 'Combat', 'ROGUE_COMBAT', SYSDATETIME(), 'I', 0)

insert into Spec (SpecID, Name, BlizzName, ModifiedDate, ModifiedStatus, ModifiedUserID)
	values (261, 'Subtlety', 'ROGUE_SUBTLETY', SYSDATETIME(), 'I', 0)

insert into Spec (SpecID, Name, BlizzName, ModifiedDate, ModifiedStatus, ModifiedUserID)
	values (262, 'Elemental', 'SHAMAN_ELEMENTAL', SYSDATETIME(), 'I', 0)

insert into Spec (SpecID, Name, BlizzName, ModifiedDate, ModifiedStatus, ModifiedUserID)
	values (263, 'Enhancement', 'SHAMAN_ENHANCEMENT', SYSDATETIME(), 'I', 0)

insert into Spec (SpecID, Name, BlizzName, ModifiedDate, ModifiedStatus, ModifiedUserID)
	values (264, 'Restoration', 'SHAMAN_RESTORATION', SYSDATETIME(), 'I', 0)

insert into Spec (SpecID, Name, BlizzName, ModifiedDate, ModifiedStatus, ModifiedUserID)
	values (265, 'Affliction', 'WARLOCK_AFFLICTION', SYSDATETIME(), 'I', 0)

insert into Spec (SpecID, Name, BlizzName, ModifiedDate, ModifiedStatus, ModifiedUserID)
	values (266, 'Demonology', 'WARLOCK_DEMONOLOGY', SYSDATETIME(), 'I', 0)

insert into Spec (SpecID, Name, BlizzName, ModifiedDate, ModifiedStatus, ModifiedUserID)
	values (267, 'Destruction', 'WARLOCK_DESTRUCTION', SYSDATETIME(), 'I', 0)

insert into Spec (SpecID, Name, BlizzName, ModifiedDate, ModifiedStatus, ModifiedUserID)
	values (268, 'Brewmaster', 'MONK_BREWMASTER', SYSDATETIME(), 'I', 0)

insert into Spec (SpecID, Name, BlizzName, ModifiedDate, ModifiedStatus, ModifiedUserID)
	values (269, 'Windwalker', 'MONK_WINDDANCER', SYSDATETIME(), 'I', 0)

insert into Spec (SpecID, Name, BlizzName, ModifiedDate, ModifiedStatus, ModifiedUserID)
	values (270, 'Mistweaver', 'MONK_MISTWEAVER', SYSDATETIME(), 'I', 0)

SET IDENTITY_INSERT Spec OFF


PRINT 'Inserting UserProfiles'

SET IDENTITY_INSERT UserProfile ON

insert into UserProfile (UserId, UserName)
	values (0, 'john')

insert into UserProfile (UserId, UserName)
	values (1, 'andrew')

SET IDENTITY_INSERT UserProfile OFF


PRINT 'Inserting webpages_Roles'

SET IDENTITY_INSERT webpages_Roles ON

insert into webpages_Roles (RoleId, RoleName)
	values (0, 'Admin')

insert into webpages_Roles (RoleId, RoleName)
	values (1, 'Member')

insert into webpages_Roles (RoleId, RoleName)
	values (2, 'Registered')

insert into webpages_Roles (RoleId, RoleName)
	values (3, 'Unregistered')

SET IDENTITY_INSERT webpages_Roles OFF


PRINT 'Inserting webpages_UsersInRoles'

insert into webpages_UsersInRoles (UserId, RoleId)
	values (0, 0)

insert into webpages_UsersInRoles (UserId, RoleId)
	values (1, 0)


PRINT 'Creating Stored Procedures'

USE [fotm]
GO

/****** Object:  StoredProcedure [dbo].[SP_GetAllTeamsByClassCompositionThenOrderThemByMostPopular]    Script Date: 2/21/2016 8:10:24 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		<John Stefanski>
-- Create date: <2/19/2016>
-- Description:	<Gather all of the teams for the
-- past month and group them by class composition.
-- Then return that list sorted by most popular.>
-- =============================================
CREATE PROCEDURE [dbo].[SP_GetAllTeamsByClassCompositionThenOrderThemByMostPopular] 
	-- Add the parameters for the stored procedure here

AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	SELECT t.TeamID, cl.Name, s.SpecName
		FROM fotm.dbo.Team t
		LEFT OUTER JOIN fotm.dbo.TeamMember tm on t.TeamID = tm.TeamID
		LEFT OUTER JOIN fotm.dbo.[Character] c on tm.CharacterID = c.CharacterID
		LEFT OUTER JOIN fotm.dbo.Class cl on c.ClassID = cl.ClassID
		LEFT OUTER JOIN fotm.dbo.Spec s on c.SpecID = s.SpecID
		WHERE tm.ModifiedDate >= DATEADD(DAY, -30, SYSDATETIME())
		ORDER BY t.TeamID, cl.Name, s.SpecName
END

GO

