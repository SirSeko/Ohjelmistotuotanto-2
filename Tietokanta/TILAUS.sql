
USE mydb
GO
 IF NOT EXISTS(SELECT * FROM sys.schemas WHERE [name] = N'mydb')      
     EXEC (N'CREATE SCHEMA mydb')                                   
 GO                                                               

USE mydb
GO
IF EXISTS (SELECT * FROM sys.objects so JOIN sys.schemas sc ON so.schema_id = sc.schema_id WHERE so.name = N'Tilaus'  AND sc.name = N'mydb'  AND type in (N'U'))
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
             WHERE so.name = N'Tilaus'  AND sc.name = N'mydb'  AND type in (N'U')
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

  DROP TABLE [mydb].[Tilaus]
END 
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE 
[mydb].[Tilaus]
(
   [TilausID] int IDENTITY(1, 1)  NOT NULL,
   [TilaajanNimi] nvarchar(45)  NULL,
   [Määrä] int  NULL,
   [MateriaaliID] int  NOT NULL
)
WITH (DATA_COMPRESSION = NONE)
GO
BEGIN TRY
    EXEC sp_addextendedproperty
        N'MS_SSMA_SOURCE', N'mydb.Tilaus',
        N'SCHEMA', N'mydb',
        N'TABLE', N'Tilaus'
END TRY
BEGIN CATCH
    IF (@@TRANCOUNT > 0) ROLLBACK
    PRINT ERROR_MESSAGE()
END CATCH
GO

USE mydb
GO
IF EXISTS (SELECT * FROM sys.objects so JOIN sys.schemas sc ON so.schema_id = sc.schema_id WHERE so.name = N'PK_Tilaus_TilausID'  AND sc.name = N'mydb'  AND type in (N'PK'))
ALTER TABLE [mydb].[Tilaus] DROP CONSTRAINT [PK_Tilaus_TilausID]
 GO



ALTER TABLE [mydb].[Tilaus]
 ADD CONSTRAINT [PK_Tilaus_TilausID]
   PRIMARY KEY
   CLUSTERED ([TilausID] ASC, [MateriaaliID] ASC)

GO


USE mydb
GO
IF EXISTS (SELECT * FROM sys.objects so JOIN sys.schemas sc ON so.schema_id = sc.schema_id WHERE so.name = N'Tilaus$TilausID_UNIQUE'  AND sc.name = N'mydb'  AND type in (N'UQ'))
ALTER TABLE [mydb].[Tilaus] DROP CONSTRAINT [Tilaus$TilausID_UNIQUE]
 GO



ALTER TABLE [mydb].[Tilaus]
 ADD CONSTRAINT [Tilaus$TilausID_UNIQUE]
 UNIQUE 
   NONCLUSTERED ([TilausID] ASC)

GO


USE mydb
GO
IF EXISTS (
       SELECT * FROM sys.objects  so JOIN sys.indexes si
       ON so.object_id = si.object_id
       JOIN sys.schemas sc
       ON so.schema_id = sc.schema_id
       WHERE so.name = N'Tilaus'  AND sc.name = N'mydb'  AND si.name = N'fk_Tilaus_Materiaali_idx' AND so.type in (N'U'))
   DROP INDEX [fk_Tilaus_Materiaali_idx] ON [mydb].[Tilaus] 
GO
CREATE NONCLUSTERED INDEX [fk_Tilaus_Materiaali_idx] ON [mydb].[Tilaus]
(
   [MateriaaliID] ASC
)
WITH (SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF) ON [PRIMARY] 
GO
GO

USE mydb
GO
IF EXISTS (SELECT * FROM sys.objects so JOIN sys.schemas sc ON so.schema_id = sc.schema_id WHERE so.name = N'Tilaus$fk_Tilaus_Materiaali'  AND sc.name = N'mydb'  AND type in (N'F'))
ALTER TABLE [mydb].[Tilaus] DROP CONSTRAINT [Tilaus$fk_Tilaus_Materiaali]
 GO



ALTER TABLE [mydb].[Tilaus]
 ADD CONSTRAINT [Tilaus$fk_Tilaus_Materiaali]
 FOREIGN KEY 
   ([MateriaaliID])
 REFERENCES 
   [mydb].[mydb].[Materiaali]     ([MateriaaliID])
    ON DELETE NO ACTION
    ON UPDATE NO ACTION

GO


USE mydb
GO
ALTER TABLE  [mydb].[Tilaus]
 ADD DEFAULT NULL FOR [TilaajanNimi]
GO

ALTER TABLE  [mydb].[Tilaus]
 ADD DEFAULT NULL FOR [Määrä]
GO

