CREATE TABLE [dbo].[AccountType] (
    [Id]          BIGINT         NOT NULL,
    [TypeName]    NVARCHAR (50)  NOT NULL,
    [Description] NVARCHAR (200) NULL,
    [Code]        VARCHAR (10)   NOT NULL,
    [Created]     DATETIME       CONSTRAINT [DF_AccountType_Created] DEFAULT (getdate()) NOT NULL,
    [Author]      BIGINT         NOT NULL,
    [Modified]    DATETIME       CONSTRAINT [DF_AccountType_Modified] DEFAULT (getdate()) NOT NULL,
    [Editor]      BIGINT         NOT NULL,
    CONSTRAINT [PK_AccountType] PRIMARY KEY CLUSTERED ([Id] ASC)
);

