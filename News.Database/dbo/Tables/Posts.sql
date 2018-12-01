CREATE TABLE [dbo].[Posts] (
    [PostId]         INT             IDENTITY (1, 1) NOT NULL,
    [PostDate]       SMALLDATETIME   NULL,
    [PostModify]     SMALLDATETIME   NULL,
    [PostTitle]      NVARCHAR (100)  COLLATE SQL_Latin1_General_CP1_CI_AI NULL,
    [PostDecription] NVARCHAR (1000) NULL,
    [PostContent]    NVARCHAR (MAX)  NULL,
    [PostStatus]     BIT             NULL,
    [CommentStatus]  BIT             NULL,
    [Image]          NVARCHAR (1000) NULL,
    [Tag]            VARCHAR (100)   NULL,
    [PostAuthorId]   INT             NULL,
    [CategoryId]     INT             NOT NULL,
    CONSTRAINT [PK_Posts] PRIMARY KEY CLUSTERED ([PostId] ASC),
    CONSTRAINT [FK_PostsCategories] FOREIGN KEY ([CategoryId]) REFERENCES [dbo].[Categories] ([CategoryId])
);

