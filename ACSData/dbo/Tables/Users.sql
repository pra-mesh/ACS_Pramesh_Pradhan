CREATE TABLE [dbo].[Users] (
    [Id]                   NVARCHAR (128) NOT NULL,
    [Email]                NVARCHAR (256) NOT NULL,
    [EmailConfirmed]       BIT            NOT NULL DEFAULT 0,
    [PasswordHash]         VARBINARY(MAX) NOT NULL,
    [PhoneNumber]          NVARCHAR (MAX) NULL,
    [PhoneNumberConfirmed] BIT            NOT NULL DEFAULT 0,
    [TwoFactorEnabled]     BIT            NOT NULL DEFAULT 0,
    [LockoutEndDateUtc]    DATETIME       NULL,
    [LockoutEnabled]       BIT            NOT NULL DEFAULT 0,
    [AccessFailedCount]    INT            NOT NULL DEFAULT 0,
    [UserName]             NVARCHAR (256) NOT NULL,
    [FirstName] NVARCHAR(100) NOT NULL, 
    [LastName] NVARCHAR(100) NOT NULL, 
    CONSTRAINT [PK_dbo.Users] PRIMARY KEY CLUSTERED ([Id] ASC)
);