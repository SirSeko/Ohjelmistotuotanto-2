
USE mydb
GO
 IF NOT EXISTS(SELECT * FROM sys.schemas WHERE [name] = N'mydb')      
     EXEC (N'CREATE SCHEMA mydb')                                   
 GO                                                               

USE mydb
GO
IF EXISTS (SELECT * FROM sys.objects so JOIN sys.schemas sc ON so.schema_id = sc.schema_id WHERE so.name = N'Varaus'  AND sc.name = N'mydb'  AND type in (N'U'))
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
             WHERE so.name = N'Varaus'  AND sc.name = N'mydb'  AND type in (N'U')
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

  DROP TABLE [mydb].[Varaus]
END 
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE 
[mydb].[Varaus]
(
   [VarausID] int IDENTITY(1, 1)  NOT NULL,
   [VaraajanNimi] nvarchar(45)  NULL,
   [MateriaaliID] int  NOT NULL,
   [Määrä] int  NULL
)
WITH (DATA_COMPRESSION = NONE)
GO
BEGIN TRY
    EXEC sp_addextendedproperty
        N'MS_SSMA_SOURCE', N'mydb.Varaus',
        N'SCHEMA', N'mydb',
        N'TABLE', N'Varaus'
END TRY
BEGIN CATCH
    IF (@@TRANCOUNT > 0) ROLLBACK
    PRINT ERROR_MESSAGE()
END CATCH
GO

USE mydb
GO
IF EXISTS (SELECT * FROM sys.objects so JOIN sys.schemas sc ON so.schema_id = sc.schema_id WHERE so.name = N'PK_Varaus_VarausID'  AND sc.name = N'mydb'  AND type in (N'PK'))
ALTER TABLE [mydb].[Varaus] DROP CONSTRAINT [PK_Varaus_VarausID]
 GO



ALTER TABLE [mydb].[Varaus]
 ADD CONSTRAINT [PK_Varaus_VarausID]
   PRIMARY KEY
   CLUSTERED ([VarausID] ASC, [MateriaaliID] ASC)

GO


USE mydb
GO
IF EXISTS (SELECT * FROM sys.objects so JOIN sys.schemas sc ON so.schema_id = sc.schema_id WHERE so.name = N'Varaus$VarausID_UNIQUE'  AND sc.name = N'mydb'  AND type in (N'UQ'))
ALTER TABLE [mydb].[Varaus] DROP CONSTRAINT [Varaus$VarausID_UNIQUE]
 GO



ALTER TABLE [mydb].[Varaus]
 ADD CONSTRAINT [Varaus$VarausID_UNIQUE]
 UNIQUE 
   NONCLUSTERED ([VarausID] ASC)

GO


USE mydb
GO
IF EXISTS (
       SELECT * FROM sys.objects  so JOIN sys.indexes si
       ON so.object_id = si.object_id
       JOIN sys.schemas sc
       ON so.schema_id = sc.schema_id
       WHERE so.name = N'Varaus'  AND sc.name = N'mydb'  AND si.name = N'fk_Varaus_Materiaali1_idx' AND so.type in (N'U'))
   DROP INDEX [fk_Varaus_Materiaali1_idx] ON [mydb].[Varaus] 
GO
CREATE NONCLUSTERED INDEX [fk_Varaus_Materiaali1_idx] ON [mydb].[Varaus]
(
   [MateriaaliID] ASC
)
WITH (SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF) ON [PRIMARY] 
GO
GO

USE mydb
GO
IF EXISTS (SELECT * FROM sys.objects so JOIN sys.schemas sc ON so.schema_id = sc.schema_id WHERE so.name = N'Varaus$fk_Varaus_Materiaali1'  AND sc.name = N'mydb'  AND type in (N'F'))
ALTER TABLE [mydb].[Varaus] DROP CONSTRAINT [Varaus$fk_Varaus_Materiaali1]
 GO



ALTER TABLE [mydb].[Varaus]
 ADD CONSTRAINT [Varaus$fk_Varaus_Materiaali1]
 FOREIGN KEY 
   ([MateriaaliID])
 REFERENCES 
   [mydb].[mydb].[Materiaali]     ([MateriaaliID])
    ON DELETE NO ACTION
    ON UPDATE NO ACTION

GO


USE mydb
GO
ALTER TABLE  [mydb].[Varaus]
 ADD DEFAULT NULL FOR [VaraajanNimi]
GO

ALTER TABLE  [mydb].[Varaus]
 ADD DEFAULT NULL FOR [Määrä]
GO

