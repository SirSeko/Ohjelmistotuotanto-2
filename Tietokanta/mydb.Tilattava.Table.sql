USE [mydb]
GO
/****** Object:  Table [mydb].[Tilattava]    Script Date: 23/03/2020 16.54.57 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [mydb].[Tilattava](
	[TilausID] [int] NOT NULL,
	[MateriaaliID] [int] NULL
) ON [PRIMARY]
GO
ALTER TABLE [mydb].[Tilattava]  WITH CHECK ADD  CONSTRAINT [FK_Tilattava_Materiaali] FOREIGN KEY([MateriaaliID])
REFERENCES [mydb].[Materiaali] ([MateriaaliID])
GO
ALTER TABLE [mydb].[Tilattava] CHECK CONSTRAINT [FK_Tilattava_Materiaali]
GO
ALTER TABLE [mydb].[Tilattava]  WITH CHECK ADD  CONSTRAINT [FK_Tilattava_Tilaus] FOREIGN KEY([TilausID])
REFERENCES [mydb].[Tilaus] ([TilausID])
GO
ALTER TABLE [mydb].[Tilattava] CHECK CONSTRAINT [FK_Tilattava_Tilaus]
GO
