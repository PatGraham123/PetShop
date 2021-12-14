CREATE TABLE [dbo].[Pets] (
    [Id]            INT           IDENTITY (1, 1) NOT NULL,
    [Name]          NVARCHAR (50) NOT NULL,
    [Breed]      NVARCHAR (50) NOT NULL,
    [Cost] MONEY         NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
);