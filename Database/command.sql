CREATE TABLE [dbo].[command]
(
	[id] INT NOT NULL IDENTITY PRIMARY KEY, 
    [client_id] INT NULL, 
    [machine_id] INT NULL, 
    [from] BIGINT NULL, 
    [to] BIGINT NULL, 
    [status] VARCHAR(255) NULL
)
