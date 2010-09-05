USE [{databaseName}]
GO

CREATE TABLE [dbo].[CodecType]
(
	[id] [int] IDENTITY(1,1) NOT NULL,
	[name] [nvarchar](max) NOT NULL,
	[description] [nvarchar](max) NULL,
    CONSTRAINT [PK_CodecType] PRIMARY KEY CLUSTERED 
    (
	    [id] ASC
    ) WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

CREATE TABLE [dbo].[SeriesItem]
(
	[id] [int] IDENTITY(1,1) NOT NULL,
	[title] [nvarchar](max) NOT NULL,
	[episodeNum] [nvarchar](max) NULL,
	[date] [datetime] NULL,
	[description] [nvarchar](max) NULL,
	[rating] [smallint] NULL,
    CONSTRAINT [PK_SeriesItem] PRIMARY KEY CLUSTERED 
    (
	    [id] ASC
    ) WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

CREATE TABLE [dbo].[MediumType]
(
	[id] [int] IDENTITY(1,1) NOT NULL,
	[description] [nvarchar](max) NULL,
	[name] [nvarchar](max) NOT NULL,
    CONSTRAINT [PK_MediumType] PRIMARY KEY CLUSTERED 
    (
	    [id] ASC
    ) WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

CREATE TABLE [dbo].[Medium]
(
	[id] [int] IDENTITY(1,1) NOT NULL,
	[catalogueNumber] [nvarchar](max) NULL,
	[mediumTypeId] [int] NOT NULL,
    CONSTRAINT [PK_Medium] PRIMARY KEY CLUSTERED 
    (
	    [id] ASC
    ) WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

CREATE TABLE [dbo].[Encoding]
(
	[id] [int] IDENTITY(1,1) NOT NULL,
	[audioBitRateKBits] [int] NULL,
	[videoBitRateKBits] [int] NULL,
	[codecTypeId] [int] NOT NULL,
    CONSTRAINT [PK_Encoding] PRIMARY KEY CLUSTERED 
    (
	    [id] ASC
    ) WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

CREATE TABLE [dbo].[PersistedSeriesItem]
(
	[id] [int] IDENTITY(1,1)  NOT NULL,
	[mediumId] [int] NOT NULL,
	[seriesItemId] [int] NOT NULL,
	[sizeKb] [bigint] NULL,
	[uri] [nvarchar](max) NULL,
	[encodingId] [int] NOT NULL,
    CONSTRAINT [PK_PersistedSeriesItem] PRIMARY KEY CLUSTERED 
    (
	    [id] ASC
    ) WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

ALTER TABLE [dbo].[Encoding]  
WITH CHECK ADD CONSTRAINT [FK_Encoding_CodecType] FOREIGN KEY([codecTypeId])
REFERENCES [dbo].[CodecType] ([id])

GO

ALTER TABLE [dbo].[Encoding] 
CHECK CONSTRAINT [FK_Encoding_CodecType]

GO

ALTER TABLE [dbo].[Medium]  
WITH CHECK ADD CONSTRAINT [FK_Medium_MediumType] FOREIGN KEY([mediumTypeId])
REFERENCES [dbo].[MediumType] ([id])

GO

ALTER TABLE [dbo].[Medium] 
CHECK CONSTRAINT [FK_Medium_MediumType]
GO

ALTER TABLE [dbo].[PersistedSeriesItem]  
WITH CHECK ADD CONSTRAINT [FK_PersistedSeriesItem_Encoding] FOREIGN KEY([encodingId])
REFERENCES [dbo].[Encoding] ([id])

GO

ALTER TABLE [dbo].[PersistedSeriesItem] 
CHECK CONSTRAINT [FK_PersistedSeriesItem_Encoding]

GO

ALTER TABLE [dbo].[PersistedSeriesItem]  
WITH CHECK ADD CONSTRAINT [FK_PersistedSeriesItem_Medium] FOREIGN KEY([mediumId])
REFERENCES [dbo].[Medium] ([id])

GO

ALTER TABLE [dbo].[PersistedSeriesItem] CHECK CONSTRAINT [FK_PersistedSeriesItem_Medium]

GO

ALTER TABLE [dbo].[PersistedSeriesItem]  
WITH CHECK ADD CONSTRAINT [FK_PersistedSeriesItem_SeriesItem] FOREIGN KEY([seriesItemId])
REFERENCES [dbo].[SeriesItem] ([id])

GO

ALTER TABLE [dbo].[PersistedSeriesItem] 
CHECK CONSTRAINT [FK_PersistedSeriesItem_SeriesItem]

GO
