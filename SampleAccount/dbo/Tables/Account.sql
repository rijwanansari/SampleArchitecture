CREATE TABLE [dbo].[Account] (
    [Id]             BIGINT   IDENTITY (1, 1) NOT NULL,
    [AccountNumber]  BIGINT   NOT NULL,
    [AccountTypeId]  BIGINT   NOT NULL,
    [CustomerId]     BIGINT   NOT NULL,
    [OpenDate]       DATETIME NULL,
    [IsBlock]        BIT      NOT NULL,
    [IsVerified]     BIT      NOT NULL,
    [BaseCurrencyId] BIGINT   NOT NULL,
    [Active]         BIT      NOT NULL,
    [Created]        DATETIME NOT NULL,
    [Author]         BIGINT   NOT NULL,
    [Modified]       DATETIME NOT NULL,
    [Editor]         BIGINT   NOT NULL,
    CONSTRAINT [PK_Account] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_Account_Currency] FOREIGN KEY ([BaseCurrencyId]) REFERENCES [dbo].[Currency] ([Id]),
    CONSTRAINT [Unique_AccountNumber] UNIQUE NONCLUSTERED ([AccountNumber] ASC)
);

