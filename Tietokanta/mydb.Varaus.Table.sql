USE [mydb]
GO
/****** Object:  Table [mydb].[Varaus]    Script Date: 21/02/2020 19.05.54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [mydb].[Varaus](
	[VarausID] [int] NOT NULL,
	[VaraajanNimi] [nvarchar](50) NOT NULL,
	[MateriaaliID] [int] NULL,
	[LautaID] [int] NULL,
	[VaneriID] [int] NULL,
	[Määrä] [int] NOT NULL,
	[Päivämäärä] [date] NULL,
	[Lisätiedot] [nvarchar](100) NULL,
	[MaaliID] [int] NULL,
 CONSTRAINT [PK_Varaus] PRIMARY KEY CLUSTERED 
(
	[VarausID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [mydb].[Varaus]  WITH CHECK ADD  CONSTRAINT [FK_Varaus_Lauta] FOREIGN KEY([LautaID])
REFERENCES [mydb].[Lauta] ([LautaID])
GO
ALTER TABLE [mydb].[Varaus] CHECK CONSTRAINT [FK_Varaus_Lauta]
GO
ALTER TABLE [mydb].[Varaus]  WITH CHECK ADD  CONSTRAINT [FK_Varaus_Maali] FOREIGN KEY([MaaliID])
REFERENCES [mydb].[Maali] ([MaaliID])
GO
ALTER TABLE [mydb].[Varaus] CHECK CONSTRAINT [FK_Varaus_Maali]
GO
ALTER TABLE [mydb].[Varaus]  WITH CHECK ADD  CONSTRAINT [FK_Varaus_Materiaali] FOREIGN KEY([MateriaaliID])
REFERENCES [mydb].[Materiaali] ([MateriaaliID])
GO
ALTER TABLE [mydb].[Varaus] CHECK CONSTRAINT [FK_Varaus_Materiaali]
GO
ALTER TABLE [mydb].[Varaus]  WITH CHECK ADD  CONSTRAINT [FK_Varaus_Vaneri] FOREIGN KEY([VaneriID])
REFERENCES [mydb].[Vaneri] ([VaneriID])
GO
ALTER TABLE [mydb].[Varaus] CHECK CONSTRAINT [FK_Varaus_Vaneri]
GO
