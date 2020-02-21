USE [mydb]
GO
/****** Object:  Table [mydb].[Materiaali]    Script Date: 21/02/2020 19.05.54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [mydb].[Materiaali](
	[MateriaaliID] [int] IDENTITY(1,1) NOT NULL,
	[Nimi] [nvarchar](45) NULL,
	[Koko] [nvarchar](45) NULL,
	[Hinta] [decimal](10, 0) NULL,
	[Määrä] [int] NULL,
	[Yksikkö] [nvarchar](10) NULL,
	[Sijainti] [int] NULL,
	[Kauppa] [nvarchar](50) NULL,
	[Lisätiedot] [nvarchar](100) NULL,
 CONSTRAINT [PK_Materiaali_MateriaaliID] PRIMARY KEY CLUSTERED 
(
	[MateriaaliID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY],
 CONSTRAINT [Materiaali$MateriaaliID_UNIQUE] UNIQUE NONCLUSTERED 
(
	[MateriaaliID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [mydb].[Materiaali] ADD  DEFAULT (NULL) FOR [Nimi]
GO
ALTER TABLE [mydb].[Materiaali] ADD  DEFAULT (NULL) FOR [Koko]
GO
ALTER TABLE [mydb].[Materiaali] ADD  DEFAULT (NULL) FOR [Hinta]
GO
ALTER TABLE [mydb].[Materiaali] ADD  DEFAULT (NULL) FOR [Määrä]
GO
ALTER TABLE [mydb].[Materiaali]  WITH CHECK ADD  CONSTRAINT [FK_Materiaali_Sijainti] FOREIGN KEY([Sijainti])
REFERENCES [mydb].[Sijainti] ([SijaintiID])
GO
ALTER TABLE [mydb].[Materiaali] CHECK CONSTRAINT [FK_Materiaali_Sijainti]
GO
EXEC sys.sp_addextendedproperty @name=N'MS_SSMA_SOURCE', @value=N'mydb.Materiaali' , @level0type=N'SCHEMA',@level0name=N'mydb', @level1type=N'TABLE',@level1name=N'Materiaali'
GO
