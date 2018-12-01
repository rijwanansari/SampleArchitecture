CREATE TABLE [dbo].[TransactionType] (
    [Id]          INT            NOT NULL,
    [Type]        NVARCHAR (50)  NOT NULL,
    [Description] NVARCHAR (200) NULL,
    [Code]        VARCHAR (10)   NOT NULL,
    [Active]      BIT            NOT NULL,
    [Created]     DATETIME       CONSTRAINT [DF_TransactionType_Created] DEFAULT (getdate()) NOT NULL,
    [Author]      BIGINT         NOT NULL,
    [Modified]    DATETIME       CONSTRAINT [DF_TransactionType_Modified] DEFAULT (getdate()) NOT NULL,
    [Editor]      BIGINT         NOT NULL,
    CONSTRAINT [PK_TransactionType] PRIMARY KEY CLUSTERED ([Id] ASC)
);

