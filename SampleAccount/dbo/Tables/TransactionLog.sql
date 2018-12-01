CREATE TABLE [dbo].[TransactionLog] (
    [Id]                        BIGINT          IDENTITY (1, 1) NOT NULL,
    [HostIP]                    NVARCHAR (50)   NOT NULL,
    [TransactionId]             BIGINT          NULL,
    [AccountId]                 BIGINT          NOT NULL,
    [AccountNumber]             BIGINT          NOT NULL,
    [TransactionTypeId]         INT             NOT NULL,
    [TransactionCurrency]       VARCHAR (5)     NULL,
    [CurrencyRateWithBase]      DECIMAL (18, 4) NULL,
    [BaseCurrency]              VARCHAR (5)     NULL,
    [TransactionDate]           DATETIME        CONSTRAINT [DF_TransactionLog_TransactionDate] DEFAULT (getdate()) NULL,
    [BalanceAmountBaseCurrency] DECIMAL (18, 4) NULL,
    [Status]                    VARCHAR (50)    NOT NULL,
    [Active]                    BIT             NOT NULL,
    [Created]                   DATETIME        CONSTRAINT [DF_TransactionLog_Created] DEFAULT (getdate()) NOT NULL,
    [Author]                    BIGINT          NOT NULL,
    [Modified]                  DATETIME        CONSTRAINT [DF_TransactionLog_Modified] DEFAULT (getdate()) NOT NULL,
    [Editor]                    BIGINT          NOT NULL,
    CONSTRAINT [PK_TransactionLog] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_TransactionLog_Account] FOREIGN KEY ([AccountId]) REFERENCES [dbo].[Account] ([Id]),
    CONSTRAINT [FK_TransactionLog_Transaction] FOREIGN KEY ([TransactionId]) REFERENCES [dbo].[TransactionDetail] ([Id]),
    CONSTRAINT [FK_TransactionLog_TransactionType] FOREIGN KEY ([TransactionTypeId]) REFERENCES [dbo].[TransactionType] ([Id])
);

