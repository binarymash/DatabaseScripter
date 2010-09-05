USE [{databaseName}]
GO

ALTER TABLE [SeriesItem] NOCHECK CONSTRAINT ALL
ALTER TABLE [MediumType] NOCHECK CONSTRAINT ALL
GO

SET IDENTITY_INSERT [SeriesItem] ON
GO

INSERT [SeriesItem] ([id], [title], [episodeNum], [date], [description], [rating]) 
VALUES (1, N'{title}', N'{episodeNum}', CAST({date} AS DateTime), N'{description}', {rating})

SET IDENTITY_INSERT [SeriesItem] OFF
GO

SET IDENTITY_INSERT [MediumType] ON
GO

INSERT [MediumType] ([id], [name], [description]) 
VALUES (1, N'{mediumType1Name}', N'{mediumType1Description}')

INSERT [MediumType] ([id], [name], [description]) 
VALUES (2, N'{mediumType2Name}', N'{mediumType2Description}')

SET IDENTITY_INSERT [MediumType] OFF
GO

ALTER TABLE [SeriesItem] CHECK CONSTRAINT ALL
ALTER TABLE [MediumType] CHECK CONSTRAINT ALL
GO
