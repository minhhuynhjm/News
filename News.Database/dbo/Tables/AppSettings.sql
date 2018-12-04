CREATE TABLE [dbo].[AppSettings] (
    [Id]          INT            NOT NULL,
    [Name]        NVARCHAR (100) NULL,
    [Description] NVARCHAR (500) NULL,
    [Logo]        NVARCHAR (300) NULL,
    [Company]     NVARCHAR (100) NULL,
    [Address]     NVARCHAR (100) NULL,
    [Phone]       VARCHAR (50)   NULL,
    [Email]       VARCHAR (50)   NULL,
    [Website]     VARCHAR (50)   NULL,
    CONSTRAINT [PK_AppSettings] PRIMARY KEY CLUSTERED ([Id] ASC)
);

