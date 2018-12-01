CREATE TABLE [dbo].[Comments] (
    [CommentId]          INT             IDENTITY (1, 1) NOT NULL,
    [CommentAuthor]      NVARCHAR (100)  NULL,
    [CommentAuthorEmail] VARCHAR (50)    NULL,
    [CommentDate]        SMALLDATETIME   NULL,
    [CommentContent]     NVARCHAR (1000) NULL,
    [Status]             BIT             NULL,
    [CommentParent]      INT             NULL,
    [UserId]             INT             NULL,
    [PostId]             INT             NULL,
    CONSTRAINT [PK_Comments] PRIMARY KEY CLUSTERED ([CommentId] ASC),
    CONSTRAINT [FK_CommentsAspNetUsers] FOREIGN KEY ([UserId]) REFERENCES [dbo].[AspNetUsers] ([Id]),
    CONSTRAINT [FK_CommentsPosts] FOREIGN KEY ([PostId]) REFERENCES [dbo].[Posts] ([PostId])
);

