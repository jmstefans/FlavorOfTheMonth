USE [fotm]
GO

/****** Object:  Table [dbo].[RatingChange]    Script Date: 2/1/2016 4:29:05 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[RatingChange](
	[RatingChangeID] [bigint] NOT NULL,
	[CharacterID] [bigint] NOT NULL,
	[RatingChange] [int] NOT NULL,
	[ModifiedDate] [datetime] NOT NULL,
	[ModifiedStatus] [char](1) NOT NULL,
	[ModifiedUserID] [bigint] NOT NULL
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO

