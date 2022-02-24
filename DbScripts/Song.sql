CREATE TABLE [awm].[Song] (
	[Id] INT NOT NULL PRIMARY KEY CLUSTERED IDENTITY,
	[RegisteredById] INT NOT NULL,
	[Name] NVARCHAR(200) NULL,
	[Artist] NVARCHAR(200) NULL,
	[Album] NVARCHAR(200) NULL,
	[Year] INT NULL,
	[Url] NVARCHAR(600) NULL,
	[CoverUrl] NVARCHAR(600) NULL,
	[IsPublic] BIT NOT NULL
)
GO

ALTER TABLE [awm].[Song]
	ADD CONSTRAINT FK_Song_RegisteredById FOREIGN KEY ([RegisteredById]) REFERENCES [awm].[User] ([Id])
GO

INSERT INTO awm.Song ([RegisteredById], [Name], [Artist], [Album], [Year], [Url], [CoverUrl], [IsPublic])
	VALUES (1, 'I Ran', 'A Flock Of Seagulls', 'A Flock Of Seagulls', 1982, 'https://www.youtube.com/watch?v=iIpfWORQWhU', 'https://arcticreviewsblog.files.wordpress.com/2020/04/flock.jpg', 1)