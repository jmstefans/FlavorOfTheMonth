USE [fotm]
GO

/****** Object:  Table [dbo].[Character]    Script Date: 2/1/2016 4:28:32 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[Character](
	[CharacterID] [bigint] NOT NULL,
	[Name] [nvarchar](100) NOT NULL,
	[Server] [nvarchar](100) NOT NULL,
	[ModifiedDate] [datetime] NOT NULL,
	[ModifiedStatus] [char](1) NOT NULL,
	[ModifiedUserID] [bigint] NOT NULL
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO

