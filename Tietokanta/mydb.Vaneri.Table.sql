USE [mydb]
GO
/****** Object:  Table [mydb].[Vaneri]    Script Date: 10/03/2020 18.21.04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [mydb].[Vaneri](
	[VaneriID] [int] NOT NULL,
	[Koko] [nvarchar](45) NULL,
	[Hinta] [decimal](10, 0) NULL,
	[Määrä] [int] NULL,
	[Yksikkö] [nvarchar](10) NULL,
	[Sijainti] [int] NULL,
	[Kauppa] [nvarchar](50) NULL,
	[Lisätiedot] [nvarchar](100) NULL,
 CONSTRAINT [PK_Vaneri] PRIMARY KEY CLUSTERED 
(
	[VaneriID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [mydb].[Vaneri]  WITH CHECK ADD  CONSTRAINT [FK_Vaneri_Sijainti] FOREIGN KEY([Sijainti])
REFERENCES [mydb].[Sijainti] ([SijaintiID])
GO
ALTER TABLE [mydb].[Vaneri] CHECK CONSTRAINT [FK_Vaneri_Sijainti]
GO
