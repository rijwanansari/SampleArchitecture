CREATE TABLE [dbo].[ResponseLog] (
    [Id]       BIGINT        IDENTITY (1, 1) NOT NULL,
    [HostIP]   NVARCHAR (50) NULL,
    [Request]  VARCHAR (MAX) NULL,
    [Response] VARCHAR (MAX) NULL,
    [Active]   BIT           NOT NULL,
    [Created]  DATETIME      CONSTRAINT [DF_ResponseLog_Created] DEFAULT (getdate()) NOT NULL,
    [Author]   BIGINT        NOT NULL,
    [Modified] DATETIME      CONSTRAINT [DF_ResponseLog_Modified] DEFAULT (getdate()) NOT NULL,
    [Editor]   BIGINT        NOT NULL,
    CONSTRAINT [PK_ResponseLog] PRIMARY KEY CLUSTERED ([Id] ASC)
);

