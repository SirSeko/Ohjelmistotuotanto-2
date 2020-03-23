USE [mydb]
GO
/****** Object:  Table [mydb].[Kayttajat]    Script Date: 23/03/2020 16.54.57 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [mydb].[Kayttajat](
	[Kayttajanimi] [varchar](50) NULL,
	[Salasana] [varchar](100) NULL,
	[Valtuudet] [int] NULL
) ON [PRIMARY]
GO
