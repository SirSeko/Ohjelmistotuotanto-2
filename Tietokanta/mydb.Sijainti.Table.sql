USE [mydb]
GO
/****** Object:  Table [mydb].[Sijainti]    Script Date: 09/03/2020 15.50.28 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [mydb].[Sijainti](
	[SijaintiID] [int] NOT NULL,
	[SijaintiNimi] [nvarchar](50) NOT NULL,
	[Kaappi] [nvarchar](50) NULL,
	[Hylly] [nvarchar](50) NULL,
	[Lisätiedot] [nvarchar](100) NULL,
 CONSTRAINT [PK_Sijainti] PRIMARY KEY CLUSTERED 
(
	[SijaintiID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
