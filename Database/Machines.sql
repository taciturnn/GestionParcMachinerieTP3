﻿CREATE TABLE [dbo].[Machines]
(
	[Id] INT NOT NULL IDENTITY PRIMARY KEY, 
    [Model] VARCHAR(255) NOT NULL, 
    [RentPrice] INT NOT NULL
)
