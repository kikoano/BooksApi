CREATE TABLE [dbo].[Books] (
    [Id]          INT            IDENTITY (1, 1) NOT NULL,
    [title]       NVARCHAR (30)  NOT NULL,
    [author]      NVARCHAR (30)  NOT NULL,
    [description] NVARCHAR (100) NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
);

