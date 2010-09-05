USE [{databaseName}]
GO

CREATE TABLE [dbo].[AnotherTable]
(
	[id] [int] IDENTITY(1,1) NOT NULL,
	[name] [nvarchar](max) NOT NULL,
	[description] [nvarchar](max) NULL,
) ON [PRIMARY]

GO