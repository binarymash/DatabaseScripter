USE [master]
GO

CREATE DATABASE [{databaseName}] ON PRIMARY 
( 
    NAME = N'{databaseName}', 
    FILENAME = N'{databasePath}\{databaseName}.mdf' , 
    SIZE = 3072KB, 
    MAXSIZE = UNLIMITED, 
    FILEGROWTH = 1024KB 
)
LOG ON 
( 
    NAME = N'{databaseName}_log', 
    FILENAME = N'{databasePath}\{databaseName}_log.ldf' , 
    SIZE = 1024KB, 
    MAXSIZE = 2048GB, 
    FILEGROWTH = 10%
)

GO

