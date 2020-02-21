USE [mydb]
GO
/****** Object:  Table [mydb].[Tilaus]    Script Date: 21/02/2020 19.05.54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [mydb].[Tilaus](
	[TilausID] [int] IDENTITY(1,1) NOT NULL,
	[TilaajanNimi] [nvarchar](45) NULL,
	[Määrä] [int] NULL,
	[MateriaaliID] [int] NOT NULL,
	[LautaID] [int] NULL,
	[VaneriID] [int] NULL,
	[MaaliID] [int] NULL,
 CONSTRAINT [PK_Tilaus_TilausID] PRIMARY KEY CLUSTERED 
(
	[TilausID] ASC,
	[MateriaaliID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY],
 CONSTRAINT [Tilaus$TilausID_UNIQUE] UNIQUE NONCLUSTERED 
(
	[TilausID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [mydb].[Tilaus] ADD  DEFAULT (NULL) FOR [TilaajanNimi]
GO
ALTER TABLE [mydb].[Tilaus] ADD  DEFAULT (NULL) FOR [Määrä]
GO
ALTER TABLE [mydb].[Tilaus]  WITH CHECK ADD  CONSTRAINT [FK_Tilaus_Lauta] FOREIGN KEY([LautaID])
REFERENCES [mydb].[Lauta] ([LautaID])
GO
ALTER TABLE [mydb].[Tilaus] CHECK CONSTRAINT [FK_Tilaus_Lauta]
GO
ALTER TABLE [mydb].[Tilaus]  WITH CHECK ADD  CONSTRAINT [FK_Tilaus_Maali] FOREIGN KEY([MaaliID])
REFERENCES [mydb].[Maali] ([MaaliID])
GO
ALTER TABLE [mydb].[Tilaus] CHECK CONSTRAINT [FK_Tilaus_Maali]
GO
ALTER TABLE [mydb].[Tilaus]  WITH CHECK ADD  CONSTRAINT [FK_Tilaus_Vaneri] FOREIGN KEY([VaneriID])
REFERENCES [mydb].[Vaneri] ([VaneriID])
GO
ALTER TABLE [mydb].[Tilaus] CHECK CONSTRAINT [FK_Tilaus_Vaneri]
GO
ALTER TABLE [mydb].[Tilaus]  WITH CHECK ADD  CONSTRAINT [Tilaus$fk_Tilaus_Materiaali] FOREIGN KEY([MateriaaliID])
REFERENCES [mydb].[Materiaali] ([MateriaaliID])
GO
ALTER TABLE [mydb].[Tilaus] CHECK CONSTRAINT [Tilaus$fk_Tilaus_Materiaali]
GO
EXEC sys.sp_addextendedproperty @name=N'MS_SSMA_SOURCE', @value=N'mydb.Tilaus' , @level0type=N'SCHEMA',@level0name=N'mydb', @level1type=N'TABLE',@level1name=N'Tilaus'
GO
