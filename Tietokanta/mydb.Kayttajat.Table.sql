USE [mydb]
GO
/****** Object:  Table [mydb].[Kayttajat]    Script Date: 10/03/2020 18.21.03 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [mydb].[Kayttajat](
	[Kayttajanimi] [varchar](50) NULL,
	[Salasana] [varchar](100) NULL
) ON [PRIMARY]
GO
