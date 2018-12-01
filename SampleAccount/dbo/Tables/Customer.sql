CREATE TABLE [dbo].[Customer] (
    [Id]      BIGINT         IDENTITY (1, 1) NOT NULL,
    [Name]    NVARCHAR (200) NOT NULL,
    [Address] NVARCHAR (200) NOT NULL,
    CONSTRAINT [PK_Customer] PRIMARY KEY CLUSTERED ([Id] ASC)
);

