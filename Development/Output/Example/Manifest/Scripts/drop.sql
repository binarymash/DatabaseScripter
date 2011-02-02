USE [master]
GO

IF EXISTS (SELECT name FROM sys.databases WHERE name = N'${databaseName}')

  ALTER DATABASE [${databaseName}] 
  SET RESTRICTED_USER WITH ROLLBACK IMMEDIATE
  GO
  
IF  EXISTS (SELECT name FROM sys.databases WHERE name = N'${databaseName}')

  DROP DATABASE [${databaseName}]
  GO
  