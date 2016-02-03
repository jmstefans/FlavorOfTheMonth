USE [fotm]
GO

/****** Object:  Table [dbo].[Character]    Script Date: 2/2/2016 4:34:49 PM ******/
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

