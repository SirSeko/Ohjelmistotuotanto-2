
USE mydb
GO
 IF NOT EXISTS(SELECT * FROM sys.schemas WHERE [name] = N'mydb')      
     EXEC (N'CREATE SCHEMA mydb')                                   
 GO                                                               

USE mydb
GO
IF EXISTS (SELECT * FROM sys.objects so JOIN sys.schemas sc ON so.schema_id = sc.schema_id WHERE so.name = N'Materiaali'  AND sc.name = N'mydb'  AND type in (N'U'))
BEGIN

  DECLARE @drop_statement nvarchar(500)

  DECLARE drop_cursor CURSOR FOR
      SELECT 'alter table '+quotename(schema_name(ob.schema_id))+
      '.'+quotename(object_name(ob.object_id))+ ' drop constraint ' + quotename(fk.name) 
      FROM sys.objects ob INNER JOIN sys.foreign_keys fk ON fk.parent_object_id = ob.object_id
      WHERE fk.referenced_object_id = 
          (
             SELECT so.object_id 
             FROM sys.objects so JOIN sys.schemas sc
             ON so.schema_id = sc.schema_id
             WHERE so.name = N'Materiaali'  AND sc.name = N'mydb'  AND type in (N'U')
           )

  OPEN drop_cursor

  FETCH NEXT FROM drop_cursor
  INTO @drop_statement

  WHILE @@FETCH_STATUS = 0
  BEGIN
     EXEC (@drop_statement)

     FETCH NEXT FROM drop_cursor
     INTO @drop_statement
  END

  CLOSE drop_cursor
  DEALLOCATE drop_cursor

  DROP TABLE [mydb].[Materiaali]
END 
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE 
[mydb].[Materiaali]
(
   [MateriaaliID] int IDENTITY(1, 1)  NOT NULL,
   [Nimi] nvarchar(45)  NULL,
   [Koko] nvarchar(45)  NULL,
   [Hinta] decimal(10, 0)  NULL,
   [Määrä] int  NULL
)
WITH (DATA_COMPRESSION = NONE)
GO
BEGIN TRY
    EXEC sp_addextendedproperty
        N'MS_SSMA_SOURCE', N'mydb.Materiaali',
        N'SCHEMA', N'mydb',
        N'TABLE', N'Materiaali'
END TRY
BEGIN CATCH
    IF (@@TRANCOUNT > 0) ROLLBACK
    PRINT ERROR_MESSAGE()
END CATCH
GO

USE mydb
GO
IF EXISTS (SELECT * FROM sys.objects so JOIN sys.schemas sc ON so.schema_id = sc.schema_id WHERE so.name = N'PK_Materiaali_MateriaaliID'  AND sc.name = N'mydb'  AND type in (N'PK'))
ALTER TABLE [mydb].[Materiaali] DROP CONSTRAINT [PK_Materiaali_MateriaaliID]
 GO



ALTER TABLE [mydb].[Materiaali]
 ADD CONSTRAINT [PK_Materiaali_MateriaaliID]
   PRIMARY KEY
   CLUSTERED ([MateriaaliID] ASC)

GO


USE mydb
GO
IF EXISTS (SELECT * FROM sys.objects so JOIN sys.schemas sc ON so.schema_id = sc.schema_id WHERE so.name = N'Materiaali$MateriaaliID_UNIQUE'  AND sc.name = N'mydb'  AND type in (N'UQ'))
ALTER TABLE [mydb].[Materiaali] DROP CONSTRAINT [Materiaali$MateriaaliID_UNIQUE]
 GO



ALTER TABLE [mydb].[Materiaali]
 ADD CONSTRAINT [Materiaali$MateriaaliID_UNIQUE]
 UNIQUE 
   NONCLUSTERED ([MateriaaliID] ASC)

GO


USE mydb
GO
ALTER TABLE  [mydb].[Materiaali]
 ADD DEFAULT NULL FOR [Nimi]
GO

ALTER TABLE  [mydb].[Materiaali]
 ADD DEFAULT NULL FOR [Koko]
GO

ALTER TABLE  [mydb].[Materiaali]
 ADD DEFAULT NULL FOR [Hinta]
GO

ALTER TABLE  [mydb].[Materiaali]
 ADD DEFAULT NULL FOR [Määrä]
GO

