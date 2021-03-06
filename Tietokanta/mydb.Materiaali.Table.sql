USE [mydb]
GO
/****** Object:  Table [mydb].[Materiaali]    Script Date: 23/03/2020 16.54.57 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [mydb].[Materiaali](
	[MateriaaliID] [int] IDENTITY(10000,1) NOT NULL,
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
ALTER TABLE [mydb].[Materiaali]  WITH CHECK ADD  CONSTRAINT [FK_Materiaali_Lauta1] FOREIGN KEY([LautaID])
REFERENCES [mydb].[Lauta] ([LautaID])
GO
ALTER TABLE [mydb].[Materiaali] CHECK CONSTRAINT [FK_Materiaali_Lauta1]
GO
ALTER TABLE [mydb].[Materiaali]  WITH CHECK ADD  CONSTRAINT [FK_Materiaali_Maali1] FOREIGN KEY([MaaliID])
REFERENCES [mydb].[Maali] ([MaaliID])
GO
ALTER TABLE [mydb].[Materiaali] CHECK CONSTRAINT [FK_Materiaali_Maali1]
GO
ALTER TABLE [mydb].[Materiaali]  WITH CHECK ADD  CONSTRAINT [FK_Materiaali_Valineet1] FOREIGN KEY([ValineetID])
REFERENCES [mydb].[Valineet] ([ValineetID])
GO
ALTER TABLE [mydb].[Materiaali] CHECK CONSTRAINT [FK_Materiaali_Valineet1]
GO
ALTER TABLE [mydb].[Materiaali]  WITH CHECK ADD  CONSTRAINT [FK_Materiaali_Vaneri1] FOREIGN KEY([VaneriID])
REFERENCES [mydb].[Vaneri] ([VaneriID])
GO
ALTER TABLE [mydb].[Materiaali] CHECK CONSTRAINT [FK_Materiaali_Vaneri1]
GO
