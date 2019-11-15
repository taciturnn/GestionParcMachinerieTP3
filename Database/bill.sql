CREATE TABLE [dbo].[bill]
(
	[id] INT NOT NULL IDENTITY PRIMARY KEY, 
    [client_id] INT NULL, 
    [commands] VARCHAR(255) NULL, 
    [paid] BIT NOT NULL DEFAULT 0, 
    [value] INT NULL
)
