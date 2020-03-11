USE [mydb]
GO
/****** Object:  Table [mydb].[Varaus]    Script Date: 10/03/2020 18.21.04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [mydb].[Varaus](
	[VarausID] [int] NOT NULL,
	[VaraajanNimi] [nvarchar](50) NOT NULL,
	[MateriaaliID] [int] NULL,
	[Päivämäärä] [date] NULL,
	[Lisätiedot] [nvarchar](100) NULL,
 CONSTRAINT [PK_Varaus] PRIMARY KEY CLUSTERED 
(
	[VarausID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [mydb].[Varaus]  WITH CHECK ADD  CONSTRAINT [FK_Varaus_Materiaali] FOREIGN KEY([MateriaaliID])
REFERENCES [mydb].[Materiaali] ([MateriaaliID])
GO
ALTER TABLE [mydb].[Varaus] CHECK CONSTRAINT [FK_Varaus_Materiaali]
GO
