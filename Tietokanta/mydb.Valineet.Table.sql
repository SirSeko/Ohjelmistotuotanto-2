USE [mydb]
GO
/****** Object:  Table [mydb].[Valineet]    Script Date: 23/03/2020 16.54.57 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [mydb].[Valineet](
	[ValineetID] [int] IDENTITY(3000,1) NOT NULL,
	[Nimi] [nvarchar](50) NULL,
	[Määrä] [int] NULL,
	[Hinta] [decimal](10, 0) NULL,
	[Yksikkö] [nvarchar](10) NULL,
	[Sijainti] [int] NULL,
	[Kauppa] [nvarchar](50) NULL,
	[Lisätiedot] [nvarchar](100) NULL,
 CONSTRAINT [PK_Valineet] PRIMARY KEY CLUSTERED 
(
	[ValineetID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [mydb].[Valineet]  WITH CHECK ADD  CONSTRAINT [FK_Valineet_Sijainti] FOREIGN KEY([Sijainti])
REFERENCES [mydb].[Sijainti] ([SijaintiID])
GO
ALTER TABLE [mydb].[Valineet] CHECK CONSTRAINT [FK_Valineet_Sijainti]
GO
