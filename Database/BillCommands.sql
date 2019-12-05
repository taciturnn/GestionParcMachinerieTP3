CREATE TABLE [dbo].[BillCommands]
(
	[Id] INT NOT NULL IDENTITY PRIMARY KEY, 
    [CommandId] INT NOT NULL, 
    [BillId] INT NOT NULL, 
	CONSTRAINT [FK_dbo.BillCommands_dbo.Commands_CommandId] FOREIGN KEY ([CommandId]) REFERENCES [dbo].[Commands] ([Id]) ON DELETE CASCADE,
	CONSTRAINT [FK_dbo.BillCommands_dbo.Bills_BillId] FOREIGN KEY ([BillId]) REFERENCES [dbo].[Bills] ([Id]) ON DELETE CASCADE
)
