CREATE TABLE [dbo].[Modules] (
    [Id]       INT           IDENTITY (1, 1) NOT NULL,
    [Title]    NVARCHAR (50) NULL,
    [Link]     VARCHAR (50)  NULL,
    [Sort]     INT           NULL,
    [Isactive] BIT           NULL,
    CONSTRAINT [PK_Modules] PRIMARY KEY CLUSTERED ([Id] ASC)
);

