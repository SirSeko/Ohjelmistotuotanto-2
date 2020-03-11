USE [mydb]
GO
/****** Object:  Table [mydb].[Materiaali]    Script Date: 10/03/2020 18.21.04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [mydb].[Materiaali](
	[MateriaaliID] [int] NOT NULL,
	[MaaliID] [int] NULL,
	[VaneriID] [int] NULL,
	[LautaID] [int] NULL,
	[ValineetID] [int] NULL,
 CONSTRAINT [PK_Materiaali] PRIMARY KEY CLUSTERED 
(
	[MateriaaliID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [mydb].[Materiaali]  WITH CHECK ADD  CONSTRAINT [FK_Materiaali_Lauta] FOREIGN KEY([LautaID])
REFERENCES [mydb].[Lauta] ([LautaID])
GO
ALTER TABLE [mydb].[Materiaali] CHECK CONSTRAINT [FK_Materiaali_Lauta]
GO
ALTER TABLE [mydb].[Materiaali]  WITH CHECK ADD  CONSTRAINT [FK_Materiaali_Maali] FOREIGN KEY([MaaliID])
REFERENCES [mydb].[Maali] ([MaaliID])
GO
ALTER TABLE [mydb].[Materiaali] CHECK CONSTRAINT [FK_Materiaali_Maali]
GO
ALTER TABLE [mydb].[Materiaali]  WITH CHECK ADD  CONSTRAINT [FK_Materiaali_Valineet] FOREIGN KEY([ValineetID])
REFERENCES [mydb].[Valineet] ([ValineetID])
GO
ALTER TABLE [mydb].[Materiaali] CHECK CONSTRAINT [FK_Materiaali_Valineet]
GO
ALTER TABLE [mydb].[Materiaali]  WITH CHECK ADD  CONSTRAINT [FK_Materiaali_Vaneri] FOREIGN KEY([VaneriID])
REFERENCES [mydb].[Vaneri] ([VaneriID])
GO
ALTER TABLE [mydb].[Materiaali] CHECK CONSTRAINT [FK_Materiaali_Vaneri]
GO
