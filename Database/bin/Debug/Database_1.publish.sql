/*
Script de déploiement pour master

Ce code a été généré par un outil.
La modification de ce fichier peut provoquer un comportement incorrect et sera perdue si
le code est régénéré.
*/

GO
SET ANSI_NULLS, ANSI_PADDING, ANSI_WARNINGS, ARITHABORT, CONCAT_NULL_YIELDS_NULL, QUOTED_IDENTIFIER ON;

SET NUMERIC_ROUNDABORT OFF;


GO
:setvar DatabaseName "master"
:setvar DefaultFilePrefix "master"
:setvar DefaultDataPath "D:\Microsoft SQL Server\MSSQL14.LOCALHOST\MSSQL\DATA\"
:setvar DefaultLogPath "D:\Microsoft SQL Server\MSSQL14.LOCALHOST\MSSQL\DATA\"

GO
:on error exit
GO
/*
Détectez le mode SQLCMD et désactivez l'exécution du script si le mode SQLCMD n'est pas pris en charge.
Pour réactiver le script une fois le mode SQLCMD activé, exécutez ce qui suit :
SET NOEXEC OFF; 
*/
:setvar __IsSqlCmdEnabled "True"
GO
IF N'$(__IsSqlCmdEnabled)' NOT LIKE N'True'
    BEGIN
        PRINT N'Le mode SQLCMD doit être activé de manière à pouvoir exécuter ce script.';
        SET NOEXEC ON;
    END


GO
USE [$(DatabaseName)];


GO
PRINT N'Création de [dbo].[AspNetRoles]...';


GO
CREATE TABLE [dbo].[AspNetRoles] (
    [Id]            NVARCHAR (128) NOT NULL,
    [Name]          NVARCHAR (256) NOT NULL,
    [Discriminator] NVARCHAR (128) NOT NULL,
    CONSTRAINT [PK_dbo.AspNetRoles] PRIMARY KEY CLUSTERED ([Id] ASC)
);


GO
PRINT N'Création de [dbo].[AspNetRoles].[RoleNameIndex]...';


GO
CREATE UNIQUE NONCLUSTERED INDEX [RoleNameIndex]
    ON [dbo].[AspNetRoles]([Name] ASC);


GO
PRINT N'Création de [dbo].[AspNetUserClaims]...';


GO
CREATE TABLE [dbo].[AspNetUserClaims] (
    [Id]         INT            IDENTITY (1, 1) NOT NULL,
    [UserId]     NVARCHAR (128) NOT NULL,
    [ClaimType]  NVARCHAR (MAX) NULL,
    [ClaimValue] NVARCHAR (MAX) NULL,
    CONSTRAINT [PK_dbo.AspNetUserClaims] PRIMARY KEY CLUSTERED ([Id] ASC)
);


GO
PRINT N'Création de [dbo].[AspNetUserClaims].[IX_UserId]...';


GO
CREATE NONCLUSTERED INDEX [IX_UserId]
    ON [dbo].[AspNetUserClaims]([UserId] ASC);


GO
PRINT N'Création de [dbo].[AspNetUserLogins]...';


GO
CREATE TABLE [dbo].[AspNetUserLogins] (
    [LoginProvider] NVARCHAR (128) NOT NULL,
    [ProviderKey]   NVARCHAR (128) NOT NULL,
    [UserId]        NVARCHAR (128) NOT NULL,
    CONSTRAINT [PK_dbo.AspNetUserLogins] PRIMARY KEY CLUSTERED ([LoginProvider] ASC, [ProviderKey] ASC, [UserId] ASC)
);


GO
PRINT N'Création de [dbo].[AspNetUserLogins].[IX_UserId]...';


GO
CREATE NONCLUSTERED INDEX [IX_UserId]
    ON [dbo].[AspNetUserLogins]([UserId] ASC);


GO
PRINT N'Création de [dbo].[AspNetUserRoles]...';


GO
CREATE TABLE [dbo].[AspNetUserRoles] (
    [UserId] NVARCHAR (128) NOT NULL,
    [RoleId] NVARCHAR (128) NOT NULL,
    CONSTRAINT [PK_dbo.AspNetUserRoles] PRIMARY KEY CLUSTERED ([UserId] ASC, [RoleId] ASC)
);


GO
PRINT N'Création de [dbo].[AspNetUserRoles].[IX_UserId]...';


GO
CREATE NONCLUSTERED INDEX [IX_UserId]
    ON [dbo].[AspNetUserRoles]([UserId] ASC);


GO
PRINT N'Création de [dbo].[AspNetUserRoles].[IX_RoleId]...';


GO
CREATE NONCLUSTERED INDEX [IX_RoleId]
    ON [dbo].[AspNetUserRoles]([RoleId] ASC);


GO
PRINT N'Création de [dbo].[AspNetUsers]...';


GO
CREATE TABLE [dbo].[AspNetUsers] (
    [Id]                   NVARCHAR (128) NOT NULL,
    [Email]                NVARCHAR (256) NULL,
    [EmailConfirmed]       BIT            NOT NULL,
    [PasswordHash]         NVARCHAR (MAX) NULL,
    [SecurityStamp]        NVARCHAR (MAX) NULL,
    [PhoneNumber]          NVARCHAR (MAX) NULL,
    [PhoneNumberConfirmed] BIT            NOT NULL,
    [TwoFactorEnabled]     BIT            NOT NULL,
    [LockoutEndDateUtc]    DATETIME       NULL,
    [LockoutEnabled]       BIT            NOT NULL,
    [AccessFailedCount]    INT            NOT NULL,
    [UserName]             NVARCHAR (256) NOT NULL,
    [FirstName]            NVARCHAR (256) NULL,
    [LastName]             NVARCHAR (256) NULL,
    [CompanyName]          NVARCHAR (256) NULL,
    CONSTRAINT [PK_dbo.AspNetUsers] PRIMARY KEY CLUSTERED ([Id] ASC)
);


GO
PRINT N'Création de [dbo].[AspNetUsers].[UserNameIndex]...';


GO
CREATE UNIQUE NONCLUSTERED INDEX [UserNameIndex]
    ON [dbo].[AspNetUsers]([UserName] ASC);


GO
PRINT N'Création de [dbo].[Bills]...';


GO
CREATE TABLE [dbo].[Bills] (
    [Id]       INT            IDENTITY (1, 1) NOT NULL,
    [UserId]   NVARCHAR (128) NOT NULL,
    [Commands] VARCHAR (255)  NOT NULL,
    [Paid]     BIT            NOT NULL,
    [Value]    INT            NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
);


GO
PRINT N'Création de [dbo].[Commands]...';


GO
CREATE TABLE [dbo].[Commands] (
    [Id]        INT            IDENTITY (1, 1) NOT NULL,
    [UserId]    NVARCHAR (128) NOT NULL,
    [MachineId] INT            NOT NULL,
    [From]      BIGINT         NOT NULL,
    [To]        BIGINT         NOT NULL,
    [Status]    VARCHAR (255)  NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
);


GO
PRINT N'Création de [dbo].[Machines]...';


GO
CREATE TABLE [dbo].[Machines] (
    [Id]        INT           IDENTITY (1, 1) NOT NULL,
    [Model]     VARCHAR (255) NOT NULL,
    [RentPrice] INT           NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
);


GO
PRINT N'Création de contrainte sans nom sur [dbo].[Bills]...';


GO
ALTER TABLE [dbo].[Bills]
    ADD DEFAULT 0 FOR [Paid];


GO
PRINT N'Création de [dbo].[FK_dbo.AspNetUserClaims_dbo.AspNetUsers_UserId]...';


GO
ALTER TABLE [dbo].[AspNetUserClaims] WITH NOCHECK
    ADD CONSTRAINT [FK_dbo.AspNetUserClaims_dbo.AspNetUsers_UserId] FOREIGN KEY ([UserId]) REFERENCES [dbo].[AspNetUsers] ([Id]) ON DELETE CASCADE;


GO
PRINT N'Création de [dbo].[FK_dbo.AspNetUserLogins_dbo.AspNetUsers_UserId]...';


GO
ALTER TABLE [dbo].[AspNetUserLogins] WITH NOCHECK
    ADD CONSTRAINT [FK_dbo.AspNetUserLogins_dbo.AspNetUsers_UserId] FOREIGN KEY ([UserId]) REFERENCES [dbo].[AspNetUsers] ([Id]) ON DELETE CASCADE;


GO
PRINT N'Création de [dbo].[FK_dbo.AspNetUserRoles_dbo.AspNetRoles_RoleId]...';


GO
ALTER TABLE [dbo].[AspNetUserRoles] WITH NOCHECK
    ADD CONSTRAINT [FK_dbo.AspNetUserRoles_dbo.AspNetRoles_RoleId] FOREIGN KEY ([RoleId]) REFERENCES [dbo].[AspNetRoles] ([Id]) ON DELETE CASCADE;


GO
PRINT N'Création de [dbo].[FK_dbo.AspNetUserRoles_dbo.AspNetUsers_UserId]...';


GO
ALTER TABLE [dbo].[AspNetUserRoles] WITH NOCHECK
    ADD CONSTRAINT [FK_dbo.AspNetUserRoles_dbo.AspNetUsers_UserId] FOREIGN KEY ([UserId]) REFERENCES [dbo].[AspNetUsers] ([Id]) ON DELETE CASCADE;


GO
PRINT N'Création de [dbo].[FK_dbo.Bills_dbo.AspNetUsers_UserId]...';


GO
ALTER TABLE [dbo].[Bills] WITH NOCHECK
    ADD CONSTRAINT [FK_dbo.Bills_dbo.AspNetUsers_UserId] FOREIGN KEY ([UserId]) REFERENCES [dbo].[AspNetUsers] ([Id]) ON DELETE CASCADE;


GO
PRINT N'Création de [dbo].[FK_dbo.command_dbo.Machines_Id]...';


GO
ALTER TABLE [dbo].[Commands] WITH NOCHECK
    ADD CONSTRAINT [FK_dbo.command_dbo.Machines_Id] FOREIGN KEY ([MachineId]) REFERENCES [dbo].[Machines] ([Id]);


GO
PRINT N'Création de [dbo].[FK_dbo.command_dbo.AspNetUsers_UserId]...';


GO
ALTER TABLE [dbo].[Commands] WITH NOCHECK
    ADD CONSTRAINT [FK_dbo.command_dbo.AspNetUsers_UserId] FOREIGN KEY ([UserId]) REFERENCES [dbo].[AspNetUsers] ([Id]);


GO
/* Post creation script here. Seed data here*/

INSERT INTO dbo.AspNetRoles (Id, Name, Discriminator) VALUES ('252af317-07fb-4021-b33f-156c19ab258b', 'Client', 'ApplicationRole'),
		('6f21fe44-08aa-4f1c-be97-10ea6a065b29', 'Admin', 'ApplicationRole'), 
		('70ab5a67-c1cc-42f6-be13-d9460accd661', 'SuperAdmin', 'ApplicationRole')

/* 'AAcarx9c3rzdkFOz0cceNihKBsouXqd0uEspt0BuZuuyWj3ODVmhT6lRWXwEbRTasg==' is 'Qwerty1234!' hashed */

INSERT INTO dbo.AspNetUsers (Id, Email, EmailConfirmed, PasswordHash, SecurityStamp, PhoneNumber, PhoneNumberConfirmed,  TwoFactorEnabled, LockoutEndDateUtc, LockoutEnabled, AccessFailedCount, UserName, FirstName, LastName, CompanyName)
VALUES  ('85846eae-d124-43b0-8046-5a18815741c8', 'admin@admin.com', 1, 'AAcarx9c3rzdkFOz0cceNihKBsouXqd0uEspt0BuZuuyWj3ODVmhT6lRWXwEbRTasg==', '69c4e121-ffc4-4c0d-9c6c-c679653c0158', NULL, 0, 0, NULL, 0, 0, 'admin@admin.com', 'Admin', 'Istrator', 'Company X'),
		('1ff6e90b-ec17-4160-81c8-749cc837a0ef', 'client_1@client.com', 1, 'AAcarx9c3rzdkFOz0cceNihKBsouXqd0uEspt0BuZuuyWj3ODVmhT6lRWXwEbRTasg==','c67cb9a7-7d8b-4dec-9f67-6b93fa2de3b5', NULL, 0, 0, NULL, 0, 0, 'client_1@client.com', 'Cli', 'Entone', 'Company Y'),
		('2ff6e90b-ec18-4160-81c8-749cc837a0ef', 'client_2@client.com', 1, 'AAcarx9c3rzdkFOz0cceNihKBsouXqd0uEspt0BuZuuyWj3ODVmhT6lRWXwEbRTasg==','c67cb9a7-7d8b-4dec-9f67-6b93fa2de3b5', NULL, 0, 0, NULL, 0, 0, 'client_2@client.com', 'Cli', 'Entwo', 'Company Z'),
		('3ff6e90b-ec19-4160-81c8-749cc837a0ef', 'client_3@client.com', 1, 'AAcarx9c3rzdkFOz0cceNihKBsouXqd0uEspt0BuZuuyWj3ODVmhT6lRWXwEbRTasg==','c67cb9a7-7d8b-4dec-9f67-6b93fa2de3b5', NULL, 0, 0, NULL, 0, 0, 'client_3@client.com', 'Cli', 'Enthree', 'Company Y')


INSERT INTO dbo.AspNetUserRoles (UserId, RoleId)
VALUES	('85846eae-d124-43b0-8046-5a18815741c8', '6f21fe44-08aa-4f1c-be97-10ea6a065b29'),
		('1ff6e90b-ec17-4160-81c8-749cc837a0ef', '252af317-07fb-4021-b33f-156c19ab258b'), 
		('2ff6e90b-ec18-4160-81c8-749cc837a0ef', '252af317-07fb-4021-b33f-156c19ab258b'),
		('3ff6e90b-ec19-4160-81c8-749cc837a0ef', '252af317-07fb-4021-b33f-156c19ab258b')
GO

GO
PRINT N'Vérification de données existantes par rapport aux nouvelles contraintes';


GO
USE [$(DatabaseName)];


GO
ALTER TABLE [dbo].[AspNetUserClaims] WITH CHECK CHECK CONSTRAINT [FK_dbo.AspNetUserClaims_dbo.AspNetUsers_UserId];

ALTER TABLE [dbo].[AspNetUserLogins] WITH CHECK CHECK CONSTRAINT [FK_dbo.AspNetUserLogins_dbo.AspNetUsers_UserId];

ALTER TABLE [dbo].[AspNetUserRoles] WITH CHECK CHECK CONSTRAINT [FK_dbo.AspNetUserRoles_dbo.AspNetRoles_RoleId];

ALTER TABLE [dbo].[AspNetUserRoles] WITH CHECK CHECK CONSTRAINT [FK_dbo.AspNetUserRoles_dbo.AspNetUsers_UserId];

ALTER TABLE [dbo].[Bills] WITH CHECK CHECK CONSTRAINT [FK_dbo.Bills_dbo.AspNetUsers_UserId];

ALTER TABLE [dbo].[Commands] WITH CHECK CHECK CONSTRAINT [FK_dbo.command_dbo.Machines_Id];

ALTER TABLE [dbo].[Commands] WITH CHECK CHECK CONSTRAINT [FK_dbo.command_dbo.AspNetUsers_UserId];


GO
PRINT N'Mise à jour terminée.';


GO
