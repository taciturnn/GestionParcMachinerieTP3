CREATE TABLE [dbo].[CartItems]
(
	[Id] INT NOT NULL IDENTITY PRIMARY KEY, 
    [UserId] NVARCHAR (128) NOT NULL, 
    [MachineId] INT NOT NULL, 
    [From] BIGINT NOT NULL, 
    [To] BIGINT NOT NULL, 
	CONSTRAINT [FK_dbo.command_dbo.Machines_Id] FOREIGN KEY ([MachineId]) REFERENCES [dbo].[Machines] ([Id]),
	CONSTRAINT [FK_dbo.command_dbo.AspNetUsers_UserId] FOREIGN KEY ([UserId]) REFERENCES [dbo].[AspNetUsers] ([Id])
)
