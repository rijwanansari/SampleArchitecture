CREATE TABLE [dbo].[CurrencyRate] (
    [Id]            BIGINT          IDENTITY (1, 1) NOT NULL,
    [CurrencyId]    BIGINT          NOT NULL,
    [Rate]          DECIMAL (18, 4) NOT NULL,
    [RefCurrencyId] BIGINT          NOT NULL,
    [Active]        BIT             NOT NULL,
    [Created]       DATETIME        CONSTRAINT [DF_CurrencyRate_Created] DEFAULT (getdate()) NOT NULL,
    [Author]        BIGINT          NOT NULL,
    [Modified]      DATETIME        CONSTRAINT [DF_CurrencyRate_Modified] DEFAULT (getdate()) NOT NULL,
    [Editor]        BIGINT          NOT NULL,
    CONSTRAINT [PK_CurrencyRate] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_CurrencyRate_Currency] FOREIGN KEY ([CurrencyId]) REFERENCES [dbo].[Currency] ([Id]),
    CONSTRAINT [FK_CurrencyRate_Currency1] FOREIGN KEY ([RefCurrencyId]) REFERENCES [dbo].[Currency] ([Id])
);

