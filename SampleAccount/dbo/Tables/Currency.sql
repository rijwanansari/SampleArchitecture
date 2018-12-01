CREATE TABLE [dbo].[Currency] (
    [Id]       BIGINT         IDENTITY (1, 1) NOT NULL,
    [Name]     NVARCHAR (100) NOT NULL,
    [Code]     NVARCHAR (5)   NOT NULL,
    [IsoCode]  VARCHAR (3)    NOT NULL,
    [Country]  VARCHAR (100)  NOT NULL,
    [Active]   BIT            NOT NULL,
    [Created]  DATETIME       CONSTRAINT [DF_Currency_Created] DEFAULT (getdate()) NOT NULL,
    [Author]   BIGINT         NOT NULL,
    [Modified] DATETIME       CONSTRAINT [DF_Currency_Modified] DEFAULT (getdate()) NOT NULL,
    [Editor]   BIGINT         NOT NULL,
    CONSTRAINT [PK_Currency] PRIMARY KEY CLUSTERED ([Id] ASC)
);

