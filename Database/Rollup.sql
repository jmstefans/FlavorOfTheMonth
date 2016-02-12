USE [master]
GO

/****** Object:  Database [fotm]    Script Date: 2/11/2016 11:35:35 PM ******/
CREATE DATABASE [fotm]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'fotm', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL12.SQLEXPRESS\MSSQL\DATA\fotm.mdf' , SIZE = 4096KB , MAXSIZE = UNLIMITED, FILEGROWTH = 1024KB )
 LOG ON 
( NAME = N'fotm_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL12.SQLEXPRESS\MSSQL\DATA\fotm_log.ldf' , SIZE = 6912KB , MAXSIZE = 2048GB , FILEGROWTH = 10%)
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

ALTER DATABASE [fotm] SET DELAYED_DURABILITY = DISABLED 
GO

ALTER DATABASE [fotm] SET  READ_WRITE 
GO


/******************************* Character Table ****************************/

USE [fotm]
GO

/****** Object:  Table [dbo].[Character]    Script Date: 2/11/2016 11:36:56 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[Character](
	[CharacterID] [bigint] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](100) NOT NULL,
	[Server] [nvarchar](100) NOT NULL,
	[ModifiedDate] [datetime] NOT NULL,
	[ModifiedStatus] [char](1) NOT NULL,
	[ModifiedUserID] [bigint] NOT NULL,
 CONSTRAINT [PK_Character] PRIMARY KEY CLUSTERED 
(
	[CharacterID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO


/******************************* PvpStats Table ****************************/

USE [fotm]
GO

/****** Object:  Table [dbo].[PvpStats]    Script Date: 2/11/2016 11:37:29 PM ******/
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


/******************************* RatingChange Table ****************************/

USE [fotm]
GO

/****** Object:  Table [dbo].[RatingChange]    Script Date: 2/11/2016 11:37:34 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[RatingChange](
	[RatingChangeID] [bigint] IDENTITY(1,1) NOT NULL,
	[CharacterID] [bigint] NOT NULL,
	[RatingChange] [int] NOT NULL,
	[ModifiedDate] [datetime] NOT NULL,
	[ModifiedStatus] [char](1) NOT NULL,
	[ModifiedUserID] [bigint] NOT NULL,
 CONSTRAINT [PK_RatingChange] PRIMARY KEY CLUSTERED 
(
	[RatingChangeID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO


/******************************* UserProfile Table ****************************/

USE [fotm]
GO

/****** Object:  Table [dbo].[UserProfile]    Script Date: 2/11/2016 11:37:38 PM ******/
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


/******************************* webpages_Membership Table ****************************/

USE [fotm]
GO

/****** Object:  Table [dbo].[webpages_Membership]    Script Date: 2/11/2016 11:41:26 PM ******/
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


/******************************* webpages_OAuthMembership Table ****************************/

USE [fotm]
GO

/****** Object:  Table [dbo].[webpages_OAuthMembership]    Script Date: 2/11/2016 11:37:45 PM ******/
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


/******************************* webpages_Roles Table ****************************/

USE [fotm]
GO

/****** Object:  Table [dbo].[webpages_Roles]    Script Date: 2/11/2016 11:37:48 PM ******/
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


/******************************* webpages_UsersInRoles Table ****************************/

USE [fotm]
GO

/****** Object:  Table [dbo].[webpages_UsersInRoles]    Script Date: 2/11/2016 11:37:53 PM ******/
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