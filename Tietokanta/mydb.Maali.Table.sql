USE [mydb]
GO
/****** Object:  Table [mydb].[Maali]    Script Date: 09/03/2020 15.50.28 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [mydb].[Maali](
	[MaaliID] [int] NOT NULL,
	[Koko] [nvarchar](45) NULL,
	[Hinta] [decimal](10, 0) NULL,
	[Määrä] [int] NULL,
	[Yksikkö] [nvarchar](45) NULL,
	[Sijainti] [int] NULL,
	[Kauppa] [nvarchar](50) NULL,
	[Lisätiedot] [nvarchar](100) NULL,
 CONSTRAINT [PK_Maali] PRIMARY KEY CLUSTERED 
(
	[MaaliID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [mydb].[Maali]  WITH CHECK ADD  CONSTRAINT [FK_Maali_Sijainti] FOREIGN KEY([Sijainti])
REFERENCES [mydb].[Sijainti] ([SijaintiID])
GO
ALTER TABLE [mydb].[Maali] CHECK CONSTRAINT [FK_Maali_Sijainti]
GO
