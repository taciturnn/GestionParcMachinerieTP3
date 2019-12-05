CREATE TABLE [dbo].[Bills]
(
	[Id] INT NOT NULL IDENTITY PRIMARY KEY, 
    [UserId] NVARCHAR (128) NOT NULL, 
    [Paid] BIT NOT NULL DEFAULT 0, 
    [Value] INT NOT NULL, 
	CONSTRAINT [FK_dbo.Bills_dbo.AspNetUsers_UserId] FOREIGN KEY ([UserId]) REFERENCES [dbo].[AspNetUsers] ([Id]) ON DELETE CASCADE
)
