CREATE TABLE [dbo].[TransactionDetail] (
    [Id]                   BIGINT          IDENTITY (1, 1) NOT NULL,
    [TransactionTypeId]    INT             NOT NULL,
    [AccountId]            BIGINT          NOT NULL,
    [AccountNumber]        BIGINT          NOT NULL,
    [Amount]               DECIMAL (18, 4) NOT NULL,
    [CurrencyDepositId]    BIGINT          NOT NULL,
    [AmountInBaseCurrency] DECIMAL (18, 4) NOT NULL,
    [CurrentBalance]       DECIMAL (18, 4) NOT NULL,
    [Active]               BIT             NOT NULL,
    [Created]              DATETIME        NOT NULL,
    [Author]               BIGINT          NOT NULL,
    [Modified]             DATETIME        NOT NULL,
    [Editor]               BIGINT          NOT NULL,
    CONSTRAINT [PK_Transaction] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_Transaction_Account] FOREIGN KEY ([AccountNumber]) REFERENCES [dbo].[Account] ([AccountNumber]),
    CONSTRAINT [FK_Transaction_Currency] FOREIGN KEY ([CurrencyDepositId]) REFERENCES [dbo].[Currency] ([Id]),
    CONSTRAINT [FK_Transaction_TransactionType] FOREIGN KEY ([TransactionTypeId]) REFERENCES [dbo].[TransactionType] ([Id])
);

