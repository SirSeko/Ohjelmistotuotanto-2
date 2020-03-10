USE [mydb]
GO
/****** Object:  Table [mydb].[Tilaus]    Script Date: 10/03/2020 18.21.04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [mydb].[Tilaus](
	[TilausID] [int] NOT NULL,
	[TilaajanNimi] [nchar](50) NOT NULL,
	[TilaajanOsoite] [nchar](50) NULL,
 CONSTRAINT [PK_Tilaus] PRIMARY KEY CLUSTERED 
(
	[TilausID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
